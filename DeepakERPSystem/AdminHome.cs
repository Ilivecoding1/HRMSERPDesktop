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
using Tulpep.NotificationWindow;
using System.Threading;

namespace DeepakERPSystem
{
   
    public partial class AdminHome : Form
    {
        
        //conn for currentyear db access
        SqlConnection conmmc = new SqlConnection(SqlconnMain.sbmmc);
        SqlConnection conpayroll = SqlconnPayroll.GetCurrentPayrollDBConnection();//new SqlConnection(SqlconnPayroll.sbpayroll);//
        //conn for backyear db access
        SqlConnection conmmc1 = new SqlConnection(SqlconnMain.sbmmc);
        SqlConnection oneyearbackdb = SqlconnPayrollbackdb.GetBackYrPayrollDBConnection();//new SqlConnection(SqlconnPayrollbackdb.sbbackpayroll);//
        private int childFormNumber = 0;

        public AdminHome()
        {
            
            InitializeComponent();
           

        }
      
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {
            try
            {
                
                string filepathwithname;
                SqlConnection conpayroll = new SqlConnection(SqlconnPayroll.sbpayroll);
                if (conpayroll.State == ConnectionState.Closed)
                    conpayroll.Open();
                SqlCommand cmd = new SqlCommand("spgetcodepath", conpayroll);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", myglobalvar.selecteduser.ToString().Trim());
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    filepathwithname = rdr["image_path"].ToString().Trim() + "\\" + rdr["pay_code"].ToString().Trim() + ".jpg";
                    if (File.Exists(filepathwithname))
                    {
                        picBoxUser.Image = Image.FromFile(filepathwithname.ToString());
                        picBoxUser.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        string absolutePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) +"\\"+ @"Images\LOGO.jpg");

                        picBoxUser.Image = System.Drawing.Image.FromFile(absolutePath);
                        picBoxUser.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                }
                else
                {
                    //lblNoImg.Text="No Image Found";
                    string absolutePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + @"Images\LOGO.jpg");

                    picBoxUser.Image = System.Drawing.Image.FromFile(absolutePath);
                    picBoxUser.SizeMode = PictureBoxSizeMode.StretchImage;

                }
               
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

            //lblCompanyName.Text = myglobalvar.selectedcompanyname.ToString();
            lblFinYear.Text = myglobalvar.selectedfinyear.ToString();
            lblUsername.Text = myglobalvar.selecteduser.ToString();
            lblUsertype.Text = "";

            if (conmmc.State == ConnectionState.Closed)
            {
                if (conpayroll.State == ConnectionState.Closed)
                {
                    if (conmmc1.State == ConnectionState.Closed)
                    {
                        if (oneyearbackdb.State == ConnectionState.Closed)
                        {
                            try
                            {

                                conmmc.Open();
                                conpayroll.Open();


                                try
                                {
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                    MessageBox.Show("connection sucessful-connected to the database payroll"+"_" + myglobalvar.selectedcompany + "_" + myglobalvar.selectedfinyear.ToString().Substring(2, 2) + "Database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    SqlCommand cmd = new SqlCommand("select cmp_name from cmpm where cmp_code=" + myglobalvar.selectedcompany, conmmc);

                                    cmd.CommandType = CommandType.Text;

                                    SqlDataReader rdr = cmd.ExecuteReader();
                                    while (rdr.Read())
                                    {
                                        lblCompanyName.Text = rdr["cmp_name"].ToString();
                                        //////////////////////////
                                        MessageBox.Show("Welcome to :" + lblCompanyName.Text, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        ////////////////////////////////////////////////////////////////////////////////////////////

                                    }
                                    rdr.Close();
                                }
                                catch(Exception ee)
                                {
                                    MessageBox.Show(ee.ToString(), "Oops something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                                

                               

                                try
                                {
                                    
                                    SqlCommand cmd1 = new SqlCommand("select CASE When usr_type=1 then 'Administrator' else CASE when usr_type=2 then  'User' end end as usertype from userm where usr_name=@uname", conmmc);
                                    cmd1.CommandType = CommandType.Text;
                                    cmd1.Parameters.AddWithValue("@uname",myglobalvar.selecteduser.ToString().Trim());
                                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                                    while (rdr1.Read())
                                    {
                                        lblUsertype.Text = rdr1["usertype"].ToString();
                                        // rdr1.Close();

                                    }
                                }
                                catch(Exception ee)
                                {
                                    //MessageBox.Show("","",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                                    MessageBox.Show(ee.ToString(),"Oops something went wrong",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                                }

                                ////////////////////////////////
                                conmmc1.Open();
                         oneyearbackdb.Open();
                         MessageBox.Show("connection sucessful-connected to the database payroll" + "_" + myglobalvar.selectedcompany + "_" + myglobalvar.selectedonebackfinyear.ToString().Substring(2, 2) + "Database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //PopupNotifier popup = new PopupNotifier();
                                //popup.TitleText = "You have some user's forget password request";
                                //popup.TitleColor = Color.Magenta;
                                //popup.ContentText = "you have one new notifications pending. ";//"Hi this is the space where you will have your message.";
                                //popup.ContentColor = Color.Maroon;
                                //popup.Popup();//CALL THIS TO SHOW POPUP

                                //PopupNotifier popup1 = new PopupNotifier();
                                //popup1.TitleText = "You have password forget request";
                                //popup1.TitleColor = Color.Magenta;
                                //popup1.ContentText = lblnotification.Text = "you have one new notifications pending. ";//"Hi this is the space where you will have your message.";
                                //popup1.ContentColor = Color.Maroon;
                                //popup1.Popup();//CALL THIS TO SHOW POPUP

                            }
                    catch (Exception ee)
                    {
                        MessageBox.Show("couldn't connected to the main database due to " + ee.ToString(), "Error In Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.Close();

                    }
                        }
                    }

                }
            }
         
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserRoleCenter userrole = new UserRoleCenter();
            userrole.MdiParent = this;
            userrole.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show_Notifications notify = new Show_Notifications();
            //notify.Show();
        }

        private void notifyIcon2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show_Notifications1 NOTIFY1 = new Show_Notifications1();
            //NOTIFY1.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void userRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on User Control Center Menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            User_Master userchild = new User_Master();
            userchild.MdiParent = this;
            userchild.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show_Notifications2 notify3 = new Show_Notifications2();
            //notify3.Show();
        }

        private void logoutExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutErp about = new AboutErp();
            about.MdiParent = this;
            about.Show();
        }

        private void salaryProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logoutExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
               
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Application restart menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            this.Hide();
            //Application.Exit();
            Login_Page login = new Login_Page();
            login.Show();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataImportExportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void userActivityReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserLogReports userlog = new UserLogReports();
            userlog.MdiParent = this;
            userlog.Show();
        }

        private void logoutExitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
               
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Logged Out from Application Successfuly.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            this.Close();
            Application.Exit();
        }

        private void companyMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Company Master Menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            Company cmpchild = new Company();
            cmpchild.MdiParent = this;
            cmpchild.Show();
        }

        private void yearClosingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on year closing Menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            Year_Closing_Tool cmpchild = new Year_Closing_Tool();
            cmpchild.MdiParent = this;
            cmpchild.Show();
        }

       

        private void employeeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Employee Master Menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            Employee_Master cmpchild = new Employee_Master();
            cmpchild.MdiParent = this;
            cmpchild.Show();
        }

        private void departmentMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Department Master Menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            NewForm1 depchild = new NewForm1();
            depchild.MdiParent = this;
            depchild.Show();
        }

        private void subDepartmentMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Sub Department Master Menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            Subdepartment depchild = new Subdepartment();
            depchild.MdiParent = this;
            depchild.Show();
        }

        private void designationMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                
                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Designation Master Menu.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
            Designation depchild = new Designation();
            depchild.MdiParent = this;
            depchild.Show();
        }

        private void employeeMasterImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeImport empimport = new EmployeeImport();
            empimport.MdiParent = this;
            empimport.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserLogReports usrlog = new UserLogReports();
            usrlog.MdiParent = this;
            usrlog.Show();
        }
    }
    }
