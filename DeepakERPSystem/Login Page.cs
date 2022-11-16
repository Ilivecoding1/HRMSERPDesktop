using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DeepakERPSystem
{
    
    public partial class Login_Page : Form
    {
        
        SqlConnection conmmc = new SqlConnection(SqlconnMain.sbmmc);
       


        public Login_Page()
        {
            Thread t = new Thread(new ThreadStart(SplashStart));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();

        }
        public void SplashStart()
        {
            Application.Run(new Splash());
        }


        private void Login_Page_Load(object sender, EventArgs e)
        {
            try
            {

                lblshowusername.Text = (myglobalvar.strUsername.ToString()); // for computer user name
                lblshowpcname.Text = (myglobalvar.strPCName.ToString());      //for computer name.
                foreach (IPAddress ip in myglobalvar.strHostname.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        myglobalvar.strIPAddress = ip.ToString();
                    }
                }
                lblshowipaddress.Text = (myglobalvar.strIPAddress.ToString()); //for ip address


              
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(),"Oops somthing went wrong",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            //getutype();
            getfinyear();
            //getcompanylist();
            getonebackyear();

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
            

        }
        private void getonebackyear()
        {

            for (int i = 2004; i < 2030; i++)
            {
                int k = i + 1;
                string l = i + "-" + k;
                cboBackyear.Items.Add(l);

            }
            string path = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\\year.dll");
            System.IO.StreamReader txtfile = new System.IO.StreamReader(path);
            int line = int.Parse(txtfile.ReadLine());
            cboBackyear.SelectedIndex = line-1;


        }

        private void getutype()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (conmmc.State == ConnectionState.Open)
                    conmmc.Close();
                conmmc.Open();
                SqlCommand cmd = new SqlCommand("select *from Usertype", conmmc);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cboUsertype.DisplayMember = "User_type";
                cboUsertype.ValueMember = "id";
                cboUsertype.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Please Select-" };
                dt.Rows.InsertAt(dr, 0);
                cboUsertype.SelectedIndex = 0;
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            //conn1.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Enter correct username", "Error in Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
           else if (txtPassword.Text == "")
            {
                MessageBox.Show("Enter correct Password", "Error in Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            //else if (cboCompany.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Select a company then proceed.", "Error in Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            else
            {
                string cmpcode;
                int maxcode = Convert.ToInt32(cboCompany.SelectedValue);
                if (maxcode > 9)
                {
                    cmpcode = cboCompany.SelectedValue.ToString();
                }
                else
                {
                    cmpcode = Convert.ToString("0" + cboCompany.SelectedValue.ToString());
                }

                myglobalvar.selectedcompany = cmpcode; 
                myglobalvar.selectedfinyear = cboFinYear.SelectedItem.ToString();
                myglobalvar.selectedonebackfinyear = cboBackyear.SelectedItem.ToString();

                myglobalvar.selectedusertype = cboUsertype.SelectedItem.ToString();

                myglobalvar.selecteduser = txtUsername.Text;
                myglobalvar.selectedcompanyname = cboCompany.SelectedItem.ToString();
                try
                {
                   

                    //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                    if (conmmc.State == ConnectionState.Open)
                        conmmc.Close();
                    conmmc.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from userm where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS and usr_pwd=@password COLLATE SQL_Latin1_General_CP1_CS_AS",conmmc);// and usr_type=@usertype", conmmc);
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@usertype", cboUsertype.SelectedIndex);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    int usercount = (int)cmd.ExecuteScalar();

                    SqlCommand cmd1 = new SqlCommand("select lock from userm where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);// and usr_type=@usertype", conmmc);
                    cmd1.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@usertype", cboUsertype.SelectedIndex);
                    cmd1.Parameters.AddWithValue("@username", txtUsername.Text);
                    int locked = (int)cmd1.ExecuteScalar();

                    SqlCommand cmd2 = new SqlCommand("select active from userm where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);// and usr_type=@usertype", conmmc);
                    cmd2.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@usertype", cboUsertype.SelectedIndex);
                    cmd2.Parameters.AddWithValue("@username", txtUsername.Text);
                    int deactivated = (int)cmd2.ExecuteScalar();
                    if (locked > 0)
                    {
                        MessageBox.Show("Dear User your user id is locked, so you are not authorised to login into the system, please contact to your department.", "Unauthorized Login Detected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        txtUsername.Focus();

                    }
                   else if (deactivated > 1)
                    {
                        MessageBox.Show("Dear User your user id was created but still not active, so you are not authorised to login into the system, please contact to your department.", "Unauthorized Login Detected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        txtUsername.Focus();

                    }
                    ////////////

                    ///////
                    else if (cboCompany.SelectedIndex==0)
                    {
                        MessageBox.Show("Please Select a company from dropdown list below then proceed.", "Company not selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (usercount == 1)
                    {
                        try
                        {
                            int u_pwd = 0;
                            myglobalvar code = new myglobalvar();
                            code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + lblshowpcname.Text + "/" + lblshowusername.Text + "','" + lblshowipaddress.Text + "','" + txtUsername.Text + "','Logged Into Application Successfuly.','" + cboCompany.SelectedIndex + "','" + u_pwd + "')");
                        }
                        catch (InvalidCastException ee)
                        {
                            throw (ee);
                        }

                        this.Hide();
                        AdminHome ahm = new AdminHome();
                        ahm.Show();
                       
                        


                        //if (cboUsertype.SelectedIndex == 1)
                        //{

                        //    this.Hide();
                        //    AdminHome ahm = new AdminHome();
                        //    ahm.Show();


                        //}
                        //else if (cboUsertype.SelectedIndex == 2)
                        //{
                        //    this.Hide();
                        //    AdminHome ahm = new AdminHome();
                        //    ahm.Show();

                        //    //this.Hide();
                        //    //Home hm = new Home();
                        //    //hm.Show();
                        //}

                    }

                    else
                    {
                        MessageBox.Show("Please check your usertype, then enter correct username and password.", "Error in login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                }

                catch (Exception ee)
                {
                    //MessageBox.Show("Error In Login, Please contact your software provider/vendor support team. ", "Error In Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    MessageBox.Show("Error In Login due to "+ee.ToString());
                }
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            
            DataRow dr;
            if (conmmc.State == ConnectionState.Open)
                conmmc.Close();
            conmmc.Open();
            SqlCommand cmd = new SqlCommand("splogin", conmmc);
            //cmd.Parameters.AddWithValue("@utype", cboUsertype.SelectedIndex);
            cmd.Parameters.AddWithValue("@uname",txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            cboCompany.DisplayMember = "cmp_name";
            cboCompany.ValueMember = "cmp_code";
            cboCompany.DataSource = dt;
            dr = dt.NewRow();

            dr.ItemArray = new object[] { 0, "-Please Select-" };
            dt.Rows.InsertAt(dr, 0);
            cboCompany.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            this.Close();
            
        }

        private void forgetUname_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forgot_Username forgetuname = new Forgot_Username();
            forgetuname.Show();
        }

        private void forgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            Forgot_Password forgetpass = new Forgot_Password();
            forgetpass.Show();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            DataRow dr;
            if (conmmc.State == ConnectionState.Open)
                conmmc.Close();
            conmmc.Open();
            SqlCommand cmd = new SqlCommand("spgetusertype", conmmc);
            cmd.Parameters.AddWithValue("@uname", txtUsername.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            cboUsertype.DisplayMember = "user_type";
            cboUsertype.ValueMember = "id";
            cboUsertype.DataSource = dt;
            dr = dt.NewRow();

            //dr.ItemArray = new object[] { 0, "-Please Select-" };
            //dt.Rows.InsertAt(dr, 0);
            //cboUsertype.SelectedIndex = 0;

        }

        private void cboUsertype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblUnlockUname_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unlock_Username unlock = new Unlock_Username();
            unlock.Show();
        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
