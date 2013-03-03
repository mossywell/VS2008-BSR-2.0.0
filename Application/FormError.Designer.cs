namespace Mossywell.BSR
{
    partial class FormError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormError));
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(313, 74);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxError
            // 
            this.textBoxError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxError.Location = new System.Drawing.Point(62, 38);
            this.textBoxError.Multiline = true;
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.ReadOnly = true;
            this.textBoxError.Size = new System.Drawing.Size(636, 26);
            this.textBoxError.TabIndex = 1;
            this.textBoxError.Text = "Should not see this";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLabel.Location = new System.Drawing.Point(62, 19);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.ReadOnly = true;
            this.textBoxLabel.Size = new System.Drawing.Size(636, 13);
            this.textBoxLabel.TabIndex = 3;
            this.textBoxLabel.Text = "Should not see this";
            // 
            // FormError
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(694, 110);
            this.Controls.Add(this.textBoxLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxError);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormError";
            this.ShowIcon = false;
            this.Text = "Should not see this";
            this.Load += new System.EventHandler(this.FormError_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxError;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxLabel;
    }
}