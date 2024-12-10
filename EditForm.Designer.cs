namespace MetaDataStringEditor {
    partial class EditForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.DiscardChanges = new System.Windows.Forms.Button();
            this.RestoreChanges = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(332, 203);
            this.textBox1.TabIndex = 0;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(270, 230);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // DiscardChanges
            // 
            this.DiscardChanges.Location = new System.Drawing.Point(173, 230);
            this.DiscardChanges.Name = "Discard";
            this.DiscardChanges.Size = new System.Drawing.Size(91, 23);
            this.DiscardChanges.TabIndex = 2;
            this.DiscardChanges.Text = "Discard";
            this.DiscardChanges.UseVisualStyleBackColor = true;
            this.DiscardChanges.Click += new System.EventHandler(this.DiscardChanges_Click);
            // 
            // RestoreChanges
            // 
            this.RestoreChanges.Location = new System.Drawing.Point(62, 230);
            this.RestoreChanges.Name = "Restore";
            this.RestoreChanges.Size = new System.Drawing.Size(105, 23);
            this.RestoreChanges.TabIndex = 3;
            this.RestoreChanges.Text = "Restore";
            this.RestoreChanges.UseVisualStyleBackColor = true;
            this.RestoreChanges.Click += new System.EventHandler(this.RestoreChanges_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 265);
            this.Controls.Add(this.RestoreChanges);
            this.Controls.Add(this.DiscardChanges);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Edit Form";
            this.ShowIcon = false;
            this.Text = "Edit Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button DiscardChanges;
        private System.Windows.Forms.Button RestoreChanges;
    }
}