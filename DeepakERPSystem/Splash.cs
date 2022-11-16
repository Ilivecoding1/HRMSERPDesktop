using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepakERPSystem
{
    public partial class Splash : Form
    {
        public enum ProgressBarDisplayText
        {
            Percentage,
            CustomText
        }
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    timer1.Enabled = true;
            //    timer1.Start();
            //    timer1.Interval = 1000;
            //    progressBar1.Maximum = 10;
            //    timer1.Tick += new EventHandler(timer1_Tick);

               
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.ToString());
            //}
           
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {


            int percent = (int)(((double)(progressBar1.Value - progressBar1.Minimum) /
(double)(progressBar1.Maximum - progressBar1.Minimum)) * 100);
            using (Graphics gr = progressBar1.CreateGraphics())
            {
               
               gr.DrawString(percent.ToString() + "%",
                    SystemFonts.DefaultFont,
                    Brushes.Black,
                    new PointF(progressBar1.Width / 2 - (gr.MeasureString(percent.ToString() + "%",
                        SystemFonts.DefaultFont).Width / 2.0F),
                    progressBar1.Height / 2 - (gr.MeasureString(percent.ToString() + "%",
                        SystemFonts.DefaultFont).Height / 2.0F)));

                progressBar1.Increment(1);
                if (progressBar1.Value == 100)
                    timer1.Stop();

            }

            //progressBar1.Increment(1);
            //if (progressBar1.Value == 100)
            //    timer1.Stop();

            //try
            //{
            //   if(progressBar1.Value != 10)
            //    {
            //        progressBar1.Value++;
            //    }

            //    else
            //    {                   
            //        timer1.Stop();
            //        this.Hide();
            //        Login_Page login = new Login_Page();
            //        login.Show();
            //    }
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.ToString());
            //}

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
