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
    public partial class Designation : Form
    {
        SqlConnection conn = new SqlConnection(SqlconnPayroll.sbpayroll);
        public Designation()
        {
            InitializeComponent();
            
        }

        private void Designation_Load(object sender, EventArgs e)
        {
            getdesignationlist();
            getdepartmentlist();
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
            txtDesigName.ReadOnly = false;
            cboGetDpartList.Enabled = true;
            cboGetSubDepart.Enabled = true;
            txtDesigName.Text = "";
            btnAdd.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            txtDesigName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            createdesignation();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            btnModify.Visible = false;
            txtDesigName.ReadOnly = false;
            cboGetSubDepart.Enabled = true;
            cboGetDpartList.Enabled = true; 
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            dataGridView1.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDesigName.Text == "")
            {
                MessageBox.Show("Please Enter a valid Designation name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("updatedesignation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desc", txtDesigName.Text);
                    cmd.Parameters.AddWithValue("@depcode", cboGetDpartList.SelectedValue);
                    cmd.Parameters.AddWithValue("@subdepcode", cboGetSubDepart.SelectedValue);
                    cmd.Parameters.AddWithValue("@desigcode", Convert.ToInt32(label4.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','An Existing Designation modified with new name." + txtDesigName.Text + " where its old name was" + textBox1.Text + "','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }

                    getdesignationlist();
                    cboGetSubDepart.Enabled = false;
                    cboGetDpartList.Enabled = false;
                    btnUpdate.Visible = false;
                    btnModify.Visible = true;
                    btnCancel.Visible = false;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;
                    dataGridView1.Enabled = true;

                    MessageBox.Show("Designation Updated successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
                finally
                {

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Are you sure you want to delete this Sub Department: " + textBox1.Text), "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("deldesignation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descode", Convert.ToInt32(label4.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','A Designation deleted with name." + txtDesigName.Text + "','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }

                    getdesignationlist();

                    MessageBox.Show("Designation Deleted successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
                finally
                {

                }
            }
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
            dataGridView1.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboGetSubDepart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void getdesignationlist()
        {
            try
            {
                SqlConnection conn = new SqlConnection(SqlconnPayroll.sbpayroll);
                SqlCommand cmd = new SqlCommand("select row_number() over(order by(des_code)) as [S.No],des_code as Code,designation.desc1 as [Designation Name],department.desc1 as [Department Name],sdep_desc as [Sub Department Name] from designation join department on department.dep_code=designation.dep_code join subdept on designation.sdep_code=subdept.sdep_code  order by designation.desc1", conn);
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

        private void createdesignation()
        {
            if (txtDesigName.Text == "" || cboGetDpartList.SelectedIndex == 0 || cboGetSubDepart.SelectedIndex == 0)
            {
                MessageBox.Show("Please enter designation name and select respective department and sub department then click on save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("createnewdesignation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desc1", txtDesigName.Text.Trim());
                    cmd.Parameters.AddWithValue("@depcode", cboGetDpartList.SelectedValue);
                    cmd.Parameters.AddWithValue("@sdepcode", cboGetSubDepart.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("You have successfuly created New Designation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getdesignationlist();
                    dataGridView1.Enabled = true;
                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();
                        //string pcusername = myglobalvar.strUsername.ToString().Trim();
                        //string pcname = myglobalvar.strPCName.ToString().Trim();
                        //string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
                        //string username = myglobalvar.selecteduser.ToString().Trim();
                        //int selectedcompany = myglobalvar.selectedcompany;

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Save button on the designation master page to create new designation with name " + txtDesigName.Text + " .','" + code.selectedcompany1 + "','" + u_pwd + "')");
                        txtDesigName.Text = "";
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

        private void getdepartmentlist()
        {
            try
            {
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand("select dep_code,desc1 from department", conn);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cboGetDpartList.DisplayMember = "desc1";
                cboGetDpartList.ValueMember = "dep_code";
                cboGetDpartList.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Please Select-" };
                dt.Rows.InsertAt(dr, 0);
                cboGetDpartList.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void cboGetDpartList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboGetSubDepart.Items.Clear();
            try
            {
               
                DataRow dr;
                //SqlConnection conn2 = new SqlConnection(sbmmc.ConnectionString);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand("select sdep_code,sdep_desc from subdept where dep_code=@depcode", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@depcode",cboGetDpartList.SelectedValue);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cboGetSubDepart.DisplayMember = "sdep_desc";
                cboGetSubDepart.ValueMember = "sdep_code";
                cboGetSubDepart.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Please Select-" };
                dt.Rows.InsertAt(dr, 0);
                cboGetSubDepart.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                txtDesigName.ReadOnly = true;
                txtDesigName.Text = selectedRow.Cells[2].Value.ToString();
                textBox1.Text = selectedRow.Cells[2].Value.ToString();
                label4.Text = selectedRow.Cells[1].Value.ToString();
                cboGetDpartList.Text = selectedRow.Cells[3].Value.ToString();
                cboGetDpartList.Enabled = false;
                cboGetSubDepart.Text = selectedRow.Cells[4].Value.ToString();
                cboGetSubDepart.Enabled = false;

            }
        }

        private void txtDesigName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
