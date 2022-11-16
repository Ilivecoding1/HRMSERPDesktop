using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net; // to get system ip
using System.IO;
using System.Runtime.InteropServices;//to get and set date time..
using System.Diagnostics; //
using System.Data;
using System.Data.SqlClient;

namespace DeepakERPSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new EmployeeImport());

            try
            {
                //////
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = DateTime.Parse("31/12/2022");
                if (dt1.Date > dt2.Date)
                { // Alert Message based on your requirement. 
                    MessageBox.Show("Dear User,Software License is expired, Please contact to your software vendor[Error Code 1033] for more info go to https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/mssqlserver-18456-database-engine-error?view=sql-server-ver15", "Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (File.Exists(SqlconnMain.serverfile))
                {

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Login_Page());
                }
                else
                {
                    //MessageBox.Show("Server doesn't exists or access denied.Look for server file in C:\\mmc folder.", "Error in Server[Server Not Found]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show("Server doesn't exists or access denied.Look for server file in C:\\mmc folder.", "Error in Server[Server Not Found]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw (ee);

            }



        }
    }
    public class myglobalvar
    {
        // some imp variables to use in inserting audit data.
       public string pcusername = myglobalvar.strUsername.ToString().Trim();
       public string pcname = myglobalvar.strPCName.ToString().Trim();
       public string ipaddress = myglobalvar.strIPAddress.ToString().Trim();
       public string username = myglobalvar.selecteduser.ToString().Trim();
       public string selectedcompany1 = myglobalvar.selectedcompany;

        //
        public static String cmpcode;
        
      //fun for inserting auditdata into the table
      public void Execute(string SQLAuditData)
        {
            try
            {
                
                SqlConnection conn = new SqlConnection(SqlconnMain.sbmmc);
                DataTable dtt = new DataTable();
                SqlDataAdapter sdaa = new SqlDataAdapter(SQLAuditData, conn);
                sdaa.Fill(dtt);
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        //
        
            //
        //for username
        public static string strUsername = System.Windows.Forms.SystemInformation.UserName.ToString();
        //for computer name
        public static string strPCName = Dns.GetHostName();
        //variables global variables for getting ip address.
        public static IPHostEntry strHostname =Dns.GetHostEntry(Dns.GetHostName());
        public static string strIPAddress;

        //
        static string Selectedcompany;
        public static string selectedcompany
        {
            get
            {
                return Selectedcompany;
            }
            set
            {
                Selectedcompany = value;
            }
        }
        static string Selectedfinyear;
        public static string selectedfinyear
        {
            get
            {
                return Selectedfinyear;
            }
            set
            {
                Selectedfinyear = value;
            }
        }

        static string SelectedoneBackfinyear;
        public static string selectedonebackfinyear
        {
            get
            {
                return SelectedoneBackfinyear;
            }
            set
            {
                SelectedoneBackfinyear = value;
            }
        }

        public static int utype;
        static string SelectedUsertype;
        public static string selectedusertype
        {
            get
            {
                return SelectedUsertype;
            }
            set
            {
                SelectedUsertype = value;
            }
        }
        static string SelectedUser;
        public static string selecteduser
        {
            get
            {
                return SelectedUser;
            }
            set
            {
                SelectedUser = value;
            }
        }

        static string SelectedCompanyname;
        public static string selectedcompanyname
        {
            get
            {
                return SelectedCompanyname;
            }
            set
            {
                SelectedCompanyname = value;
            }
        }
        //
      
        
    }

    //class end
}
