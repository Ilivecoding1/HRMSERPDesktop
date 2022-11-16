using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DeepakERPSystem
{
    class SQLConnMethod2
    {
        public static String serverfile = @"C:\\mmc\\server.txt";
        public static StreamReader txtfile = new StreamReader(serverfile);

        public static string servername = txtfile.ReadLine();
        public static string username = txtfile.ReadLine();
        public static string password = txtfile.ReadLine();

        public static string sbmaster = ("Data Source=" + servername + ";Initial Catalog=master" + ";User Id=" + username + ";Password=" + password + ";");

        public static SqlConnection GetMasterConDB()
        {
            SqlConnection conn = new SqlConnection(sbmaster);
            return conn;
        }

    }
}
