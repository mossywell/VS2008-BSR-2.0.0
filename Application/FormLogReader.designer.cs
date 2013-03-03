namespace Mossywell
{
    namespace BSR
    {
        partial class FormLogReader
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
                this.components = new System.ComponentModel.Container();
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogReader));
                this.richTextBox = new System.Windows.Forms.RichTextBox();
                this.timer = new System.Windows.Forms.Timer(this.components);
                this.buttonStop = new System.Windows.Forms.Button();
                this.buttonPlay = new System.Windows.Forms.Button();
                this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
                this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
                this.contextMenuStrip.SuspendLayout();
                this.SuspendLayout();
                // 
                // richTextBox
                // 
                this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.richTextBox.ContextMenuStrip = this.contextMenuStrip;
                this.richTextBox.Location = new System.Drawing.Point(0, 29);
                this.richTextBox.Name = "richTextBox";
                this.richTextBox.Size = new System.Drawing.Size(532, 244);
                this.richTextBox.TabIndex = 0;
                this.richTextBox.Text = "Loading log file...";
                // 
                // timer
                // 
                this.timer.Tick += new System.EventHandler(this.timer_Tick);
                // 
                // buttonStop
                // 
                this.buttonStop.AutoEllipsis = true;
                this.buttonStop.Enabled = false;
                this.buttonStop.FlatAppearance.BorderSize = 0;
                this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.buttonStop.Image = global::Mossywell.BSR.Properties.Resources.stop;
                this.buttonStop.Location = new System.Drawing.Point(24, 3);
                this.buttonStop.Name = "buttonStop";
                this.buttonStop.Size = new System.Drawing.Size(20, 20);
                this.buttonStop.TabIndex = 2;
                this.buttonStop.UseVisualStyleBackColor = true;
                this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
                // 
                // buttonPlay
                // 
                this.buttonPlay.Enabled = false;
                this.buttonPlay.FlatAppearance.BorderSize = 0;
                this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.buttonPlay.Image = global::Mossywell.BSR.Properties.Resources.play;
                this.buttonPlay.Location = new System.Drawing.Point(3, 3);
                this.buttonPlay.Name = "buttonPlay";
                this.buttonPlay.Size = new System.Drawing.Size(20, 20);
                this.buttonPlay.TabIndex = 1;
                this.buttonPlay.UseVisualStyleBackColor = true;
                this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
                // 
                // contextMenuStrip
                // 
                this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy});
                this.contextMenuStrip.Name = "contextMenuStrip";
                this.contextMenuStrip.Size = new System.Drawing.Size(153, 48);
                // 
                // toolStripMenuItemCopy
                // 
                this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
                this.toolStripMenuItemCopy.Size = new System.Drawing.Size(152, 22);
                this.toolStripMenuItemCopy.Text = "Copy";
                this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
                // 
                // FormLogReader
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(532, 273);
                this.Controls.Add(this.buttonStop);
                this.Controls.Add(this.buttonPlay);
                this.Controls.Add(this.richTextBox);
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Name = "FormLogReader";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "BSR Log Viewer";
                this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
                this.Load += new System.EventHandler(this.Form1_Load);
                this.contextMenuStrip.ResumeLayout(false);
                this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.RichTextBox richTextBox;
            private System.Windows.Forms.Timer timer;
            private System.Windows.Forms.Button buttonPlay;
            private System.Windows.Forms.Button buttonStop;
            private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
            private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;

        }
    }
}

