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
using System.Net.Mail;
using System.Net;

namespace DeepakERPSystem
{
    public partial class User_Master : Form
    {
        //SqlConnection conmmc = SQLConnMethod2.GetMMCConDB();
        SqlConnection conmmc = new SqlConnection(SqlconnMain.sbmmc);
        public User_Master()
        {
            InitializeComponent();
        }

        private void btnCreateNewUser_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (txtEmailId.Text == "")
                    {
                        MessageBox.Show("Please enter user's email id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (cboUType.SelectedIndex == 0)
                        {
                            MessageBox.Show("Please select user type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (cboActivate.SelectedIndex == 0)
                            {
                                MessageBox.Show("Please select activation level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                }
            }
            else
            {

                try
                {
                    if (txtImgUpload.Text == "")
                    {
                        MessageBox.Show("Please select a profile picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        if (conmmc.State == ConnectionState.Closed)
                            conmmc.Open();
                        SqlCommand cmd = new SqlCommand("spcreatuser", conmmc);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usr_name", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@usr_pwd", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@usr_type", cboUType.SelectedIndex);
                        cmd.Parameters.AddWithValue("@email", txtEmailId.Text);
                        cmd.Parameters.AddWithValue("@active", cboActivate.SelectedIndex);
                        cmd.ExecuteNonQuery();
                        //to upload image in folder.
                        try
                        {


                            string absolutePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + @"Images\" + Path.GetFileName(txtImgUpload.Text));

                            File.Copy(txtImgUpload.Text, absolutePath, true);

                            /////////////
                            using (MailMessage mm = new MailMessage("mmc.software@gmail.com", txtEmailId.Text.Trim()))
                            {
                                mm.Subject = "Your profile created successfully.";//txtSubject.Text;
                                mm.Body = "Dear User, profile created successfully,Your Login Username is:-"+txtUsername.Text.Trim()+" "+"and password is:-" + txtPassword.Text.Trim() + ".";//txtBody.Text;
                                                                                                                                        //foreach (string filePath in openFileDialog1.FileNames)
                                                                                                                                        //{
                                                                                                                                        //    if (File.Exists(filePath))
                                                                                                                                        //    {
                                                                                                                                        //        string fileName = Path.GetFileName(filePath);
                                                                                                                                        //        mm.Attachments.Add(new Attachment(filePath));
                                                                                                                                        //    }
                                                                                                                                        //}
                                mm.IsBodyHtml = false;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("mmc.software@gmail.com", "anna_2005");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);
                                //MessageBox.Show("Email sent.", "Message");
                            }

                            //////


                            MessageBox.Show("You have sucessfully created new user.", "User creation sucessful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            /////////////
                            try
                            {
                                int u_pwd = 0;
                                myglobalvar code = new myglobalvar();
                                //string pcusername = myglobalvar.strUsername.ToString().Trim();
                                //string pcname = myglobalvar.strPCName.ToString().Trim();
                                //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                                //string username = myglobalvar.selecteduser.ToString().Trim();
                                //int selectedcompany = myglobalvar.selectedcompany;

                                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on create new user and created a new user with name-" +txtUsername.Text+"-successfuly.','" + code.selectedcompany1 + "','" + u_pwd + "')");
                            }
                            catch (InvalidCastException ee)
                            {
                                throw (ee);
                            }
                            ///////////////

                            txtUsername.Text = "";
                            txtPassword.Text = "";
                            txtEmailId.Text = "";
                            txtImgUpload.Text = "";
                            cboActivate.SelectedIndex = 0;
                            cboUType.SelectedIndex = 0;

                            bindgridview1();

                        }
                        catch (Exception ee)
                        {

                        }
                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString(), "Error in User creation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }


        private void SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectfile = new OpenFileDialog();
            selectfile.Filter = "Image Files(*.jpg;)|*.jpg";
            if (selectfile.ShowDialog() == DialogResult.OK)
            {
                txtImgUpload.Text = selectfile.FileName;
                empPicBox.Image = new Bitmap(selectfile.FileName);
                empPicBox.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void UploadFile_Click(object sender, EventArgs e)
        {

        }

        private void User_Master_Load(object sender, EventArgs e)
        {
            bindgridview1();
            getusername();
            getusername1();
            getusername2();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
         
        }

        private void btnGetUserList_Click(object sender, EventArgs e)
        {
            bindgridview1();
        }
        private void bindgridview1()
        {
            dataGridView1.Columns.Clear();
            try
            {
                if (conmmc.State == ConnectionState.Closed)
                    conmmc.Open();
                SqlCommand cmd1 = new SqlCommand("select ROW_NUMBER()OVER(order by (usr_code))as SNo,usr_code as Code, usr_name as [User Name],usr_pwd as [Password],usr_pwd1 as CPassword,case when usr_type=1 then 'Administrator' else 'User' end as [Type],email as Email,case when active=1 then 'Yes' else 'No' end as Active,case when lock=0 then 'Unlocked User' else 'Locked User' end as [Status]  from userm  order by usr_code", conmmc);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

                //int i = 1;
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    row.Cells["SNo"].Value = i; i++;
                //}
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmailId.Text = "";
            txtImgUpload.Text = "";
            cboActivate.SelectedIndex = 0;
            cboUType.SelectedIndex = 0;
            lblSucess.Visible = false;
            LBLpwdmatch.Visible = false;
            cboUTypeUpdate.SelectedIndex = 0;
            cboGetUsername.SelectedIndex = 0;
            cboUnlockUser.SelectedIndex = 0;
            cboUpdateBasicDetails.SelectedIndex = 0;
            txtUsername.Focus();

            //Add the Button Column.
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "";
            buttonColumn.Width = 60;
            buttonColumn.Name = "buttonColumn";
            buttonColumn.Text = "Delete";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Insert(9, buttonColumn);
        }
        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
           
            if (cboGetUsername.SelectedIndex==0)
            {
                MessageBox.Show("Please select username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtOldPass.Text == "")
            {
                MessageBox.Show("Please enter old password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtNewPass.Text == "")
            {
                MessageBox.Show("Please enter new password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
           else if (txtConfirmPass.Text == "")
            {
                MessageBox.Show("Please enter confirm password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtNewPass.Text.Trim()!= txtConfirmPass.Text.Trim())
            {
                MessageBox.Show("Your New password and confirmed password doesn't match, Please correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                updateusername();
              
            }

        }
        private void getusername()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (conmmc.State == ConnectionState.Open)
                    conmmc.Close();
                conmmc.Open();
                SqlCommand cmd = new SqlCommand("select *from userm", conmmc);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cboGetUsername.DisplayMember = "usr_name";
                cboGetUsername.ValueMember = "usr_code";
                cboGetUsername.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Please Select Username-" };
                dt.Rows.InsertAt(dr, 0);
                cboGetUsername.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            //conn1.Close();
        }

        private void getusername1()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (conmmc.State == ConnectionState.Open)
                    conmmc.Close();
                conmmc.Open();
                SqlCommand cmd = new SqlCommand("select *from userm", conmmc);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cboUnlockUser.DisplayMember = "usr_name";
                cboUnlockUser.ValueMember = "usr_code";
                cboUnlockUser.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select Username-" };
                dt.Rows.InsertAt(dr, 0);
                cboUnlockUser.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            //conn1.Close();
        }
        private void getusername2()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (conmmc.State == ConnectionState.Open)
                    conmmc.Close();
                conmmc.Open();
                SqlCommand cmd = new SqlCommand("select *from userm", conmmc);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cboUpdateBasicDetails.DisplayMember = "usr_name";
                cboUpdateBasicDetails.ValueMember = "usr_code";
                cboUpdateBasicDetails.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Select Username-" };
                dt.Rows.InsertAt(dr, 0);
                cboUpdateBasicDetails.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            //conn1.Close();
        }

        private void updateusername()
        {
            try
            {

                int result = 0;
                if (conmmc.State == ConnectionState.Closed)
                    conmmc.Open();
                SqlCommand cmd = new SqlCommand("updateupass", conmmc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usrname", cboGetUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@oldpass", txtOldPass.Text);
                cmd.Parameters.AddWithValue("@newpass", txtNewPass.Text);
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    using (MailMessage mm = new MailMessage("mmc.software@gmail.com", txtEmailHolder.Text.Trim()))
                    {
                        mm.Subject = "Your Password Reset Sucessful.";//txtSubject.Text;
                        mm.Body = "Dear User, your password reset completed, your password is:-"+txtNewPass.Text.Trim()+".";//txtBody.Text;
                        //foreach (string filePath in openFileDialog1.FileNames)
                        //{
                        //    if (File.Exists(filePath))
                        //    {
                        //        string fileName = Path.GetFileName(filePath);
                        //        mm.Attachments.Add(new Attachment(filePath));
                        //    }
                        //}
                        mm.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("mmc.software@gmail.com", "anna_2005");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        //MessageBox.Show("Email sent.", "Message");
                    }

                    MessageBox.Show("You have updated your password successfully,Check your Inbox for new password.", "Update Sucsessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();
                        //string pcusername = myglobalvar.strUsername.ToString().Trim();
                        //string pcname = myglobalvar.strPCName.ToString().Trim();
                        //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                        //string username = myglobalvar.selecteduser.ToString().Trim();
                        //int selectedcompany = myglobalvar.selectedcompany;

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on update password button and updated password is " + txtNewPass.Text + "for" + cboGetUsername.Text + "user.','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }
                    cboGetUsername.SelectedIndex = 0;
                    txtOldPass.Text = "";
                    txtNewPass.Text = "";
                    txtConfirmPass.Text = "";


                    
                }
                else
                {
                    MessageBox.Show("Your password did not changed.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPass.Text != txtConfirmPass.Text)
            {
                lBLSuccess.Visible = false;
                LBLpwdmatch.Visible = true;
                LBLpwdmatch.Text = "";
               
                LBLpwdmatch.Text = "Your new password and confirm password doesn't match.";
                //  MessageBox.Show("Your new password and confirm password doesn't match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LBLpwdmatch.Visible = false;
                lBLSuccess.Visible = true;
                lBLSuccess.Text = "";
                lBLSuccess.Text = "Your new password and confirm password matched.";
            }
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {

           
        }
        private void getemailid()
        {
            if (conmmc.State == ConnectionState.Closed)
                conmmc.Open();
            SqlCommand cmd1 = new SqlCommand("select email from userm where usr_name=@usrname",conmmc);
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.AddWithValue("@usrname",cboGetUsername.Text.Trim());
           // SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlDataReader rdr1 = cmd1.ExecuteReader();
            while(rdr1.Read())
            {
                txtEmailHolder.Text = rdr1["email"].ToString();
                
            }
            txtEmailHolder.ReadOnly = true;
            rdr1.Close();

        }

        private void cboGetUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboGetUsername.SelectedIndex>0)
                {
                    getemailid();
                }
                   
               
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());

            }
        }

        private void btnLockUser_Click(object sender, EventArgs e)
        {
            if (cboUnlockUser.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a user to lock..", "User Lock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (conmmc.State == ConnectionState.Closed)
                    conmmc.Open();
                SqlCommand cmd4 = new SqlCommand("SELECT lock from userm where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                cmd4.CommandType = CommandType.Text;
                cmd4.Parameters.AddWithValue("@username", cboUnlockUser.Text.Trim());
                int locked = (int)cmd4.ExecuteScalar();

                if (locked == 1)
                {
                    MessageBox.Show("This user is already locked..", "User locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        if (conmmc.State == ConnectionState.Closed)
                            conmmc.Open();
                        SqlCommand cmd5 = new SqlCommand("update userm set lock=1 where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                        cmd5.CommandType = CommandType.Text;
                        cmd5.Parameters.AddWithValue("@username", cboUnlockUser.Text);
                        cmd5.ExecuteNonQuery();
                        MessageBox.Show("You have locked username successfully.", "User Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            int u_pwd = 0;
                            myglobalvar code = new myglobalvar();
                            //string pcusername = myglobalvar.strUsername.ToString().Trim();
                            //string pcname = myglobalvar.strPCName.ToString().Trim();
                            //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                            //string username = myglobalvar.selecteduser.ToString().Trim();
                            //int selectedcompany = myglobalvar.selectedcompany;

                            code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on lock user button and locked " + cboUnlockUser.Text +" user.','" + code.selectedcompany1 + "','" + u_pwd + "')");
                        }
                        catch (InvalidCastException ee)
                        {
                            throw (ee);
                        }

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.ToString());

                    }
                }
            }
        }

        private void btnUnlockUser_Click(object sender, EventArgs e)
        {
            if (cboUnlockUser.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a user to unlock..", "User unLock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    if (conmmc.State == ConnectionState.Closed)
                        conmmc.Open();
                    SqlCommand cmd2 = new SqlCommand("SELECT lock from userm where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@username", cboUnlockUser.Text.Trim());
                    int locked = (int)cmd2.ExecuteScalar();

               
                 if (locked == 0)
                    {
                        MessageBox.Show("This user is already unlocked..", "User unlocked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        try
                        {
                            if (conmmc.State == ConnectionState.Closed)
                                conmmc.Open();
                            SqlCommand cmd3 = new SqlCommand("update userm set lock=0 where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                            cmd3.CommandType = CommandType.Text;
                            cmd3.Parameters.AddWithValue("@username", cboUnlockUser.Text);
                            cmd3.ExecuteNonQuery();
                            MessageBox.Show("You have unlocked username successfully.", "User unlocked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            try
                            {
                                int u_pwd = 0;
                                myglobalvar code = new myglobalvar();
                                //string pcusername = myglobalvar.strUsername.ToString().Trim();
                                //string pcname = myglobalvar.strPCName.ToString().Trim();
                                //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                                //string username = myglobalvar.selecteduser.ToString().Trim();
                                //int selectedcompany = myglobalvar.selectedcompany;

                                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on unlock user button and unlocked " + cboUnlockUser.Text + "user.','" + code.selectedcompany1 + "','" + u_pwd + "')");
                            }
                            catch (InvalidCastException ee)
                            {
                                throw (ee);
                            }

                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.ToString());

                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboUpdateBasicDetails.SelectedIndex == 0 || txtUpdateEmail.Text=="" || cboUTypeUpdate.SelectedIndex==0)
            {
                MessageBox.Show("Please select user to update details..", "User updation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {
                    if (conmmc.State == ConnectionState.Closed)
                        conmmc.Open();
                    SqlCommand cmd3 = new SqlCommand("update userm set email=@emailid,usr_type=@usertype where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                    cmd3.CommandType = CommandType.Text;
                    cmd3.Parameters.AddWithValue("@emailid",txtUpdateEmail.Text);
                    cmd3.Parameters.AddWithValue("@usertype", cboUTypeUpdate.SelectedIndex);
                    cmd3.Parameters.AddWithValue("@username", cboUpdateBasicDetails.Text);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("You have successfully updated emailid and user type.", "User updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();
                        //string pcusername = myglobalvar.strUsername.ToString().Trim();
                        //string pcname = myglobalvar.strPCName.ToString().Trim();
                        //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                        //string username = myglobalvar.selecteduser.ToString().Trim();
                        //int selectedcompany = myglobalvar.selectedcompany;

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on update details button and updated "+txtUpdateEmail.Text+"and changed user type"+cboUTypeUpdate.Text+"for"+cboUpdateBasicDetails.Text+".','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }
                    cboUpdateBasicDetails.SelectedIndex = 0;
                    cboUTypeUpdate.SelectedIndex = 0;
                    txtUpdateEmail.Text = "";
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cboUnlockUser.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a user to activate..", "User activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    if (conmmc.State == ConnectionState.Closed)
                        conmmc.Open();
                    SqlCommand cmd2 = new SqlCommand("SELECT active from userm where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@username", cboUnlockUser.Text.Trim());
                    int activated = (int)cmd2.ExecuteScalar();


                    if (activated == 1)
                    {
                        MessageBox.Show("This user is already activated..", "User activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        try
                        {
                            if (conmmc.State == ConnectionState.Closed)
                                conmmc.Open();
                            SqlCommand cmd3 = new SqlCommand("update userm set active=1 where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                            cmd3.CommandType = CommandType.Text;
                            cmd3.Parameters.AddWithValue("@username", cboUnlockUser.Text);
                            cmd3.ExecuteNonQuery();
                            MessageBox.Show("You have activated username successfully.", "User activated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            try
                            {
                                int u_pwd = 0;
                                myglobalvar code = new myglobalvar();
                                //string pcusername = myglobalvar.strUsername.ToString().Trim();
                                //string pcname = myglobalvar.strPCName.ToString().Trim();
                                //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                                //string username = myglobalvar.selecteduser.ToString().Trim();
                                //int selectedcompany = myglobalvar.selectedcompany;

                                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on activate user button and activated " + cboUnlockUser.Text + "user.','" + code.selectedcompany1 + "','" + u_pwd + "')");
                            }
                            catch (InvalidCastException ee)
                            {
                                throw (ee);
                            }

                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.ToString());

                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cboUnlockUser.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a user to deactivate..", "User Deactivation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                if (conmmc.State == ConnectionState.Closed)
                    conmmc.Open();
                SqlCommand cmd4 = new SqlCommand("SELECT active from userm where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                cmd4.CommandType = CommandType.Text;
                cmd4.Parameters.AddWithValue("@username", cboUnlockUser.Text.Trim());
                int deactivate = (int)cmd4.ExecuteScalar();

                if (deactivate == 2)
                {
                    MessageBox.Show("This user is already deactivated..", "User deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        if (conmmc.State == ConnectionState.Closed)
                            conmmc.Open();
                        SqlCommand cmd5 = new SqlCommand("update userm set active=2 where usr_name=@username COLLATE SQL_Latin1_General_CP1_CS_AS", conmmc);
                        cmd5.CommandType = CommandType.Text;
                        cmd5.Parameters.AddWithValue("@username", cboUnlockUser.Text);
                        cmd5.ExecuteNonQuery();
                        MessageBox.Show("You have deactivated username successfully.", "User deactivated.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            int u_pwd = 0;
                            myglobalvar code = new myglobalvar();
                            //string pcusername = myglobalvar.strUsername.ToString().Trim();
                            //string pcname = myglobalvar.strPCName.ToString().Trim();
                            //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                            //string username = myglobalvar.selecteduser.ToString().Trim();
                            //int selectedcompany = myglobalvar.selectedcompany;

                            code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on deactivate button and deactivated " + cboUnlockUser.Text + "user.','" + code.selectedcompany1 + "','" + u_pwd + "')");
                        }
                        catch (InvalidCastException ee)
                        {
                            throw (ee);
                        }

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.ToString());

                    }
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {

           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 9)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    if (MessageBox.Show(string.Format("Do you want to delete User Name: {0}?", row.Cells[2].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            int u_pwd = 0;
                            myglobalvar code = new myglobalvar();
                            //string pcusername = myglobalvar.strUsername.ToString().Trim();
                            //string pcname = myglobalvar.strPCName.ToString().Trim();
                            //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                            //string username = myglobalvar.selecteduser.ToString().Trim();
                            //int selectedcompany = myglobalvar.selectedcompany;

                            code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on delete user button and deleted record for " + row.Cells[2].Value.ToString() + "user.','" + code.selectedcompany1 + "','" + u_pwd + "')");
                        }
                        catch (InvalidCastException ee)
                        {
                            throw (ee);
                        }

                        using (SqlCommand delcmd = new SqlCommand("DELETE FROM userm WHERE usr_code = @usercode", conmmc))
                        {

                            delcmd.CommandType = CommandType.Text;
                            delcmd.Parameters.AddWithValue("@usercode", row.Cells[1].Value);
                            if (conmmc.State == ConnectionState.Closed)
                                conmmc.Open();
                            delcmd.ExecuteNonQuery();
                            conmmc.Close();

                        }


                    }
                    //dataGridView1.Columns.Clear();
                    bindgridview1();



                }


                else if (e.RowIndex >= 0)
                {

                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    txtUsername.Text = row.Cells[2].Value.ToString();
                    txtPassword.Text = row.Cells[3].Value.ToString();
                    txtEmailId.Text = row.Cells[6].Value.ToString();
                    cboUType.Text = row.Cells[5].Value.ToString();
                    cboActivate.Text = row.Cells[7].Value.ToString();

                    txtUsername.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    txtEmailId.ReadOnly = true;
                    txtImgUpload.ReadOnly = true;
                    cboActivate.Enabled = false;
                    cboUType.Enabled = false;
                    btnCreateNewUser.Enabled = false;

                    try
                    {
                        //string username = myglobalvar.selecteduser.ToString().Trim();

                        string filepathwithname;
                        SqlConnection conpayroll = new SqlConnection(SqlconnPayroll.sbpayroll);
                        if (conpayroll.State == ConnectionState.Closed)
                            conpayroll.Open();
                        SqlCommand cmd = new SqlCommand("spgetcodepath", conpayroll);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", row.Cells[2].Value.ToString());
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            filepathwithname = rdr["image_path"].ToString().Trim() + "\\" + rdr["pay_code"].ToString().Trim() + ".jpg";
                            if (File.Exists(filepathwithname))
                            {

                                selectEmpPic.Image = System.Drawing.Image.FromFile(filepathwithname);
                                selectEmpPic.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                            else
                            {
                                string absolutePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + @"Images\LOGO.jpg");

                                selectEmpPic.Image = System.Drawing.Image.FromFile(absolutePath);
                                selectEmpPic.SizeMode = PictureBoxSizeMode.StretchImage;
                            }

                        }
                        else
                        {
                            //lblNoImg.Text="No Image Found";
                            string absolutePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + @"Images\LOGO.jpg");

                            selectEmpPic.Image = System.Drawing.Image.FromFile(absolutePath);
                            selectEmpPic.SizeMode = PictureBoxSizeMode.StretchImage;

                        }
                        rdr.Close();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.ToString());
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }
    }
}
