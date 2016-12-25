using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Designer
{
    class DBAccess
    {
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\ALEXE\\DESKTOP\\123.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlConnection connection;
        
        public DBAccess()
        {
            connection = new SqlConnection(connectionString);
            
        }

        public DataTable GetData(string nameTable)
        {
            DataTable table = new DataTable();

            SqlCommand com = new SqlCommand("SELECT * FROM " + nameTable, connection);
            connection.Open();
            SqlDataReader data = com.ExecuteReader(CommandBehavior.CloseConnection);
            
            table.Load(data);
            connection.Close();
            return table;
            
        }

        public bool InsertData(string nameTable, List<SaveData> data)
        {
            SqlCommand com = connection.CreateCommand();
            string comString = "INSERT INTO " + nameTable + "(";
            string values = " VALUES(";
            for(int i = 0;i < data.Count;i++)
            {
                comString += data[i].NameColumn;
                if (i + 1 < data.Count)
                    comString += ",";
                values += "@param" + i;
                if(i + 1 < data.Count)
                    values += ",";
                com.Parameters.Add(new SqlParameter("@param" + i, data[i].Value));

            }
            comString += ")";
            comString += values + ")";
            com.CommandText = comString;
            connection.Open();
           
            int res = com.ExecuteNonQuery();
            connection.Close();
            return res == 1;
            
            
        }


        
    }
}
