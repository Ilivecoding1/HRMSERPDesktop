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
    public partial class Subdepartment : Form
    {
        SqlConnection conn = new SqlConnection(SqlconnPayroll.sbpayroll);
        public Subdepartment()
        {
            InitializeComponent();
        }

        private void Subdepartment_Load(object sender, EventArgs e)
        {
            getsubdepartmentlist();
            getdepartmentlist();
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            createsubdepartment();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtSubDeptName.Text == "")
            {
                MessageBox.Show("Please Enter a valid sub department name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("updatesubdept", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subdep", txtSubDeptName.Text);
                    cmd.Parameters.AddWithValue("@depcode", cboGetDpartList.SelectedValue);
                    cmd.Parameters.AddWithValue("@subdepcode", Convert.ToInt32(label3.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','An Existing Sub Department modified with new name." + txtSubDeptName.Text + " where its old name was" + textBox1.Text + "','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }

                    getsubdepartmentlist();
                    cboGetDpartList.Enabled = false;
                    btnUpdate.Visible = false;
                    btnModify.Visible = true;
                    btnCancel.Visible = false;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;
                    dataGridView1.Enabled = true;

                    MessageBox.Show("Sub-Department Updated successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    SqlCommand cmd = new SqlCommand("delsubdept", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subdepcode", Convert.ToInt32(label3.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','A Sub Department deleted with name." + txtSubDeptName.Text + "','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }

                    getsubdepartmentlist();

                    MessageBox.Show("Sub-Department Deleted successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnModify_Click(object sender, EventArgs e)
        {
            btnModify.Visible = false;
            txtSubDeptName.ReadOnly = false;
            cboGetDpartList.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            dataGridView1.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
            txtSubDeptName.ReadOnly = false;
            cboGetDpartList.Enabled = true;
            
            txtSubDeptName.Text = "";
            btnAdd.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        public void getsubdepartmentlist()
        {
            try
            {
                SqlConnection conn = new SqlConnection(SqlconnPayroll.sbpayroll);
                SqlCommand cmd = new SqlCommand("select row_number() over(order by(sdep_code)) as [S.No],sdep_code as Code,sdep_desc as [Sub Department Name],desc1 as [Department Name] from subdept join department on department.dep_code=subdept.dep_code  order by sdep_desc", conn);
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

        private void createsubdepartment()
        {
            if (txtSubDeptName.Text == ""|| cboGetDpartList.SelectedIndex==0)
            {
                MessageBox.Show("Please enter sub department name and select respective department then click on save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("createnewsubdept", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desc1", txtSubDeptName.Text.Trim());
                    cmd.Parameters.AddWithValue("@depcode", cboGetDpartList.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("You have successfuly created New Sub-Department.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getsubdepartmentlist();
                    dataGridView1.Enabled = true;

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();
                       
                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','Clicked on Save button on the sub department master page to create new sub department with name " + txtSubDeptName.Text + " .','" + code.selectedcompany1 + "','" + u_pwd + "')");
                        txtSubDeptName.Text = "";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                txtSubDeptName.ReadOnly = true;
                txtSubDeptName.Text = selectedRow.Cells[2].Value.ToString();
                textBox1.Text = selectedRow.Cells[2].Value.ToString();
                label3.Text= selectedRow.Cells[1].Value.ToString();
                cboGetDpartList.Text = selectedRow.Cells[3].Value.ToString();
                cboGetDpartList.Enabled = false;
                
            }
        }
    }
}
