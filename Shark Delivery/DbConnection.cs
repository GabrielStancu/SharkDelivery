using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Shark_Delivery
{
    public class DbConnection
    {
        private SqlConnection conn;
        private string connStr;

        public void OpenConnection()
        {
            connStr = @"Server=ASUS-ROG\SQLEXPRESS;Database=DeliveryDb;User Id=Gabi;Password = acon; ";
            conn = new SqlConnection(connStr);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public SqlConnection GetConnection()
        {
            return this.conn;
        }
    }
}
