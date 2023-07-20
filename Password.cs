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
    public partial class Password : Form
    {
        private int type = 0;

        private ComboBox box;

        public Password()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals("abc.123456"))
                {
                    box.Enabled = true;
                }
                else
                {
                    MessageBox.Show("密码错误");
                }
                this.Dispose();
            }
        }

        internal void open(ComboBox fileTypeBox)
        {
            this.box = fileTypeBox;
            this.ShowDialog(fileTypeBox);
        }
    }
}
