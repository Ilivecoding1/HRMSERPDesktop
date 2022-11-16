using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DeepakERPSystem
{
   public class SqlconnMain
    {
       
public static String serverfile = @"C:\\mmc\\server.txt";
        public static StreamReader txtfile = new StreamReader(serverfile);
        
        public static string servername = txtfile.ReadLine();
        public static string username = txtfile.ReadLine();
        public static string password = txtfile.ReadLine();

        public static string sbmmc = ("Data Source=" + servername + ";Initial Catalog=mmc" + ";User Id=" + username + ";Password=" + password + ";");
        public static SqlConnection GetMMCDBConnection()
        {
            SqlConnection conn = new SqlConnection(sbmmc);
            return conn;
        }

    }


   
  public  class SqlconnPayroll
    {
        public static String serverfile = @"C:\\mmc\\server.txt";



        public static StreamReader txtfile = new StreamReader(serverfile);
        public static string servername = txtfile.ReadLine();
        public static string username = txtfile.ReadLine();
        public static string password = txtfile.ReadLine();

        public static string sbpayroll = ("Data Source=" + servername + ";Initial Catalog=payroll"+"_" + myglobalvar.selectedcompany + "_" + myglobalvar.selectedfinyear.ToString().Substring(2, 2) + ";User Id=" + username + ";Password=" + password + ";");

        public static SqlConnection GetCurrentPayrollDBConnection()
        {
            SqlConnection conn = new SqlConnection(sbpayroll);
            return conn;
        }


    }

   public class SqlconnPayrollbackdb
    {
        public static String serverfile = @"C:\\mmc\\server.txt";
        
        public static StreamReader txtfile = new StreamReader(serverfile);
        public static string servername = txtfile.ReadLine();
        public static string username = txtfile.ReadLine();
        public static string password = txtfile.ReadLine();

        public static string sbbackpayroll = ("Data Source=" + servername + ";Initial Catalog=payroll" + "_" + myglobalvar.selectedcompany + "_" + myglobalvar.selectedonebackfinyear.ToString().Substring(2, 2) + ";User Id=" + username + ";Password=" + password + ";");

        public static SqlConnection GetBackYrPayrollDBConnection()
        {
            SqlConnection conn = new SqlConnection(sbbackpayroll);
            return conn;
        }


    }

 
}