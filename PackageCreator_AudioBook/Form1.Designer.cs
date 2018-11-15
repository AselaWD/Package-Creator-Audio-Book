namespace PackageCreator_AudioBook
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txt_FilePath = new System.Windows.Forms.TextBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.lbl_FilePath = new System.Windows.Forms.Label();
            this.rdo_Simplified = new System.Windows.Forms.RadioButton();
            this.rdo_Detailed = new System.Windows.Forms.RadioButton();
            this.btn_Generate = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbl_TemplateType = new System.Windows.Forms.Label();
            this.rdo_FetchMetadata = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txt_FilePath
            // 
            this.txt_FilePath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_FilePath.Location = new System.Drawing.Point(123, 64);
            this.txt_FilePath.Name = "txt_FilePath";
            this.txt_FilePath.Size = new System.Drawing.Size(298, 20);
            this.txt_FilePath.TabIndex = 0;
            this.txt_FilePath.WordWrap = false;
            this.txt_FilePath.TextChanged += new System.EventHandler(this.txt_FilePath_TextChanged);
            // 
            // btn_Browse
            // 
            this.btn_Browse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Browse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Browse.Location = new System.Drawing.Point(451, 54);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(75, 38);
            this.btn_Browse.TabIndex = 1;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // lbl_FilePath
            // 
            this.lbl_FilePath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_FilePath.AutoSize = true;
            this.lbl_FilePath.Location = new System.Drawing.Point(24, 69);
            this.lbl_FilePath.Name = "lbl_FilePath";
            this.lbl_FilePath.Size = new System.Drawing.Size(79, 13);
            this.lbl_FilePath.TabIndex = 2;
            this.lbl_FilePath.Text = "Template Path:";
            // 
            // rdo_Simplified
            // 
            this.rdo_Simplified.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdo_Simplified.AutoSize = true;
            this.rdo_Simplified.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdo_Simplified.Location = new System.Drawing.Point(258, 29);
            this.rdo_Simplified.Name = "rdo_Simplified";
            this.rdo_Simplified.Size = new System.Drawing.Size(69, 17);
            this.rdo_Simplified.TabIndex = 3;
            this.rdo_Simplified.TabStop = true;
            this.rdo_Simplified.Text = "Simplified";
            this.rdo_Simplified.UseVisualStyleBackColor = true;
            this.rdo_Simplified.CheckedChanged += new System.EventHandler(this.rdo_Simplified_CheckedChanged);
            // 
            // rdo_Detailed
            // 
            this.rdo_Detailed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdo_Detailed.AutoSize = true;
            this.rdo_Detailed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdo_Detailed.Enabled = false;
            this.rdo_Detailed.Location = new System.Drawing.Point(357, 29);
            this.rdo_Detailed.Name = "rdo_Detailed";
            this.rdo_Detailed.Size = new System.Drawing.Size(64, 17);
            this.rdo_Detailed.TabIndex = 4;
            this.rdo_Detailed.TabStop = true;
            this.rdo_Detailed.Text = "Detailed";
            this.rdo_Detailed.UseVisualStyleBackColor = true;
            this.rdo_Detailed.CheckedChanged += new System.EventHandler(this.rdo_Detailed_CheckedChanged);
            // 
            // btn_Generate
            // 
            this.btn_Generate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Generate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Generate.Location = new System.Drawing.Point(213, 122);
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(124, 39);
            this.btn_Generate.TabIndex = 5;
            this.btn_Generate.Text = "Generate";
            this.btn_Generate.UseVisualStyleBackColor = true;
            this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(12, 190);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(530, 15);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.UseWaitCursor = true;
            // 
            // lbl_TemplateType
            // 
            this.lbl_TemplateType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_TemplateType.AutoSize = true;
            this.lbl_TemplateType.Location = new System.Drawing.Point(28, 31);
            this.lbl_TemplateType.Name = "lbl_TemplateType";
            this.lbl_TemplateType.Size = new System.Drawing.Size(75, 13);
            this.lbl_TemplateType.TabIndex = 7;
            this.lbl_TemplateType.Text = "Process Type:";
            // 
            // rdo_FetchMetadata
            // 
            this.rdo_FetchMetadata.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdo_FetchMetadata.AutoSize = true;
            this.rdo_FetchMetadata.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdo_FetchMetadata.Location = new System.Drawing.Point(123, 29);
            this.rdo_FetchMetadata.Name = "rdo_FetchMetadata";
            this.rdo_FetchMetadata.Size = new System.Drawing.Size(100, 17);
            this.rdo_FetchMetadata.TabIndex = 8;
            this.rdo_FetchMetadata.TabStop = true;
            this.rdo_FetchMetadata.Text = "Fetch Metadata";
            this.rdo_FetchMetadata.UseVisualStyleBackColor = true;
            this.rdo_FetchMetadata.CheckedChanged += new System.EventHandler(this.rdo_FetchMetadata_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 216);
            this.Controls.Add(this.rdo_FetchMetadata);
            this.Controls.Add(this.lbl_TemplateType);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_Generate);
            this.Controls.Add(this.rdo_Detailed);
            this.Controls.Add(this.rdo_Simplified);
            this.Controls.Add(this.lbl_FilePath);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.txt_FilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Package Creator Audiobooks";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_FilePath;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Label lbl_FilePath;
        private System.Windows.Forms.RadioButton rdo_Simplified;
        private System.Windows.Forms.RadioButton rdo_Detailed;
        private System.Windows.Forms.Button btn_Generate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbl_TemplateType;
        private System.Windows.Forms.RadioButton rdo_FetchMetadata;
    }
}

