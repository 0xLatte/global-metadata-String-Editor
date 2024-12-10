# String Modify Tool for Unity global-metadata.dat

## Reference
- [il2cppdumper](https://github.com/Perfare/Il2CppDumper)<br>
The understanding of the contents of this file are learned from the source code of this tool, which itself is used to export class definitions from inside the compiled libil2cpp.so file and the global-metadata.dat file, in the form of rename scripts available for IDA, DLLs available for UABE and AssetStudio, etc., and it is a very useful tool.
## Changes
In global-metadata.dat, the way to save the strings in the code is that there is a list in the header to put the offset, length, etc. of each string, and then there is an area in the data area to compactly put all the strings directly, with a list in the header, so it doesn't need to be \0-ending.<br><br>
Because the number of strings remains the same before and after the modification, the modification of the list is a direct overwrite on the original area. The length of the data area may change. If the length of the data area is less than or equal to the original length after the modification, it is directly overwritten and written, if it is too long, it is written to the end of the file.<br><br>
Change to English.<br><br>
Added save as text and CSV files.
