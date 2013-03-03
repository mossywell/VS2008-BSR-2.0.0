namespace Mossywell.BSR
{
    partial class FormUsernamePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUsernamePassword));
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.AcceptsTab = true;
            this.textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUsername.Location = new System.Drawing.Point(123, 12);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.ReadOnly = true;
            this.textBoxUsername.Size = new System.Drawing.Size(119, 13);
            this.textBoxUsername.TabIndex = 0;
            this.textBoxUsername.Text = "Should not see this";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassword.Location = new System.Drawing.Point(123, 26);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.ReadOnly = true;
            this.textBoxPassword.Size = new System.Drawing.Size(119, 13);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.Text = "Should not see this";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
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
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(137, 60);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(105, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(12, 60);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(105, 23);
            this.buttonCopyToClipboard.TabIndex = 12;
            this.buttonCopyToClipboard.Text = "Copy to Clipboard";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboard.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // FormUsernamePassword
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 95);
            this.Controls.Add(this.buttonCopyToClipboard);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUsernamePassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADSL Username and Password...";
            this.Load += new System.EventHandler(this.FormUsernamePassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxUsername;
        public System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCopyToClipboard;




    }
}