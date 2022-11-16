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
    public partial class Year_Closing_Tool : Form
    {
        public Year_Closing_Tool()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        private void btnOpenTool_Click(object sender, EventArgs e)
        {
            string upass = "MMCDeepak@12345";
            if (txtuPass.Text == ""||txtuPass.Text!=upass.Trim())
            {
                MessageBox.Show("Please Enter correct Password to access the Year closing Tool.", "Year Closing Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtuPass.Text==upass.Trim())
            {
                Yearclosing yclosing = new Yearclosing();
                yclosing.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Enter correct Password to access the Year closing Tool.","Year Closing Info",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
