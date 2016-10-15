using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoShutDownForThai_Version1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            lblTimeLeft.Hide();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.ClassStyle = 0x200;
                return parms;
            }
        }

        private int hour;
        private int min;
        private int second;
        private int dem = 0;

        private void btnBegin_Click(object sender, EventArgs e)
        {
            if (numMinute.Value > 0 || numHour.Value > 0)
            {
                hour = (int)numHour.Value;
                min = (int)numMinute.Value;
                timer1.Start();
                btnBegin.Enabled = false;
                numHour.Hide();
                numMinute.Hide();
                lblTimeLeft.Show();
                label1.Text = "Time left!!!";
            }
            else
            {
                MessageBox.Show("Select TIME", "SOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string formatHour(int s)
        {
            string t = s.ToString();
            return s < 10 ? "0" + t : t;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hour > 0 || min > 0 || second > 0)
            {
                if (min == 00 && hour > 0) { min = 60; hour--; }
                if (second == 00 && min > 0) { second = 60; min--; }
                second--;
            }
            numHour.Value = hour;
            numMinute.Value = min;

            lblTimeLeft.Text = string.Format("{0}:{1}:{2}", formatHour(hour), formatHour(min), formatHour(second));

            if (min == 5 && second == 0)
            {
                MessageBox.Show("5 Minute left ( Tat chuong trinh dii)!!", "SOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Show();
                label1.Hide();
                this.MinimizeBox = false;
            }
            else if(min == 1 && second == 0)
            {
                MessageBox.Show("1 Minute left ( Tat chuong trinh dii)!!", "SOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 

            if (hour == 0 && min == 0 && second == 0)
            {
                timer1.Stop();
                ExitWindows.Shutdown(true);    
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            hour = min = second = 0;
            timer1.Stop();
            btnBegin.Enabled = true;
            lblTimeLeft.Text = "00:00:00";
            lblTimeLeft.Hide();
            numHour.Show();
            numMinute.Show();
            label1.Text = "ShutDown";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "1035")
                this.Close();
            else if (textBox1.Text == "1111")
            {
                
                label1.Show();
            }
                
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.Text = "ShutdownPC";
                this.Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show(); //Hiển thị Form
            this.WindowState = FormWindowState.Normal; //Trở về trạng thái bình thường
            notifyIcon1.Visible = false;
            label1.Hide();
        }


        private void FormMain_Click(object sender, EventArgs e)
        {
            dem++;
            if(dem > 2)
            {
                MessageBox.Show("If you click much, we will shutdown soon !!!");
                if(min > 10)
                     min = min - 5;
            }
            
            
        }
    }
}
