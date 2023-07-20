using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ftpdown
{
    public partial class AppForm : Form
    {
        private Core core;

        public AppForm()
        {
            core = new Core();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.downTypeBox.SelectedIndex = AppConfig.downType;
            this.fileTypeBox.SelectedIndex = AppConfig.fileType;
            this.setABEnable();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string str = textBox1.Text;
                if (str.Length > 15)
                {
                    try
                    {
                        core.downFiles(str);
                        checkAB();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("服务器未找到数据");
                    }
                }
                textBox1.Text = "";
            }
        }

        private void checkAB()
        {
            if (AppConfig.fileN == 0) { return; }
            if (AppConfig.fileN == 1)
            {
                buttonA.Text = "A ◎";
                radioButton7.Visible = false;
                AppConfig.fileN = 0;
            }
            else if (AppConfig.fileN == 2)
            {
                buttonA.Text = "A ◎";
                radioButton7.Visible = true;
                radioButton7.Text = "B ◎";
                AppConfig.fileN = 0;
            }

        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked == true)
            {
                AppConfig.openFile = false;
            }
        }

        private void buttonA_CheckedChanged(object sender, EventArgs e)
        {
            if (buttonA.Checked == true)
            {
                AppConfig.openFile = true;
                core.openLastFile(true);
            }

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                AppConfig.openFile = true;
                core.openLastFile(false);
            }

        }

        private void downTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppConfig.switchDownType(downTypeBox.SelectedIndex);
            setABEnable();
        }

        private void fileTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppConfig.switchFileType(fileTypeBox.SelectedIndex);
        }

        private void setABEnable()
        {
            if (AppConfig.downType == 2)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;
            }
        }

        private void fileTypeBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Password password = new Password();
            password.open(fileTypeBox);
        }
    }
}
