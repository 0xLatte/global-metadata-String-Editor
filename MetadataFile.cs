using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlTypes;

namespace MetaDataStringEditor {
    class MetadataFile : IDisposable {
        public BinaryReader reader;

        private uint stringLiteralOffset;
        private uint stringLiteralCount;
        private long DataInfoPosition;
        private uint stringLiteralDataOffset;
        private uint stringLiteralDataCount;
        private List<StringLiteral> stringLiterals = new List<StringLiteral>();
        public List<byte[]> strBytes = new List<byte[]>();

        public MetadataFile(string fullName) {
            reader = new BinaryReader(File.OpenRead(fullName));

            // Read File
            ReadHeader();

            // Obtaining a String
            ReadLiteral();
            ReadStrByte();

            Logger.I("File read complete");
        }

        private void ReadHeader() {
            Logger.I("Read header");
            uint vansity = reader.ReadUInt32();
            if (vansity != 0xFAB11BAF) {
                throw new Exception("Failed flag check");
            }
            int version = reader.ReadInt32();
            stringLiteralOffset = reader.ReadUInt32();      // The location of the list area will not be changed later
            stringLiteralCount = reader.ReadUInt32();       // The size of the list area will not be changed later
            DataInfoPosition = reader.BaseStream.Position;  // Make a note of the current position
            stringLiteralDataOffset = reader.ReadUInt32();  // The location of the data area, which may have to be changed
            stringLiteralDataCount = reader.ReadUInt32();   // The length of the data area, which may have to be changed
        }

        private void ReadLiteral() {
            Logger.I("Read literal");
            ProgressBar.SetMax((int)stringLiteralCount / 8);

            reader.BaseStream.Position = stringLiteralOffset;
            for (int i = 0; i < stringLiteralCount / 8; i++) {
                stringLiterals.Add(new StringLiteral {
                    Length = reader.ReadUInt32(),
                    Offset = reader.ReadUInt32()
                });
                ProgressBar.Report();
            }
        }

        private void ReadStrByte() {
            Logger.I("Read the string of the bytes");
            ProgressBar.SetMax(stringLiterals.Count);

            for (int i = 0; i < stringLiterals.Count; i++) {
                reader.BaseStream.Position = stringLiteralDataOffset + stringLiterals[i].Offset;
                strBytes.Add(reader.ReadBytes((int)stringLiterals[i].Length));
                ProgressBar.Report();
            }
        }

        public void WriteToNewFile(string fileName) {
            BinaryWriter writer = new BinaryWriter(File.Create(fileName));

            // Let's copy everything.
            reader.BaseStream.Position = 0;
            reader.BaseStream.CopyTo(writer.BaseStream);

            // Update Literal
            Logger.I("Update literal");
            ProgressBar.SetMax(stringLiterals.Count);
            writer.BaseStream.Position = stringLiteralOffset;
            uint count = 0;
            for (int i = 0; i < stringLiterals.Count; i++) {

                stringLiterals[i].Offset = count;
                stringLiterals[i].Length = (uint)strBytes[i].Length;

                writer.Write(stringLiterals[i].Length);
                writer.Write(stringLiterals[i].Offset);
                count += stringLiterals[i].Length;

                ProgressBar.Report();
            }

            // Perform an alignment, not sure if it's necessarily needed, but Unity does it, so it's better to make up for it
            var tmp = (stringLiteralDataOffset + count) % 4;
            if (tmp != 0) count += 4 - tmp;

            // Check if there is enough space for
            if (count > stringLiteralDataCount) {
                // Check if there is any other data behind the data area, if not, you can extend the data area directly.
                if (stringLiteralDataOffset + stringLiteralDataCount < writer.BaseStream.Length) {
                    // The original space was not enough to put it in, and it could not be extended directly, so the whole thing was moved to the end of the document
                    stringLiteralDataOffset = (uint)writer.BaseStream.Length;
                }
            }
            stringLiteralDataCount = count;

            // Write String
            Logger.I("Update string");
            ProgressBar.SetMax(strBytes.Count);
            writer.BaseStream.Position = stringLiteralDataOffset;
            for (int i = 0; i < strBytes.Count; i++) {
                writer.Write(strBytes[i]);
                ProgressBar.Report();
            }

            // Update Header
            Logger.I("Update header");
            writer.BaseStream.Position = DataInfoPosition;
            writer.Write(stringLiteralDataOffset);
            writer.Write(stringLiteralDataCount);

            Logger.I("Update complete");
            writer.Close();
        }
        
        public void Dispose() {
            reader?.Dispose();
        }
        
        public class StringLiteral {
            public uint Length;
            public uint Offset;
        }

        public void ExportToText(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (var byteArray in strBytes)
                {
                    string str = Encoding.UTF8.GetString(byteArray);
                    writer.WriteLine(str);
                }
            }
        }

        public void ExportToCSV(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (var byteArray in strBytes)
                {
                    string str = Encoding.UTF8.GetString(byteArray);
                    // Escape processing as needed to match CSV format
                    writer.WriteLine($"\"{str.Replace("\"", "\"\"")}\"");
                }
            }
        }
    }

}
