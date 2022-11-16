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
    public partial class UserLogReports : Form
    {
        SqlConnection conmmc = new SqlConnection(SqlconnMain.sbmmc);
        public UserLogReports()
        {
            InitializeComponent();
        }

        private void UserLogReports_Load(object sender, EventArgs e)
        {
            getuerlist();
        }

        private void btnViewRecord_Click(object sender, EventArgs e)
        {
            if (cboGetUserList.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a username to view activity log report","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else {
                try
                {
                    if (conmmc.State == ConnectionState.Closed)
                        conmmc.Open();
                    SqlCommand cmd = new SqlCommand("getlogreport", conmmc);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@from", DateTime.Parse(dateTimePicker1.Text.ToString()));
                    cmd.Parameters.AddWithValue("@to", DateTime.Parse(dateTimePicker2.Text.ToString()));
                    cmd.Parameters.AddWithValue("@usrname", cboGetUserList.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            

        }
        private void getuerlist()
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
                cboGetUserList.DisplayMember = "usr_name";
                cboGetUserList.ValueMember = "usr_code";
                cboGetUserList.DataSource = dt;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-Please Select Username-" };
                dt.Rows.InsertAt(dr, 0);
                cboGetUserList.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            //conn1.Close();
        }

        private void exportExl_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                GridToExcelExport();
                MessageBox.Show("User Activity Log Report Export completed check in c:\\mmc\\ location", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No Record To Export in Excel","Information",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
        }
        private void GridToExcelExport()
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "UserActivityLog";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            workbook.SaveAs("c:\\mmc\\UserActivityLog.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();

        }
    }
}
