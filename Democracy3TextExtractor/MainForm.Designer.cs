namespace Democracy3TextExtractor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.folderBrowserDialogSource = new System.Windows.Forms.FolderBrowserDialog();
            this.btCompile = new System.Windows.Forms.Button();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.btBrowseSource = new System.Windows.Forms.Button();
            this.btTransifexFile = new System.Windows.Forms.Button();
            this.textBoxTransifexFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialogTransifex = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btExtract = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.btOutput = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCompile
            // 
            this.btCompile.Location = new System.Drawing.Point(649, 16);
            this.btCompile.Name = "btCompile";
            this.btCompile.Size = new System.Drawing.Size(149, 23);
            this.btCompile.TabIndex = 2;
            this.btCompile.Text = "Re-inject Text";
            this.btCompile.UseVisualStyleBackColor = true;
            this.btCompile.Click += new System.EventHandler(this.btCompile_Click);
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(147, 14);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.ReadOnly = true;
            this.textBoxSource.Size = new System.Drawing.Size(453, 20);
            this.textBoxSource.TabIndex = 3;
            // 
            // btBrowseSource
            // 
            this.btBrowseSource.Location = new System.Drawing.Point(607, 12);
            this.btBrowseSource.Name = "btBrowseSource";
            this.btBrowseSource.Size = new System.Drawing.Size(25, 23);
            this.btBrowseSource.TabIndex = 4;
            this.btBrowseSource.Text = "...";
            this.btBrowseSource.UseVisualStyleBackColor = true;
            this.btBrowseSource.Click += new System.EventHandler(this.btBrowseSource_Click);
            // 
            // btTransifexFile
            // 
            this.btTransifexFile.Location = new System.Drawing.Point(602, 16);
            this.btTransifexFile.Name = "btTransifexFile";
            this.btTransifexFile.Size = new System.Drawing.Size(25, 23);
            this.btTransifexFile.TabIndex = 6;
            this.btTransifexFile.Text = "...";
            this.btTransifexFile.UseVisualStyleBackColor = true;
            this.btTransifexFile.Click += new System.EventHandler(this.btTransifexFile_Click);
            // 
            // textBoxTransifexFile
            // 
            this.textBoxTransifexFile.Location = new System.Drawing.Point(142, 18);
            this.textBoxTransifexFile.Name = "textBoxTransifexFile";
            this.textBoxTransifexFile.ReadOnly = true;
            this.textBoxTransifexFile.Size = new System.Drawing.Size(453, 20);
            this.textBoxTransifexFile.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "Democracy 3 \"data\" folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "Transifex translation file";
            // 
            // openFileDialogTransifex
            // 
            this.openFileDialogTransifex.Filter = "Translation ini|*.ini|All files|*.*";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(812, 100);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btExtract);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBoxOutputFolder);
            this.tabPage1.Controls.Add(this.btOutput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(804, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Extraction";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(139, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "democracy3ExtractedText.ini";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "Filename";
            // 
            // btExtract
            // 
            this.btExtract.Location = new System.Drawing.Point(649, 16);
            this.btExtract.Name = "btExtract";
            this.btExtract.Size = new System.Drawing.Size(149, 23);
            this.btExtract.TabIndex = 1;
            this.btExtract.Text = "Extract Text";
            this.btExtract.UseVisualStyleBackColor = true;
            this.btExtract.Click += new System.EventHandler(this.btExtract_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "Output folder";
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.Location = new System.Drawing.Point(142, 18);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.ReadOnly = true;
            this.textBoxOutputFolder.Size = new System.Drawing.Size(453, 20);
            this.textBoxOutputFolder.TabIndex = 10;
            // 
            // btOutput
            // 
            this.btOutput.Location = new System.Drawing.Point(602, 16);
            this.btOutput.Name = "btOutput";
            this.btOutput.Size = new System.Drawing.Size(25, 23);
            this.btOutput.TabIndex = 11;
            this.btOutput.Text = "...";
            this.btOutput.UseVisualStyleBackColor = true;
            this.btOutput.Click += new System.EventHandler(this.btOutput_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btCompile);
            this.tabPage2.Controls.Add(this.textBoxTransifexFile);
            this.tabPage2.Controls.Add(this.btTransifexFile);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(804, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Injection";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 151);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btBrowseSource);
            this.Controls.Add(this.textBoxSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Democracy 3 Text Extractor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSource;
        private System.Windows.Forms.Button btCompile;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Button btBrowseSource;
        private System.Windows.Forms.Button btTransifexFile;
        private System.Windows.Forms.TextBox textBoxTransifexFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialogTransifex;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btExtract;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.Button btOutput;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}

