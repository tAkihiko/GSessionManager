using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        /// GSessionCtrl.dllの必要バージョン
        /// </summary>
        static System.Version RequireCtrlVersion = new Version("2.2.0.0");

        /// <summary>
        /// 在席・不在フラグ
        /// </summary>
        bool StayFlg = false;

        /// <summary>
        /// スケジュール取得ロック
        /// </summary>
        object LockGettingSchedule = null;

        /// <summary>
        /// スケジュールポップアップ通知前時間 [ミリ秒]
        /// </summary>
        uint SchedulePopupTime = 0;

        /// <summary>
        /// スケジュールバルーン通知前時間 [ミリ秒]
        /// </summary>
        uint ScheduleNotfyTime = 60000;

        /// <summary>
        /// スケジュールノード
        /// </summary>
        private class ScheduleNode : GSessionCtrl.Ctrl.ScheduleNode
        {
            /// <summary>
            /// 表示済みフラグ
            /// </summary>
            public bool Viewed { get; set; }

            public ScheduleNode(ulong id, string name, DateTime begin, DateTime end, string title, string text)
                : base( id, name, begin, end, title, text )
            {
                Viewed = false;
            }

            public ScheduleNode(GSessionCtrl.Ctrl.ScheduleNode node)
                : this(node.Id, node.Name, node.Begin, node.End, node.Title, node.Text) { }
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

            ScheduleNotfyTime = Properties.Settings.Default.ScheduleNotfyTime * 1000;
            SchedulePopupTime = Properties.Settings.Default.SchedulePopupTime * 1000;
            StayFlg = false;
            SchList = new List<ScheduleNode>();
            LockGettingSchedule = new object();
            PopupScheduleTitle = "";
            PopupScheduleText = "";
        }

        /// <summary>
        /// 設定Formを表示
        /// </summary>
        public void ShowSettingForm()
        {
            SettingForm set = new SettingForm();
            set.ShowDialog();
            ScheduleNotfyTime = Properties.Settings.Default.ScheduleNotfyTime * 1000;
            SchedulePopupTime = Properties.Settings.Default.SchedulePopupTime * 1000;
            return;
        }

        /// <summary>
        /// アプリケーション開始
        /// </summary>
        /// <returns>true: 初期化成功, false: 初期化失敗</returns>
        public bool Start()
        {
            // GSessionCtrl.dllのバージョン確認
            System.Version ctrlver = GSessionCtrl.Ctrl.GetVersion();
            if (ctrlver < RequireCtrlVersion)
            {
                MessageBox.Show(String.Format("GSessionCtrl.dllのバージョンが古いため実行できません。\nVersion {0} （以上）が必要です。", RequireCtrlVersion));
                notifyIcon1.Visible = false;
                return false;
            }


            // ID・PassWord設定
            bool init = GSessionCtrl.Ctrl.ParamSetting(Properties.Settings.Default.UserID, Properties.Settings.Default.PassWord);
            if (!init)
            {
                ShowSettingForm();
                bool check = GSessionCtrl.Ctrl.ParamSetting(Properties.Settings.Default.UserID, Properties.Settings.Default.PassWord);
                if (!check)
                {

                    MessageBox.Show("接続できませんでした。");
                    notifyIcon1.Visible = false;
                    return false;
                }
            }

            System.Threading.Thread th = new System.Threading.Thread(() =>
            {

                // 最初は必ず在席状態にする。
                try
                {
                    StayFlg = false;
                    Zaiseki();
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
            if (!StayFlg && Properties.Settings.Default.AutoChangeZaiseki)
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
            if (StayFlg && Properties.Settings.Default.AutoChangeZaiseki)
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
            Monitor.Enter(LockGettingSchedule);
            try
            {
                List<GSessionCtrl.Ctrl.ScheduleNode> schlist = GSessionCtrl.Ctrl.Schedule();

                if (schlist == null)
                {
                    Monitor.Exit(LockGettingSchedule);
                    return;
                }

                TimeSpan ts;
                List<ulong> idlist = new List<ulong>();
                int idx;
                foreach (GSessionCtrl.Ctrl.ScheduleNode sch in schlist)
                {
                    ts = sch.Begin - DateTime.Now;
                    if (0 < ts.Milliseconds)
                    {
                        // スケジュールIDのリスト
                        idlist.Add(sch.Id);

                        idx = SchList.FindIndex(node => node.Id == sch.Id);
                        if (idx < 0)
                        {
                            // 新規追加・更新でIDに変更があれば追加する
                            SchList.Add(new ScheduleNode(sch));
                        }

                    }
                }

                // 存在しないスケジュールは既に見たことにする。
                for (int i = 0; i < SchList.Count(); i++)
			    {
                    idx = idlist.FindIndex(node => node == SchList[i].Id);
                    if (idx < 0)
                    {
                        SchList[i].Viewed = true;
                    }
			    }
            }
            finally
            {
                Monitor.Exit(LockGettingSchedule);
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
                    if (StayFlg && Properties.Settings.Default.AutoChangeZaiseki)
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
        /// <param name="sch">スケジュールノード</param>
        private void PopupSchedule(ScheduleNode sch)
        {
            PopupSchedule(sch.Title, sch.Text);
        }

        /// <summary>
        /// スケジュールのポップアップ
        /// </summary>
        private void PopupSchedule()
        {
            PopupSchedule(PopupScheduleTitle, PopupScheduleText);
        }

        /// <summary>
        /// スケジュールのポップアップ
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="detail">詳細</param>
        private void PopupSchedule(string title, string detail)
        {
            // マルチスレッド化すると TopMost プロパティが通用しなくなるため、無効化
            //System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    using (Form topmostform = new Form())
                    {
                        string text;

                        text = title;

                        if (detail.Length > 0)
                        {
                            text += "\r\n\r\n" + detail.Replace("<BR>", "\r\n");
                        }

                        topmostform.TopMost = true;
                        MessageBox.Show(topmostform, text, "スケジュール");
                        topmostform.TopMost = false;
                    }
                }
            //);
            //th.Start();
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
                    foreach (int i in Enumerable.Range(0, SchList.Count()))
                    {
                        ScheduleNode sch = SchList[i];
                        ts = sch.Begin - DateTime.Now;

                        // ポップアップ通知（SchedulePopupTimeミリ秒前）
                        if (ts.TotalMilliseconds <= (SchedulePopupTime + this.ScheduleCheckTimer.Interval) && !sch.Viewed)
                        {
                            SchList[i].Viewed = true;
                            PopupSchedule(sch);
                        }

                        // バルーンで通知（ScheduleNotfyTimeミリ秒前）
                        if (ScheduleNotfyTime <= ts.TotalMilliseconds && ts.TotalMilliseconds < (ScheduleNotfyTime + this.ScheduleCheckTimer.Interval) && !sch.Viewed)
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

        /// <summary>
        /// 設定ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingForm();
        }
    }
}
