using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop.Excel;
//using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

namespace DeepakERPSystem
{
    public partial class EmployeeImport : Form
    {
        SqlConnection conn1 = SqlconnPayroll.GetCurrentPayrollDBConnection();
        public EmployeeImport()
        {
            InitializeComponent();
        }

        // CREATE EXCEL OBJECTS.
        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;

        string sFileName;

        
        private void btnBrows_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Excel File to Upload Data";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Excel File|*.xlsx;*.xls";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFileName = openFileDialog1.FileName;
                txtexcelpath.Text = openFileDialog1.FileName;
                // if (sFileName.Trim() != "")
                // {
                //   Excel2Grid(sFileName);
                //}
            }

        }
               

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("Please click on select excel file to select an excel file filled with employee details then click on upload and import", "Select Excel File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else 
            {
                GridToDatabase();
            }
        }

        private void btnUploadExcel_Click(object sender, EventArgs e)
        {
            //sFileName = //txtexcelpath.Text.Trim();
            if (txtexcelpath.Text.Trim() == "")
            {
                MessageBox.Show("Please click on select excel file to select an excel file filled with employee details then click on upload and import", "Select Excel File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (sFileName.Trim() != "")
            {
                Excel2Grid(sFileName);
            }
            
        }
        //
        // IMPORT DATA FROM EXCEL AND POPULATE THE GRID.
        private void Excel2Grid(string sFile)
        {
            if (conn1.State == ConnectionState.Closed)
                conn1.Open();
            SqlCommand cmd1 = new SqlCommand("create table temp_dept(depcode int identity(1,1),deptname varchar(150));create table temp_desig(descode int identity(1,1),designame varchar(150))", conn1);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
            //SqlCommand cmd2 = new SqlCommand("create table temp_desig(descode int identity(1,1),designame varchar(150))", conn1);
            //cmd2.CommandType = CommandType.Text;
            //cmd2.ExecuteNonQuery();

            btnUploadExcel.Text = "Processing please wait..";
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(sFile);               // WORKBOOK TO OPEN THE EXCEL FILE.
                xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];          // THE SHEET WITH THE DATA.

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                int iRow, iCol;

                // FIRST, CREATE THE DataGridView COLUMN HEADERS.
                for (iCol = 1; iCol <= xlWorkSheet.Columns.Count; iCol++)
                {
                    label2.Text = "Column :" + " " + Convert.ToString(iCol);
                    if (xlWorkSheet.Cells[1, iCol].value == null)
                    {
                        break;      // BREAK LOOP.
                    }
                    else
                    {
                        DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                        col.HeaderText = xlWorkSheet.Cells[1, iCol].value;
                        int colIndex = dataGridView1.Columns.Add(col);        // ADD A NEW COLUMN.
                    }
                    
                }
                
                //// ADD A BUTTON AT THE LAST COLUMN IN EVERY ROW.
                //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                //btn.HeaderText = "";
                //btn.Text = "Save Data";
                //btn.Name = "btSave";
                //btn.UseColumnTextForButtonValue = true;
                //dataGridView1.Columns.Add(btn);

                // ADD ROWS TO THE GRID USING EXCEL DATA.
                for (iRow = 2; iCol <= xlWorkSheet.Rows.Count; iRow++)
                {
                    label3.Text = "Rows :" + " " + Convert.ToString(iRow);
                    if (xlWorkSheet.Cells[iRow, 2].value == null)
                    {
                        break;      // BREAK LOOP.
                    }
                    else
                    {
                        // CREATE A STRING ARRAY USING THE VALUES IN EACH ROW OF THE SHEET.
                       
                        string[] row = new string[] {Convert.ToString(xlWorkSheet.Cells[iRow, 1].value),
                    Convert.ToString(xlWorkSheet.Cells[iRow, 2].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 3].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 4].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 5].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 6].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 7].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 8].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 9].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 10].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 11].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 12].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 13].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 14].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 15].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 16].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 17].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 18].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 19].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 20].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 21].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 22].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 23].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 24].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 25].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 26].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 27].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 28].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 29].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 30].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 31].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 32].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 33].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 34].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 35].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 36].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 37].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 38].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 39].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 40].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 41].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 42].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 43].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 44].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 45].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 46].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 47].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 48].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 49].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 50].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 51].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 52].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 53].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 54].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 55].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 56].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 57].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 58].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 59].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 60].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 61].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 62].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 63].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 64].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 65].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 66].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 67].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 68].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 69].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 70].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 71].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 72].value),
                     Convert.ToString(xlWorkSheet.Cells[iRow, 73].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 74].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 75].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 76].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 77].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 78].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 79].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 80].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 81].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 82].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 83].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 84].value),
                        Convert.ToString(xlWorkSheet.Cells[iRow, 85].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 86].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 87].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 88].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 89].value),
                       Convert.ToString(xlWorkSheet.Cells[iRow, 90].value) };

                        // ADD A NEW ROW TO THE GRID USING THE ARRAY DATA.
                        dataGridView1.Rows.Add(row);
                        
                    }
                    
                }

                xlWorkBook.Close();
                xlApp.Quit();
            }
            catch (Exception EX)
            {
                throw EX;
                //MessageBox.Show(EX.ToString(), "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUploadExcel.Text = "Upload && View Excel File";
                // CLEAN UP.
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
            }
        }
        //
        //Import data
        private void GridToDatabase()
        {
            btnImport.Text = "Processing please wait..";
            try
            {
               
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (conn1.State == ConnectionState.Closed)
                        conn1.Open();
                        SqlCommand cmd = new SqlCommand("select pay_code from employee where pay_code=@paycode", conn1);
                        cmd.Parameters.AddWithValue("@paycode", Convert.ToString(row.Cells[1].Value));
                        SqlDataReader rdr = cmd.ExecuteReader();
                      
                    if (rdr.HasRows)
                    {
                        
                        MessageBox.Show("Duplicate Paycode or Card No found please remove duplicate data and try again.!!","Duplicate Record Found", MessageBoxButtons.OK,MessageBoxIcon.Stop );
                       // break;
                        conn1.Close();
                        rdr.Close();
                    }
                    else
                    {
                        rdr.Close();
                        SqlCommand cmdemp = new SqlCommand("spInsertEmpData", conn1);
                        cmdemp.CommandType = CommandType.StoredProcedure;
                        //for personal details from 0 to 45
                        cmdemp.Parameters.AddWithValue("@empcode", Convert.ToInt32(row.Cells[0].Value));
                        cmdemp.Parameters.AddWithValue("@pay_code", Convert.ToString(row.Cells[1].Value));
                        cmdemp.Parameters.AddWithValue("@card_no", Convert.ToString(row.Cells[2].Value));
                        cmdemp.Parameters.AddWithValue("@name", Convert.ToString(row.Cells[4].Value));
                        cmdemp.Parameters.AddWithValue("@fname", Convert.ToString(row.Cells[5].Value));
                        cmdemp.Parameters.AddWithValue("@tempdepname", Convert.ToString(row.Cells[11].Value)); //temptab
                        cmdemp.Parameters.AddWithValue("@subcode", Convert.ToInt32(row.Cells[3].Value));
                        cmdemp.Parameters.AddWithValue("@m_name", Convert.ToString(row.Cells[6].Value));
                        cmdemp.Parameters.AddWithValue("@w_name", Convert.ToString(row.Cells[7].Value));
                        //DateTime dobb = Convert.ToDateTime(row.Cells[8].Value.ToString());
                        cmdemp.Parameters.AddWithValue("@dob", Convert.ToDateTime(row.Cells[8].Value.ToString()));
                        //DateTime dojj = Convert.ToDateTime(row.Cells[9].Value.ToString());
                        cmdemp.Parameters.AddWithValue("@doj", Convert.ToDateTime(row.Cells[9].Value.ToString()));
                        //DateTime dojj1 = Convert.ToDateTime(row.Cells[9].Value.ToString());
                        cmdemp.Parameters.AddWithValue("@doj1", Convert.ToDateTime(row.Cells[9].Value.ToString()));
                        //DateTime rdate = Convert.ToDateTime(row.Cells[10].Value.ToString());
                        cmdemp.Parameters.AddWithValue("@r_date", Convert.ToDateTime(row.Cells[10].Value.ToString()));
                        cmdemp.Parameters.AddWithValue("@tempdesigname", Convert.ToString(row.Cells[12].Value));// temptab



                        //for biodata 55 to 90
                        //cmdemp.Parameters.AddWithValue("@empcode", Convert.ToInt32(row.Cells[0].Value));
                        //cmdemp.Parameters.AddWithValue("@pay_code", Convert.ToString(row.Cells[1].Value));
                        //cmdemp.Parameters.AddWithValue("@card_no", Convert.ToString(row.Cells[2].Value));
                        //cmdemp.Parameters.AddWithValue("@name", Convert.ToString(row.Cells[4].Value));
                        //cmdemp.Parameters.AddWithValue("@fname", Convert.ToString(row.Cells[5].Value));

                        //for salary rate table from 46 to 54 column
                        //cmdemp.Parameters.AddWithValue("@empcode", Convert.ToInt32(row.Cells[0].Value));
                        //cmdemp.Parameters.AddWithValue("@empcode", Convert.ToInt32(row.Cells[0].Value));
                        //cmdemp.Parameters.AddWithValue("@pay_code", Convert.ToString(row.Cells[1].Value));
                        //cmdemp.Parameters.AddWithValue("@card_no", Convert.ToString(row.Cells[2].Value));
                        //cmdemp.Parameters.AddWithValue("@name", Convert.ToString(row.Cells[4].Value));
                        //cmdemp.Parameters.AddWithValue("@fname", Convert.ToString(row.Cells[5].Value));

                        if (conn1.State == ConnectionState.Closed)
                            conn1.Open();
                        cmdemp.ExecuteNonQuery();
                        
                    }
                    //MessageBox.Show("Data Imported to database successfuly.", "Successful Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                MessageBox.Show("Data Imported to database successfuly.", "Successful Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception EX)
            {
                //throw EX;
                MessageBox.Show(EX.ToString(), "Oops error in import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                btnImport.Text = "Import Excel Data";
                conn1.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (txtexcelpath.Text=="")
            {
                MessageBox.Show("Please select excle file and click on upload then click on this button", "Message", MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
               
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        SqlCommand cmd = new SqlCommand("spInsertOtherDetailsFirstTime", conn1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tempdeptname", Convert.ToString(row.Cells[11].Value));
                        cmd.Parameters.AddWithValue("@tempdesigname", Convert.ToString(row.Cells[12].Value));
                        if (conn1.State == ConnectionState.Closed)
                            conn1.Open();
                        cmd.ExecuteNonQuery();
                        conn1.Close();


                    }
                    MessageBox.Show("table created and data loaded into table and transfered into actual department", "Message", MessageBoxButtons.OK);
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    if (conn1.State == ConnectionState.Closed)
                        conn1.Open();
                    SqlCommand cmd1 = new SqlCommand("drop table temp_dept;drop table temp_desig", conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
               
            }
           
        }
        //

    }
}
