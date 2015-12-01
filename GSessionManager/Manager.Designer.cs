namespace GSessionManager
{
    partial class Manager
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manager));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.在席管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在席ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.不在ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.スケジュール取得ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleGetTimer = new System.Windows.Forms.Timer(this.components);
            this.ScheduleCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "GSessionManager";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在席管理ToolStripMenuItem,
            this.スケジュール取得ToolStripMenuItem,
            this.toolStripSeparator1,
            this.終了ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 76);
            // 
            // 在席管理ToolStripMenuItem
            // 
            this.在席管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在席ToolStripMenuItem,
            this.不在ToolStripMenuItem});
            this.在席管理ToolStripMenuItem.Name = "在席管理ToolStripMenuItem";
            this.在席管理ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.在席管理ToolStripMenuItem.Text = "在席管理";
            // 
            // 在席ToolStripMenuItem
            // 
            this.在席ToolStripMenuItem.Name = "在席ToolStripMenuItem";
            this.在席ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.在席ToolStripMenuItem.Text = "在席";
            this.在席ToolStripMenuItem.Click += new System.EventHandler(this.在席ToolStripMenuItem_Click);
            // 
            // 不在ToolStripMenuItem
            // 
            this.不在ToolStripMenuItem.Name = "不在ToolStripMenuItem";
            this.不在ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.不在ToolStripMenuItem.Text = "不在";
            this.不在ToolStripMenuItem.Click += new System.EventHandler(this.不在ToolStripMenuItem_Click);
            // 
            // スケジュール取得ToolStripMenuItem
            // 
            this.スケジュール取得ToolStripMenuItem.Name = "スケジュール取得ToolStripMenuItem";
            this.スケジュール取得ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.スケジュール取得ToolStripMenuItem.Text = "スケジュール取得";
            this.スケジュール取得ToolStripMenuItem.Click += new System.EventHandler(this.スケジュール取得ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // ScheduleGetTimer
            // 
            this.ScheduleGetTimer.Interval = 1800000;
            this.ScheduleGetTimer.Tick += new System.EventHandler(this.GetScheduleTimer_Tick);
            // 
            // ScheduleCheckTimer
            // 
            this.ScheduleCheckTimer.Interval = 1000;
            this.ScheduleCheckTimer.Tick += new System.EventHandler(this.ScheduleCheckTimer_Tick);
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Manager";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 在席管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在席ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 不在ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.Timer ScheduleGetTimer;
        private System.Windows.Forms.Timer ScheduleCheckTimer;
        private System.Windows.Forms.ToolStripMenuItem スケジュール取得ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

