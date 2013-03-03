namespace Mossywell.BSR
{
    partial class NotifyIconForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemUpdateWebInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemUpdateStatsNow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisplayCurrentStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisplayDetailedStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisplayUsernamePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStartUtelnetd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemKillUtelnetd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRunCustomCommandsNow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRebootRouter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemMaintenanceMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPause = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisplayLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemViewEditConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemReadme = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconFormLabel = new System.Windows.Forms.Label();
            this.labelMainForm = new System.Windows.Forms.Label();
            this.timerStartupDelay = new System.Windows.Forms.Timer(this.components);
            this.timerStateCheck = new System.Windows.Forms.Timer(this.components);
            this.timerCustomCommandStateCheckDelay = new System.Windows.Forms.Timer(this.components);
            this.timerRebootStateCheckDelay = new System.Windows.Forms.Timer(this.components);
            this.timerDnsResolverCacheFlush = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Loading...";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemUpdateWebInfo,
            this.toolStripSeparator4,
            this.toolStripMenuItemUpdateStatsNow,
            this.toolStripMenuItemDisplayCurrentStats,
            this.toolStripMenuItemDisplayDetailedStats,
            this.toolStripMenuItemDisplayUsernamePassword,
            this.toolStripMenuItemStartUtelnetd,
            this.toolStripMenuItemKillUtelnetd,
            this.toolStripMenuItemRunCustomCommandsNow,
            this.toolStripMenuItemRebootRouter,
            this.toolStripSeparator3,
            this.toolStripMenuItemMaintenanceMode,
            this.toolStripMenuItemPause,
            this.toolStripMenuItemDisplayLog,
            this.toolStripMenuItemViewEditConfigFile,
            this.toolStripSeparator2,
            this.toolStripMenuItemReadme,
            this.toolStripMenuItemHelp,
            this.toolStripMenuItemAbout,
            this.toolStripSeparator1,
            this.toolStripMenuItemExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(261, 402);
            // 
            // toolStripMenuItemUpdateWebInfo
            // 
            this.toolStripMenuItemUpdateWebInfo.Name = "toolStripMenuItemUpdateWebInfo";
            this.toolStripMenuItemUpdateWebInfo.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemUpdateWebInfo.Text = "Update Web Info Now";
            this.toolStripMenuItemUpdateWebInfo.Click += new System.EventHandler(this.toolStripMenuItemUpdateWebInfo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(257, 6);
            // 
            // toolStripMenuItemUpdateStatsNow
            // 
            this.toolStripMenuItemUpdateStatsNow.Name = "toolStripMenuItemUpdateStatsNow";
            this.toolStripMenuItemUpdateStatsNow.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemUpdateStatsNow.Text = "Update Stats Now";
            this.toolStripMenuItemUpdateStatsNow.Click += new System.EventHandler(this.toolStripMenuItemUpdateStatsNow_Click);
            // 
            // toolStripMenuItemDisplayCurrentStats
            // 
            this.toolStripMenuItemDisplayCurrentStats.Name = "toolStripMenuItemDisplayCurrentStats";
            this.toolStripMenuItemDisplayCurrentStats.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemDisplayCurrentStats.Text = "Display Current Stats";
            this.toolStripMenuItemDisplayCurrentStats.Click += new System.EventHandler(this.toolStripMenuItemDisplayCurrentStats_Click);
            // 
            // toolStripMenuItemDisplayDetailedStats
            // 
            this.toolStripMenuItemDisplayDetailedStats.Name = "toolStripMenuItemDisplayDetailedStats";
            this.toolStripMenuItemDisplayDetailedStats.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemDisplayDetailedStats.Text = "Display Detailed Stats";
            this.toolStripMenuItemDisplayDetailedStats.Click += new System.EventHandler(this.toolStripMenuItemDisplayDetailedStats_Click);
            // 
            // toolStripMenuItemDisplayUsernamePassword
            // 
            this.toolStripMenuItemDisplayUsernamePassword.Name = "toolStripMenuItemDisplayUsernamePassword";
            this.toolStripMenuItemDisplayUsernamePassword.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemDisplayUsernamePassword.Text = "Display ADSL Username and Password";
            this.toolStripMenuItemDisplayUsernamePassword.Click += new System.EventHandler(this.toolStripMenuItemDisplayUsernamePassword_Click);
            // 
            // toolStripMenuItemStartUtelnetd
            // 
            this.toolStripMenuItemStartUtelnetd.Name = "toolStripMenuItemStartUtelnetd";
            this.toolStripMenuItemStartUtelnetd.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemStartUtelnetd.Text = "Start Telnet Daemon";
            this.toolStripMenuItemStartUtelnetd.Click += new System.EventHandler(this.toolStripMenuItemStartUtelnetd_Click);
            // 
            // toolStripMenuItemKillUtelnetd
            // 
            this.toolStripMenuItemKillUtelnetd.Name = "toolStripMenuItemKillUtelnetd";
            this.toolStripMenuItemKillUtelnetd.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemKillUtelnetd.Text = "Kill Telnet Daemon";
            this.toolStripMenuItemKillUtelnetd.Click += new System.EventHandler(this.toolStripMenuItemKillUtelnetd_Click);
            // 
            // toolStripMenuItemRunCustomCommandsNow
            // 
            this.toolStripMenuItemRunCustomCommandsNow.Name = "toolStripMenuItemRunCustomCommandsNow";
            this.toolStripMenuItemRunCustomCommandsNow.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemRunCustomCommandsNow.Text = "Run Custom Commands Now";
            this.toolStripMenuItemRunCustomCommandsNow.Click += new System.EventHandler(this.toolStripMenuItemRunCustomCommandsNow_Click);
            // 
            // toolStripMenuItemRebootRouter
            // 
            this.toolStripMenuItemRebootRouter.Name = "toolStripMenuItemRebootRouter";
            this.toolStripMenuItemRebootRouter.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemRebootRouter.Text = "Reboot Router Now";
            this.toolStripMenuItemRebootRouter.Click += new System.EventHandler(this.toolStripMenuItemRebootRouter_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(257, 6);
            // 
            // toolStripMenuItemMaintenanceMode
            // 
            this.toolStripMenuItemMaintenanceMode.Name = "toolStripMenuItemMaintenanceMode";
            this.toolStripMenuItemMaintenanceMode.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemMaintenanceMode.Text = "Enter / Leave Maintenance Mode";
            this.toolStripMenuItemMaintenanceMode.Click += new System.EventHandler(this.toolStripMenuItemMaintenanceMode_Click);
            // 
            // toolStripMenuItemPause
            // 
            this.toolStripMenuItemPause.Name = "toolStripMenuItemPause";
            this.toolStripMenuItemPause.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemPause.Text = "Pause / Re-start BSR";
            this.toolStripMenuItemPause.Click += new System.EventHandler(this.toolStripMenuItemPause_Click);
            // 
            // toolStripMenuItemDisplayLog
            // 
            this.toolStripMenuItemDisplayLog.Name = "toolStripMenuItemDisplayLog";
            this.toolStripMenuItemDisplayLog.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemDisplayLog.Text = "Display Log in Log Viewer";
            this.toolStripMenuItemDisplayLog.Click += new System.EventHandler(this.toolStripMenuItemDisplayLog_Click);
            // 
            // toolStripMenuItemViewEditConfigFile
            // 
            this.toolStripMenuItemViewEditConfigFile.Name = "toolStripMenuItemViewEditConfigFile";
            this.toolStripMenuItemViewEditConfigFile.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemViewEditConfigFile.Text = "View / Edit Config File";
            this.toolStripMenuItemViewEditConfigFile.Click += new System.EventHandler(this.toolStripMenuItemViewEditConfigFile_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(257, 6);
            // 
            // toolStripMenuItemReadme
            // 
            this.toolStripMenuItemReadme.Name = "toolStripMenuItemReadme";
            this.toolStripMenuItemReadme.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemReadme.Text = "Display Readme";
            this.toolStripMenuItemReadme.Click += new System.EventHandler(this.toolStripMenuItemReadme_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemHelp.Text = "Help";
            this.toolStripMenuItemHelp.Click += new System.EventHandler(this.toolStripMenuItemHelp_Click);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemAbout.Text = "About...";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(257, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemClose_Click);
            // 
            // NotifyIconFormLabel
            // 
            this.NotifyIconFormLabel.AutoSize = true;
            this.NotifyIconFormLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotifyIconFormLabel.Location = new System.Drawing.Point(38, 10);
            this.NotifyIconFormLabel.Name = "NotifyIconFormLabel";
            this.NotifyIconFormLabel.Size = new System.Drawing.Size(129, 17);
            this.NotifyIconFormLabel.TabIndex = 1;
            this.NotifyIconFormLabel.Text = "Should not see this";
            // 
            // labelMainForm
            // 
            this.labelMainForm.AutoSize = true;
            this.labelMainForm.Location = new System.Drawing.Point(98, 9);
            this.labelMainForm.Name = "labelMainForm";
            this.labelMainForm.Size = new System.Drawing.Size(97, 13);
            this.labelMainForm.TabIndex = 1;
            this.labelMainForm.Text = "Should not see this";
            // 
            // timerStartupDelay
            // 
            this.timerStartupDelay.Tick += new System.EventHandler(this.timerStartupDelay_Tick);
            // 
            // timerStateCheck
            // 
            this.timerStateCheck.Tick += new System.EventHandler(this.timerStateCheck_Tick);
            // 
            // timerCustomCommandStateCheckDelay
            // 
            this.timerCustomCommandStateCheckDelay.Tick += new System.EventHandler(this.timerCustomCommandStateCheckDelay_Tick);
            // 
            // timerRebootStateCheckDelay
            // 
            this.timerRebootStateCheckDelay.Tick += new System.EventHandler(this.timerRebootStateCheckDelay_Tick);
            // 
            // timerDnsResolverCacheFlush
            // 
            this.timerDnsResolverCacheFlush.Tick += new System.EventHandler(this.timerDnsResolverCacheFlush_Tick);
            // 
            // NotifyIconForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 30);
            this.Controls.Add(this.labelMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NotifyIconForm";
            this.ShowInTaskbar = false;
            this.Text = "BSRNotifyIconForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotifyIconForm_FormClosing);
            this.Load += new System.EventHandler(this.NotifyIconForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.Label NotifyIconFormLabel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisplayLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRebootRouter;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartUtelnetd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKillUtelnetd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisplayCurrentStats;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUpdateStatsNow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUpdateWebInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemViewEditConfigFile;
        private System.Windows.Forms.Label labelMainForm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisplayUsernamePassword;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRunCustomCommandsNow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisplayDetailedStats;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPause;
        private System.Windows.Forms.Timer timerStartupDelay;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReadme;
        private System.Windows.Forms.Timer timerStateCheck;
        private System.Windows.Forms.Timer timerCustomCommandStateCheckDelay;
        private System.Windows.Forms.Timer timerRebootStateCheckDelay;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMaintenanceMode;
        private System.Windows.Forms.Timer timerDnsResolverCacheFlush;
    }
}

