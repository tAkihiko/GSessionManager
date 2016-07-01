using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSessionManager
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.textBox_ID.Text = Properties.Settings.Default.UserID;
            this.textBox_PW.Text = Properties.Settings.Default.PassWord;
            this.textBox_SchNotifyTime.Text = Properties.Settings.Default.ScheduleNotfyTime.ToString();
            this.textBox_SchPopupTime.Text = Properties.Settings.Default.SchedulePopupTime.ToString();
        }

        private void button_Check_Click(object sender, EventArgs e)
        {
            // 接続の確認のみ行う
            bool check = GSessionCtrl.Ctrl.ParamCheck(this.textBox_ID.Text, this.textBox_PW.Text);
            if (check)
            {
                MessageBox.Show("接続できました。");
            }else{
                MessageBox.Show("接続できませんでした。");
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            // 接続
            bool check = GSessionCtrl.Ctrl.ParamSetting(this.textBox_ID.Text, this.textBox_PW.Text);
            if (check)
            {
                Properties.Settings.Default.UserID = this.textBox_ID.Text;
                Properties.Settings.Default.PassWord = this.textBox_PW.Text;
                Properties.Settings.Default.ScheduleNotfyTime = uint.Parse(this.textBox_SchNotifyTime.Text);
                Properties.Settings.Default.SchedulePopupTime = uint.Parse(this.textBox_SchPopupTime.Text);
                Properties.Settings.Default.Save();
                this.Close();
            }
            else
            {
                MessageBox.Show("接続できませんでした。");
            }
        }

        private void textBox_NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0') || ('9' < e.KeyChar))
            {
                if (e.KeyChar != '\b')  // バックスペース
                {
                    // ハンドルを無効に
                    e.Handled = true;
                }
            }
        }
    }
}
