using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace MYMLibrary
{
    public class OracleSQLConnector
    {
        public static String GetConnectionString()
        {
            return "User Id=projektcsoracle; Password=haslo2020; Data Source=localhost:1521/xe";
        }
    }
}
