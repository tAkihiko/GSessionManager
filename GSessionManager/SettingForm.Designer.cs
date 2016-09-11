namespace GSessionManager
{
    partial class SettingForm
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
            this.label_ID = new System.Windows.Forms.Label();
            this.label_PW = new System.Windows.Forms.Label();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.textBox_PW = new System.Windows.Forms.TextBox();
            this.button_Check = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.label_SchNotifyTime = new System.Windows.Forms.Label();
            this.label_SchPopupTime = new System.Windows.Forms.Label();
            this.textBox_SchPopupTime = new System.Windows.Forms.TextBox();
            this.textBox_SchNotifyTime = new System.Windows.Forms.TextBox();
            this.checkBox_AutoChangeZaiseki = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(69, 22);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(16, 12);
            this.label_ID.TabIndex = 0;
            this.label_ID.Text = "ID";
            // 
            // label_PW
            // 
            this.label_PW.AutoSize = true;
            this.label_PW.Location = new System.Drawing.Point(30, 51);
            this.label_PW.Name = "label_PW";
            this.label_PW.Size = new System.Drawing.Size(55, 12);
            this.label_PW.TabIndex = 0;
            this.label_PW.Text = "PassWord";
            // 
            // textBox_ID
            // 
            this.textBox_ID.Location = new System.Drawing.Point(91, 19);
            this.textBox_ID.Name = "textBox_ID";
            this.textBox_ID.Size = new System.Drawing.Size(165, 19);
            this.textBox_ID.TabIndex = 0;
            // 
            // textBox_PW
            // 
            this.textBox_PW.Location = new System.Drawing.Point(91, 48);
            this.textBox_PW.Name = "textBox_PW";
            this.textBox_PW.PasswordChar = '●';
            this.textBox_PW.Size = new System.Drawing.Size(165, 19);
            this.textBox_PW.TabIndex = 1;
            // 
            // button_Check
            // 
            this.button_Check.Location = new System.Drawing.Point(181, 73);
            this.button_Check.Name = "button_Check";
            this.button_Check.Size = new System.Drawing.Size(75, 23);
            this.button_Check.TabIndex = 2;
            this.button_Check.Text = "接続確認";
            this.button_Check.UseVisualStyleBackColor = true;
            this.button_Check.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(197, 258);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // label_SchNotifyTime
            // 
            this.label_SchNotifyTime.AutoSize = true;
            this.label_SchNotifyTime.Location = new System.Drawing.Point(29, 144);
            this.label_SchNotifyTime.Name = "label_SchNotifyTime";
            this.label_SchNotifyTime.Size = new System.Drawing.Size(68, 12);
            this.label_SchNotifyTime.TabIndex = 4;
            this.label_SchNotifyTime.Text = "バルーン通知";
            // 
            // label_SchPopupTime
            // 
            this.label_SchPopupTime.AutoSize = true;
            this.label_SchPopupTime.Location = new System.Drawing.Point(17, 118);
            this.label_SchPopupTime.Name = "label_SchPopupTime";
            this.label_SchPopupTime.Size = new System.Drawing.Size(80, 12);
            this.label_SchPopupTime.TabIndex = 4;
            this.label_SchPopupTime.Text = "ポップアップ通知";
            // 
            // textBox_SchPopupTime
            // 
            this.textBox_SchPopupTime.Location = new System.Drawing.Point(103, 115);
            this.textBox_SchPopupTime.Name = "textBox_SchPopupTime";
            this.textBox_SchPopupTime.Size = new System.Drawing.Size(153, 19);
            this.textBox_SchPopupTime.TabIndex = 3;
            this.textBox_SchPopupTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_NumberOnly_KeyPress);
            // 
            // textBox_SchNotifyTime
            // 
            this.textBox_SchNotifyTime.Location = new System.Drawing.Point(103, 141);
            this.textBox_SchNotifyTime.Name = "textBox_SchNotifyTime";
            this.textBox_SchNotifyTime.Size = new System.Drawing.Size(153, 19);
            this.textBox_SchNotifyTime.TabIndex = 4;
            this.textBox_SchNotifyTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_NumberOnly_KeyPress);
            // 
            // checkBox_AutoChangeZaiseki
            // 
            this.checkBox_AutoChangeZaiseki.AutoSize = true;
            this.checkBox_AutoChangeZaiseki.Location = new System.Drawing.Point(19, 173);
            this.checkBox_AutoChangeZaiseki.Name = "checkBox_AutoChangeZaiseki";
            this.checkBox_AutoChangeZaiseki.Size = new System.Drawing.Size(135, 16);
            this.checkBox_AutoChangeZaiseki.TabIndex = 6;
            this.checkBox_AutoChangeZaiseki.Text = "在席・不在を自動操作";
            this.checkBox_AutoChangeZaiseki.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 293);
            this.Controls.Add(this.checkBox_AutoChangeZaiseki);
            this.Controls.Add(this.textBox_SchNotifyTime);
            this.Controls.Add(this.textBox_SchPopupTime);
            this.Controls.Add(this.label_SchPopupTime);
            this.Controls.Add(this.label_SchNotifyTime);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Check);
            this.Controls.Add(this.textBox_PW);
            this.Controls.Add(this.textBox_ID);
            this.Controls.Add(this.label_PW);
            this.Controls.Add(this.label_ID);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label_PW;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.TextBox textBox_PW;
        private System.Windows.Forms.Button button_Check;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Label label_SchNotifyTime;
        private System.Windows.Forms.Label label_SchPopupTime;
        private System.Windows.Forms.TextBox textBox_SchPopupTime;
        private System.Windows.Forms.TextBox textBox_SchNotifyTime;
        private System.Windows.Forms.CheckBox checkBox_AutoChangeZaiseki;
    }
}