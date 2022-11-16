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
    public partial class NewForm1 : Form
    {
        SqlConnection conn =new SqlConnection(SqlconnPayroll.sbpayroll);
       
        public NewForm1()
        {
        
            InitializeComponent();
        
    }

        private void Department_Load(object sender, EventArgs e)
        {
            getdepartmentlist();
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
        }
        public void getdepartmentlist()
        {
            try
            {
                SqlConnection conn = new SqlConnection(SqlconnPayroll.sbpayroll);
                SqlCommand cmd = new SqlCommand("select row_number() over(order by(dep_code)) as [S.No],dep_code as Code,desc1 as [Department Name] from department order by desc1", conn);
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

        private void createdepartment()
        {
            if (txtDeptName.Text == "" )
            {
                MessageBox.Show("Please enter department name then click on save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {
                    
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("createnewdept", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desc1", txtDeptName.Text.Trim());
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("You have successfuly created a new Department.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getdepartmentlist();
                    dataGridView1.Enabled = true;
                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();
                        
                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','A New Department was created with name " + txtDeptName.Text + " .','" + code.selectedcompany1 + "','" + u_pwd + "')");
                        txtDeptName.Text = "";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            createdepartment();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
            txtDeptName.ReadOnly = false;
            txtDeptName.Text = "";
            btnAdd.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            btnModify.Visible = false;
            txtDeptName.ReadOnly = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            dataGridView1.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
            txtDeptName.ReadOnly = true;
                txtDeptName.Text = selectedRow.Cells[2].Value.ToString();
            textBox1.Text = selectedRow.Cells[2].Value.ToString();
            label1.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            
            if (txtDeptName.Text == "")
            {
                MessageBox.Show("Please Enter a valid department name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("updatedept", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@newdept", txtDeptName.Text);
                    cmd.Parameters.AddWithValue("@depcode", Convert.ToInt32(label1.Text));
                    cmd.Parameters.AddWithValue("@olddept", textBox1.Text.Trim());
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();
                        
                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','A Department modified with new name."+txtDeptName.Text+" where its old name was"+textBox1.Text+"','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }

                    getdepartmentlist();
                    btnUpdate.Visible = false;
                    btnModify.Visible = true;
                    btnCancel.Visible = false;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;
                    dataGridView1.Enabled = true;

                    MessageBox.Show("Department Updated successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
          
              
          
           if (MessageBox.Show(string.Format("Are you sure you want to delete this department: "+textBox1.Text), "Delete Confirmation", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                try
                {

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("deldept", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@depcode", Convert.ToInt32(label1.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    try
                    {
                        int u_pwd = 0;
                        myglobalvar code = new myglobalvar();

                        code.Execute(@"insert into usr_log (log_date,sys_name,sys_ip,usr_name,desc1,cmp_code,u_pwd)values(GETDATE(),'" + code.pcname + "/" + code.pcusername + "','" + code.ipaddress + "','" + code.username + "','A Department deleted with name." + txtDeptName.Text + "','" + code.selectedcompany1 + "','" + u_pwd + "')");
                    }
                    catch (InvalidCastException ee)
                    {
                        throw (ee);
                    }

                    getdepartmentlist();
                    
                    MessageBox.Show("Department Deleted successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
    }
}
