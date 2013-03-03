namespace Mossywell
{
    namespace BSR
    {
        partial class FormAbout
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
                this.labelVersion = new System.Windows.Forms.Label();
                this.labelExe = new System.Windows.Forms.Label();
                this.labelTitle = new System.Windows.Forms.Label();
                this.labelVersionData = new System.Windows.Forms.Label();
                this.labelExeLocationData = new System.Windows.Forms.Label();
                this.pictureBox = new System.Windows.Forms.PictureBox();
                this.buttonOK = new System.Windows.Forms.Button();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
                this.SuspendLayout();
                // 
                // labelVersion
                // 
                this.labelVersion.AutoSize = true;
                this.labelVersion.Location = new System.Drawing.Point(62, 38);
                this.labelVersion.Name = "labelVersion";
                this.labelVersion.Size = new System.Drawing.Size(45, 13);
                this.labelVersion.TabIndex = 2;
                this.labelVersion.Text = "Version:";
                // 
                // labelExe
                // 
                this.labelExe.AutoSize = true;
                this.labelExe.Location = new System.Drawing.Point(62, 52);
                this.labelExe.Name = "labelExe";
                this.labelExe.Size = new System.Drawing.Size(103, 13);
                this.labelExe.TabIndex = 3;
                this.labelExe.Text = "Executable location:";
                // 
                // labelTitle
                // 
                this.labelTitle.Location = new System.Drawing.Point(62, 12);
                this.labelTitle.Name = "labelTitle";
                this.labelTitle.Size = new System.Drawing.Size(391, 15);
                this.labelTitle.TabIndex = 5;
                this.labelTitle.Text = "Should not see this";
                // 
                // labelVersionData
                // 
                this.labelVersionData.Location = new System.Drawing.Point(171, 38);
                this.labelVersionData.Name = "labelVersionData";
                this.labelVersionData.Size = new System.Drawing.Size(397, 14);
                this.labelVersionData.TabIndex = 6;
                this.labelVersionData.Text = "Should not see this";
                // 
                // labelExeLocationData
                // 
                this.labelExeLocationData.Location = new System.Drawing.Point(171, 52);
                this.labelExeLocationData.Name = "labelExeLocationData";
                this.labelExeLocationData.Size = new System.Drawing.Size(397, 13);
                this.labelExeLocationData.TabIndex = 7;
                this.labelExeLocationData.Text = "Should not see this";
                // 
                // pictureBox
                // 
                this.pictureBox.Image = global::Mossywell.BSR.Properties.Resources.about_image_32x32;
                this.pictureBox.Location = new System.Drawing.Point(12, 9);
                this.pictureBox.Name = "pictureBox";
                this.pictureBox.Size = new System.Drawing.Size(38, 42);
                this.pictureBox.TabIndex = 8;
                this.pictureBox.TabStop = false;
                // 
                // buttonOK
                // 
                this.buttonOK.Location = new System.Drawing.Point(251, 81);
                this.buttonOK.Name = "buttonOK";
                this.buttonOK.Size = new System.Drawing.Size(75, 23);
                this.buttonOK.TabIndex = 9;
                this.buttonOK.Text = "OK";
                this.buttonOK.UseVisualStyleBackColor = true;
                this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
                // 
                // FormAbout
                // 
                this.AcceptButton = this.buttonOK;
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(576, 115);
                this.Controls.Add(this.buttonOK);
                this.Controls.Add(this.pictureBox);
                this.Controls.Add(this.labelExeLocationData);
                this.Controls.Add(this.labelVersionData);
                this.Controls.Add(this.labelTitle);
                this.Controls.Add(this.labelExe);
                this.Controls.Add(this.labelVersion);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "FormAbout";
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "About...";
                this.Load += new System.EventHandler(this.FormAbout_Load);
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label labelVersion;
            private System.Windows.Forms.Label labelExe;
            private System.Windows.Forms.Label labelTitle;
            private System.Windows.Forms.Label labelVersionData;
            private System.Windows.Forms.Label labelExeLocationData;
            private System.Windows.Forms.PictureBox pictureBox;
            private System.Windows.Forms.Button buttonOK;

        }
    }
}