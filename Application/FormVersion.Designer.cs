namespace Mossywell.BSR
{
    partial class FormVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVersion));
            this.textBoxThisVersion = new System.Windows.Forms.TextBox();
            this.textBoxLatestVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxQuestion = new System.Windows.Forms.TextBox();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxThisVersion
            // 
            this.textBoxThisVersion.AcceptsTab = true;
            this.textBoxThisVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxThisVersion.Location = new System.Drawing.Point(148, 12);
            this.textBoxThisVersion.Name = "textBoxThisVersion";
            this.textBoxThisVersion.ReadOnly = true;
            this.textBoxThisVersion.Size = new System.Drawing.Size(159, 13);
            this.textBoxThisVersion.TabIndex = 0;
            this.textBoxThisVersion.Text = "Should not see this";
            // 
            // textBoxLatestVersion
            // 
            this.textBoxLatestVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLatestVersion.Location = new System.Drawing.Point(148, 26);
            this.textBoxLatestVersion.Name = "textBoxLatestVersion";
            this.textBoxLatestVersion.ReadOnly = true;
            this.textBoxLatestVersion.Size = new System.Drawing.Size(159, 13);
            this.textBoxLatestVersion.TabIndex = 1;
            this.textBoxLatestVersion.Text = "Should not see this";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "This version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Latest version:";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::Mossywell.BSR.Properties.Resources.about_image_32x32;
            this.pictureBox.Location = new System.Drawing.Point(12, 9);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(40, 40);
            this.pictureBox.TabIndex = 9;
            this.pictureBox.TabStop = false;
            // 
            // textBoxQuestion
            // 
            this.textBoxQuestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxQuestion.Location = new System.Drawing.Point(65, 55);
            this.textBoxQuestion.Multiline = true;
            this.textBoxQuestion.Name = "textBoxQuestion";
            this.textBoxQuestion.ReadOnly = true;
            this.textBoxQuestion.Size = new System.Drawing.Size(337, 37);
            this.textBoxQuestion.TabIndex = 11;
            this.textBoxQuestion.Text = "Should not see this";
            // 
            // buttonYes
            // 
            this.buttonYes.Location = new System.Drawing.Point(120, 98);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(75, 23);
            this.buttonYes.TabIndex = 12;
            this.buttonYes.Text = "Yes";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Location = new System.Drawing.Point(204, 98);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(75, 23);
            this.buttonNo.TabIndex = 13;
            this.buttonNo.Text = "No";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // FormVersion
            // 
            this.AcceptButton = this.buttonNo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 131);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.textBoxQuestion);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLatestVersion);
            this.Controls.Add(this.textBoxThisVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVersion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Should not see this";
            this.Load += new System.EventHandler(this.FormVersion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxThisVersion;
        public System.Windows.Forms.TextBox textBoxLatestVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox textBoxQuestion;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;




    }
}