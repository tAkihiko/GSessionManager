﻿namespace GSessionManager
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
            this.textBox_ID.TabIndex = 2;
            // 
            // textBox_PW
            // 
            this.textBox_PW.Location = new System.Drawing.Point(91, 48);
            this.textBox_PW.Name = "textBox_PW";
            this.textBox_PW.PasswordChar = '●';
            this.textBox_PW.Size = new System.Drawing.Size(165, 19);
            this.textBox_PW.TabIndex = 2;
            // 
            // button_Check
            // 
            this.button_Check.Location = new System.Drawing.Point(116, 73);
            this.button_Check.Name = "button_Check";
            this.button_Check.Size = new System.Drawing.Size(75, 23);
            this.button_Check.TabIndex = 3;
            this.button_Check.Text = "接続確認";
            this.button_Check.UseVisualStyleBackColor = true;
            this.button_Check.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(197, 73);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 114);
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
    }
}