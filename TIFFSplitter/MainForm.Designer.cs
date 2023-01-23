namespace TIFFSplitter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSplit = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.CompressionCheckBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(451, 94);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 23);
            this.btnSplit.TabIndex = 0;
            this.btnSplit.Text = "Rozdělit";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.BtnSplit_Click);
            // 
            // tbPath
            // 
            this.tbPath.BackColor = System.Drawing.Color.White;
            this.tbPath.Location = new System.Drawing.Point(12, 12);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(433, 23);
            this.tbPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(451, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Procházet";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // CompressionCheckBox
            // 
            this.CompressionCheckBox.CheckOnClick = true;
            this.CompressionCheckBox.FormattingEnabled = true;
            this.CompressionCheckBox.Items.AddRange(new object[] {
            "None",
            "LZW",
            "CCITT Group 3 fax",
            "CCITT Group 4 fax"});
            this.CompressionCheckBox.Location = new System.Drawing.Point(12, 41);
            this.CompressionCheckBox.Name = "CompressionCheckBox";
            this.CompressionCheckBox.Size = new System.Drawing.Size(127, 76);
            this.CompressionCheckBox.TabIndex = 3;
            this.CompressionCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CompressionCheckBox_ItemCheck);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 127);
            this.Controls.Add(this.CompressionCheckBox);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btnSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "TIFF Splitter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSplit;
        private TextBox tbPath;
        private Button btnBrowse;
        private CheckedListBox CompressionCheckBox;
    }
}