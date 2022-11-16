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

namespace DeepakERPSystem
{
    public partial class Company : Form
    {
       //SqlConnection conn = new SqlConnection(SqlconnMain.sbmmc);
       SqlConnection conn = new SqlConnection(SqlconnPayroll.sbpayroll);
        public Company()
        {
            InitializeComponent();
        }

      

        private void btnAdd_Click(object sender, EventArgs e)
        {
            blankonaddnew();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            createnewcompany();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            btnModify.Visible = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;

            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                string pcusername = myglobalvar.strUsername.ToString().Trim();
                string pcname = myglobalvar.strPCName.ToString().Trim();
                string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                string username = myglobalvar.selecteduser.ToString().Trim();
                string selectedcompany = myglobalvar.selectedcompany;

                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + pcname + "/" + pcusername + "','" + ipaddress + "','" + username + "','Clicked on Modify button on the company master page.','" + selectedcompany + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            this.Close();
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                string pcusername = myglobalvar.strUsername.ToString().Trim();
                string pcname = myglobalvar.strPCName.ToString().Trim();
                string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                string username = myglobalvar.selecteduser.ToString().Trim();
                string selectedcompany = myglobalvar.selectedcompany;

                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + pcname + "/" + pcusername + "','" + ipaddress + "','" + username + "','Clicked on Exit button on the company master page.','" + selectedcompany + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
        }
        private void blankonaddnew()
        {
            txtcompname.Text = "";
            txtcmpaddress.Text = "";
            txtTelephone.Text = "";
            txtMobile.Text = "";
            txtFax.Text = "";
            txtShortname.Text = "";
            txtplace.Text = "";
            txtPfno.Text = "";
            txtEsino.Text = "";
            txtemailid.Text = "";
            txtwebsite.Text = "";
            txtpan.Text = "";
            txtgst.Text = "";
            txtimgpath.Text = "";
            txtOwner.Text = "";
            txtOwneraddress.Text = "";
            txtOwnContact.Text = "";
            txtOwndesg.Text = "";
            txtcompbankname.Text = "";
            txtbranchname.Text = "";
            txtbranchaddress.Text = "";
            txtacno.Text = "";
            txtifscno.Text = "";
            txtManager.Text = "";
            txtcontact1.Text = "";
            txtcontact2.Text = "";
            txtcontact3.Text = "";
            cboCmptype.SelectedIndex = 0;
            txtcompname.Focus();
            btnAdd.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                //string pcusername = myglobalvar.strUsername.ToString().Trim();
                //string pcname = myglobalvar.strPCName.ToString().Trim();
                //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                //string username = myglobalvar.selecteduser.ToString().Trim();
                //int selectedcompany = myglobalvar.selectedcompany;

                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Addnew button on the company master page.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
        }

        private void Company_Load(object sender, EventArgs e)
        {
            getcompanylist();
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnModify.Visible = true;
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnModify.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Enabled = true;
          
            try
            {
                int u_pwd = 0;
                myglobalvar code = new myglobalvar();
                //string pcusername = myglobalvar.strUsername.ToString().Trim();
                //string pcname = myglobalvar.strPCName.ToString().Trim();
                //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                //string username = myglobalvar.selecteduser.ToString().Trim();
                //string selectedcompany = myglobalvar.selectedcompany;

                code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Cancel button on the company master page.','" + code.selectedcompany1 + "','" + u_pwd + "')");
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }

        }
        //
        private void createnewcompany()
        {
            if (txtcompname.Text == "" || txtShortname.Text == "" || cboCmptype.SelectedIndex == 0)
            {
                MessageBox.Show("Please enter company name,short name and select company type atlease", "Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {

                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("spinsertcompanydata", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compname", txtcompname.Text.Trim());
                    cmd.Parameters.AddWithValue("@compaddress", txtcmpaddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@telephone", txtTelephone.Text.Trim());
                    cmd.Parameters.AddWithValue("@mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@fax", txtFax.Text.Trim());
                    cmd.Parameters.AddWithValue("@shortname", txtShortname.Text.Trim());
                    cmd.Parameters.AddWithValue("@place", txtplace.Text.Trim());
                    cmd.Parameters.AddWithValue("@pfno", txtPfno.Text.Trim());
                    cmd.Parameters.AddWithValue("@esino", txtEsino.Text.Trim());
                    cmd.Parameters.AddWithValue("@emailid", txtemailid.Text.Trim());
                    cmd.Parameters.AddWithValue("@website", txtwebsite.Text.Trim());
                    cmd.Parameters.AddWithValue("@pan", txtpan.Text.Trim());
                    cmd.Parameters.AddWithValue("@gst", txtgst.Text.Trim());
                    cmd.Parameters.AddWithValue("@imgpath", txtimgpath.Text.Trim());
                    cmd.Parameters.AddWithValue("@ownname", txtOwner.Text.Trim());
                    cmd.Parameters.AddWithValue("@ownaddress", txtOwneraddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@owncontact", txtOwnContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@owndesg", txtOwndesg.Text.Trim());
                    cmd.Parameters.AddWithValue("@compbankname", txtcompbankname.Text.Trim());
                    cmd.Parameters.AddWithValue("@branchname", txtbranchname.Text.Trim());
                    cmd.Parameters.AddWithValue("@branchaddress", txtbranchaddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@acno", txtacno.Text.Trim());
                    cmd.Parameters.AddWithValue("@ifscno", txtifscno.Text.Trim());
                    cmd.Parameters.AddWithValue("@mangr", txtManager.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone1", txtcontact1.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone2", txtcontact2.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone3", txtcontact3.Text.Trim());
                    cmd.Parameters.AddWithValue("@comptype", cboCmptype.SelectedIndex);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("You have successfuly created a new company.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getcompanylist();

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();
                        //string pcusername = myglobalvar.strUsername.ToString().Trim();
                        //string pcname = myglobalvar.strPCName.ToString().Trim();
                        //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                        //string username = myglobalvar.selecteduser.ToString().Trim();
                        //int selectedcompany = myglobalvar.selectedcompany;

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Save button on the company master page to create new company with name " + txtcompname.Text + " .','" + code.selectedcompany1 + "','" + u_pwd + "')");
                        blankonaddnew();
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnAdd.Visible = true;
                        btnModify.Enabled = true;
                        btnDelete.Enabled = true;
                        conn.Close();
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }
                    //
                }
                catch (InvalidCastException ee)
                {
                    throw (ee);
                }
            }
        }
        //fun for getting company list
        public void getcompanylist()
        {
            try
            {
                SqlConnection conn = new SqlConnection(SqlconnPayroll.sbpayroll);
                SqlCommand cmd = new SqlCommand("select row_number() over(order by(sub_code)) as sno,sub_code,name,[address],telephone,mobile,fax,sub_sname,place,pf_no,esi_no,email,website,pan,gst,image_path,ownname,ownaddress,owncontact,owndesg,cmpbankname,cmpbankbranc,cmpbranchadd,cmpbankacno,cmpbankifsc,mangrname,phone1,phone2,phone3,case when cont=1 then 'Parent Company' else 'Contractor' end as cont from subcomp order by sub_code,name", conn);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (InvalidCastException ee)
            {
                throw (ee);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtcompname.Text = row.Cells[2].Value.ToString();
            }

        }
        ///////////////keep uperside of this line..
    }
}
