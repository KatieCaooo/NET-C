using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectViewScore.sample.connection
{
    public class MyConnection
    {
        public static SqlConnection getMyConnection()
        {
            string connection = @"server = localhost; User ID = sa; Password = 123456; database=QLSVien";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            return con;           
        }
    }
}
