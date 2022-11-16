using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace DeepakERPSystem
{
    public partial class Yearclosing : Form
    {
        //BackgroundWorker bgw = new BackgroundWorker();

        SqlConnection conmmc = SqlconnMain.GetMMCDBConnection();
        SqlConnection conmaster = SQLConnMethod2.GetMasterConDB();

        //SqlConnection conn1 = SqlconnPayroll.GetCurrentPayrollDBConnection();
        //SqlConnection conn2 = SqlconnPayrollbackdb.GetBackYrPayrollDBConnection();

        public Yearclosing()
        {
            InitializeComponent();
        }

        private void btnYearclose_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || cboSelectCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Please enter correct servername,userid,password,and select a company then click year close button.", "Year Closing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                //bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                //bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
                //bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
                //bgw.WorkerReportsProgress = true;
                //bgw.RunWorkerAsync();
                //lblYclosestatus.Show();
                yclose();
               
            }
        }
      
        private void Yearclosing_Load(object sender, EventArgs e)
        {
            textBox1.Text = SqlconnMain.servername;
            textBox2.Text = SqlconnMain.username;
            textBox3.Text = SqlconnMain.password;
            textBox4.Text = "";
            lblYclosestatus.Visible = false;
           
            getcompanylist();
            getfinyear();
        }
        private void getfinyear()
        {
            
            for (int i = 2004; i < 2030; i++)
            {
                int k = i + 1;
                string l = i + "-" + k;
                cboFinYear.Items.Add(l);

            }
            string path = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\\year.dll");
            System.IO.StreamReader txtfile = new System.IO.StreamReader(path);
            int line = int.Parse(txtfile.ReadLine());
            cboFinYear.SelectedIndex = line;
            cboFinYear.Enabled = false;


        }
        private void getcompanylist()
        {
           
            DataRow dr;
            if (conmmc.State == ConnectionState.Open)
                conmmc.Close();
            conmmc.Open();
            SqlCommand cmd = new SqlCommand("select cmp_code,cmp_name from cmpm order by cmp_code", conmmc);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@utype", cboSelectCompany.SelectedIndex);
            
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            cboSelectCompany.DisplayMember = "cmp_name";
            cboSelectCompany.ValueMember = "cmp_code";
            cboSelectCompany.DataSource = dt;
            dr = dt.NewRow();

            dr.ItemArray = new object[] { 0, "-Please Select-" };
            dt.Rows.InsertAt(dr, 0);
            cboSelectCompany.SelectedIndex = 0;
        }




        private void yclose()
        {
            string cmpcode;
            int maxcode = Convert.ToInt32(cboSelectCompany.SelectedValue);
            if (maxcode > 9)
            {
                cmpcode = cboSelectCompany.SelectedValue.ToString();
            }
            else
            {
                cmpcode = Convert.ToString("0" + cboSelectCompany.SelectedValue.ToString());
            }
            string currentdbname = "payroll" + "_" + cmpcode + "_" + cboFinYear.Text.Substring(2, 2);

            string dbname = "payroll" + "_" + cmpcode + "_" + cboFinYear.Text.Substring(7, 2);
            string dbpath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath));
            String str, str1, str2;
            str = "BACKUP DATABASE " + currentdbname + " TO DISK='" + dbpath + "\\" + currentdbname + ".bak' WITH INIT";

            str1 = "RESTORE DATABASE " + dbname + " FROM disk='" + dbpath + "\\" + currentdbname + ".bak'" +
                    "WITH FILE=1," +
                    "MOVE 'payroll_02_16' TO '" + dbpath + "\\" + dbname + "_Data.mdf'," +
                    "MOVE 'payroll_02_16_log' TO '" + dbpath + "\\" + dbname + "_Log.ldf'," +
                    "RECOVERY, REPLACE, STATS = 10";

            str2 = "truncate table arear;truncate table arear1;truncate table arear_mnth;truncate table arr_monthly;truncate table bonus;truncate table dup_att;truncate table emp_hist;truncate table fulfinal;truncate table mshift;truncate table overhr;truncate table overtm;truncate table timet;truncate table hourt;truncate table incmnt;truncate table loan;truncate table mdeduct;truncate table month_ad;truncate table month_re;truncate table monthly;truncate table monthlyd;truncate table month_add;truncate table emp_histd;truncate table hourtd;truncate table timetd;truncate table fulfinal1;truncate table user_select;truncate table day_status;truncate table day_statusd;";

            if (conmaster.State == ConnectionState.Closed)
                    conmaster.Open();
                SqlCommand checkdbexistscmd = new SqlCommand("select count(*) from sys.databases where name='" + dbname + "' COLLATE SQL_Latin1_General_CP1_CS_AS", conmaster);
                checkdbexistscmd.CommandType = CommandType.Text;
                int count = (int)checkdbexistscmd.ExecuteScalar();

            if (count == 1)
            {
                MessageBox.Show("Year closing already done for this company try with another company.", "Year Closing", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                try
                {



                    if (conmaster.State == ConnectionState.Open)
                        conmaster.Close();
                    conmaster.Open();
                    SqlCommand myCommand = new SqlCommand(str, conmaster);
                    myCommand.CommandTimeout = 0;
                    myCommand.ExecuteNonQuery();
                    lblYclosestatus.Show();
                    //MessageBox.Show("Year Closing Done Successfully.[backup created]", "Year Closing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SqlCommand mycommand1 = new SqlCommand(str1, conmaster);
                    mycommand1.CommandTimeout = 0;
                    mycommand1.ExecuteNonQuery();

                    lblYclosestatus.Show();

                    SqlConnection conn = new SqlConnection("Data Source=" + SqlconnMain.servername + ";Initial Catalog=" + dbname + ";User Id=" + SqlconnMain.username + ";Password=" + SqlconnMain.password + ";");
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand delcmd = new SqlCommand(str2, conn);
                    delcmd.CommandType = CommandType.Text;
                    delcmd.ExecuteNonQuery();

                    lblYclosestatus.Show();

                    MessageBox.Show("Year Closing Done Successfully for this company.[Database Restored Successfully.]", "Year Closing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblYclosestatus.Hide();


                }
                catch (System.Exception ex)
                {

                    //MessageBox.Show("Year closing already done for this company.", "Year Closing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(ex.ToString(), "Year Closing", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            Year_Closing_Tool yclose = new Year_Closing_Tool();
            yclose.Show();
            this.Hide();
        }
        // void bgw_DoWork(object sender, DoWorkEventArgs e)
        // {
        //     int total = 57; //some number (this is your variable to change)!!

        //     for (int i = 0; i <= total; i++) //some number (total)
        //     {
        //         System.Threading.Thread.Sleep(100);
        //         int percents = (i * 100) / total;
        //         bgw.ReportProgress(percents, i);
        //         //2 arguments:
        //         //1. procenteges (from 0 t0 100) - i do a calcumation 
        //         //2. some current value!
        //     }
        // }

        // void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        // {
        //     progressBar1.Value = e.ProgressPercentage;
        //     lblpg1.Text = String.Format("Progress: {0} %", e.ProgressPercentage);
        //     //label7.Text = String.Format("Total items transfered: {0}", e.UserState);
        // }

        //private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        // {
        //     //do the code when bgv completes its work
        //     lblYclosestatus.Text = "";
        //     lblYclosestatus.Text = "Year closing process completed successfully.";

        // }
    }
}
