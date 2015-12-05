using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GSessionManager
{
    public partial class Manager : Form
    {
        // Window Message
        const int WM_QUERYENDSESSION = 0x0011;
        const int WM_ENDSESSION = 0x0016;

        // ShutdownBlock
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        extern static bool ShutdownBlockReasonCreate(IntPtr hWnd, string str);
        [DllImport("user32.dll")]
        extern static bool ShutdownBlockReasonDestroy(IntPtr hWnd);

        /// <summary>
        /// 在席・不在フラグ
        /// </summary>
        bool StayFlg = false;

        /// <summary>
        /// スケジュール取得ロック
        /// </summary>
        object LockGettingSchedule = null;

        /// <summary>
        /// スケジュール通知前時間 [ミリ秒]
        /// </summary>
        int ScheduleNotfyTime = 60000;

        /// <summary>
        /// スケジュールノード
        /// </summary>
        private class ScheduleNode : GSessionCtrl.Ctrl.ScheduleNode
        {
            /// <summary>
            /// 表示済みフラグ
            /// </summary>
            public bool Viewed { get; set; }

            public ScheduleNode(string name, DateTime begin, DateTime end, string title, string text)
                : base( name, begin, end, title, text )
            {
                Viewed = false;
            }

            public ScheduleNode(GSessionCtrl.Ctrl.ScheduleNode node)
                : this(node.Name, node.Begin, node.End, node.Title, node.Text) { }
        }

        /// <summary>
        /// スケジュールリスト
        /// </summary>
        List<ScheduleNode> SchList = null;

        /// <summary>
        /// ポップアップに表示するテキスト（主題）
        /// </summary>
        private string PopupScheduleTitle;

        /// <summary>
        /// ポップアップに表示するテキスト（詳細）
        /// </summary>
        private string PopupScheduleText;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Manager()
        {
            InitializeComponent();

            ScheduleNotfyTime = 60000;
            StayFlg = false;
            SchList = new List<ScheduleNode>();
            LockGettingSchedule = new object();
            PopupScheduleTitle = "";
            PopupScheduleText = "";
        }

        /// <summary>
        /// アプリケーション開始
        /// </summary>
        /// <returns>true: 初期化成功, false: 初期化失敗</returns>
        public bool Start()
        {

            // ID・PassWord設定
            GSessionCtrl.Ctrl.ParamSetting(Properties.Settings.Default.UserID, Properties.Settings.Default.PassWord);

            System.Threading.Thread th = new System.Threading.Thread(() =>
            {

                // 最初は必ず在席状態にする。
                try
                {
                    GSessionCtrl.Ctrl.Zaiseki();
                    StayFlg = true;
                }
                finally
                {
                }

                try
                {
                    // スケジュール取得
                    GetSchedule();
                }
                finally
                {
                }


            });

            th.Start();

            // タイマ起動
            this.ScheduleGetTimer.Start();
            this.ScheduleCheckTimer.Start();

            // ポップアップ登録
            this.notifyIcon1.BalloonTipClicked += new EventHandler((_sender, _e) => { PopupSchedule(); });

            return true;
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        /// <summary>
        /// 在席化
        /// </summary>
        private void Zaiseki()
        {
            if (!StayFlg)
            {
                GSessionCtrl.Ctrl.Zaiseki();
                StayFlg = true;
            }
        }

        /// <summary>
        /// 不在化
        /// </summary>
        private void Huzai()
        {
            if (StayFlg)
            {
                GSessionCtrl.Ctrl.Huzai();
                StayFlg = false;
            }
        }

        /// <summary>
        /// スケジュール情報取得
        /// </summary>
        private void GetSchedule()
        {
            lock (LockGettingSchedule)
            {
                List<GSessionCtrl.Ctrl.ScheduleNode> schlist = GSessionCtrl.Ctrl.Schedule();

                if (schlist == null)
                {
                    return;
                }

                SchList.Clear();

                foreach (GSessionCtrl.Ctrl.ScheduleNode sch in schlist)
                {
                    SchList.Add(new ScheduleNode(sch));
                }
            }
        }

        /// <summary>
        /// ウィンドウプロシージャ
        /// </summary>
        /// <param name="m">ウィンドウメッセージ</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_QUERYENDSESSION:
                case WM_ENDSESSION:
                    if (StayFlg)
                    {
                        try
                        {
                            ShutdownBlockReasonCreate(this.Handle, "不在にしています。");
                            Huzai();
                        }
                        finally
                        {
                            ShutdownBlockReasonDestroy(this.Handle);
                        }
                    }
                    break;

                default:
                    break;
            }

            base.WndProc(ref m);
        }

        private void 在席ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zaiseki();
        }

        private void 不在ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Huzai();
        }

        private void GetScheduleTimer_Tick(object sender, EventArgs e)
        {
            GetSchedule();
        }

        /// <summary>
        /// スケジュールのポップアップ
        /// </summary>
        private void PopupSchedule()
        {
            using (Form topmostform = new Form())
            {
                string text;

                text = PopupScheduleTitle;

                if (PopupScheduleText.Length > 0)
                {
                    text += "\n\n" + PopupScheduleText;
                }

                topmostform.TopMost = true;
                MessageBox.Show(topmostform, text, "スケジュール");
                topmostform.TopMost = false;
            }
        }

        /// <summary>
        /// スケジュールポップアップに登録する
        /// </summary>
        /// <param name="sch">スケジュールノード</param>
        private void PopupScheduleRegister(ScheduleNode sch)
        {
            PopupScheduleRegister(sch.Title, sch.Text);
        }

        /// <summary>
        /// スケジュールポップアップに登録する
        /// </summary>
        /// <param name="title">主題</param>
        /// <param name="text">詳細</param>
        private void PopupScheduleRegister(string title, string text)
        {
            PopupScheduleTitle = title;
            PopupScheduleText = text;
        }

        private void ScheduleCheckTimer_Tick(object sender, EventArgs e)
        {
            if (SchList == null)
            {
                return;
            }

            if (System.Threading.Monitor.TryEnter(LockGettingSchedule, this.ScheduleCheckTimer.Interval))
            {
                if (SchList.Count <= 0)
                {
                    System.Threading.Monitor.Exit(LockGettingSchedule);
                    return;
                }

                try
                {
                    TimeSpan ts;
                    foreach (ScheduleNode sch in SchList)
                    {
                        ts = sch.Begin - DateTime.Now;

                        // ポップアップ通知（時間目前）
                        if (Math.Abs(ts.TotalMilliseconds) <= this.ScheduleCheckTimer.Interval && !sch.Viewed)
                        {
                            sch.Viewed = true;
                            PopupScheduleRegister(sch);
                            PopupSchedule();
                        }

                        // バルーンで通知（ScheduleNotfyTimeミリ秒前）
                        if (ScheduleNotfyTime <= ts.TotalMilliseconds && ts.TotalMilliseconds < (ScheduleNotfyTime + this.ScheduleCheckTimer.Interval))
                        {
                            string title = sch.Title;
                            string text = sch.Text;

                            this.notifyIcon1.BalloonTipText = title;
                            this.notifyIcon1.BalloonTipTitle = "スケジュール";
                                                        
                            PopupScheduleRegister(title, text);

                            this.notifyIcon1.ShowBalloonTip(30000);
                        }
                    }
                }
                finally
                {
                    System.Threading.Monitor.Exit(LockGettingSchedule);
                }
                
            }
        }

        private void スケジュール取得ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetSchedule();
        }
    }
}
