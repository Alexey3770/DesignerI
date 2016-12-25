using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Designer.Foundation
{
    class DBAccess
    {
        string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBDesighner;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//"Data Source=ALEXEY\\SQLEXPRESS;Initial Catalog=Designer;Integrated Security=True;";
        const string FURNITURE_TABLE_NAME = "Furniture";
        SqlConnection connection;
        
        public DBAccess()
        {
            connection = new SqlConnection(CONNECTION_STRING);
            
        }

        public List<string> GetData(string nameTable,int numColumn)
        {
            List<string> outMass = new List<string>();
            SqlCommand com = new SqlCommand("SELECT * FROM @param1", connection);
            com.Parameters.Add(new SqlParameter("@param1", nameTable));
            connection.Open();
            SqlDataReader data = com.ExecuteReader(CommandBehavior.CloseConnection);
            while (data.Read())
            {
                outMass.Add(data.GetString(numColumn));
            }
            connection.Close();
            return outMass;
            
        }
        public DataTable GetData(string nameTable)
        {
            DataTable table = new DataTable();
            SqlCommand com = new SqlCommand("SELECT * FROM "+nameTable, connection);
            connection.Open();
            SqlDataReader data = com.ExecuteReader(CommandBehavior.CloseConnection);
            table.Load(data);
            connection.Close();
            return table;
        }
        public bool InsertCommand(string queury)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                try
                {
                    command.CommandText = queury;
                    command.ExecuteNonQuery();                   

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        public bool InsertTransaction(List<string> querys)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    foreach(string q in querys)
                    {
                        command.CommandText = q;
                        command.ExecuteNonQuery();
                    }

                    // подтверждаем транзакцию
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }

        public string GetModel3D(string nameModel)
        {
            DataTable table = new DataTable();

            SqlCommand com = new SqlCommand("SELECT * FROM"+ FURNITURE_TABLE_NAME+ " WHERE ", connection);
            com.Parameters.Add(new SqlParameter("@param1", nameModel));
            connection.Open();
            SqlDataReader data = com.ExecuteReader(CommandBehavior.CloseConnection);

            table.Load(data);
            connection.Close();
            return "";
        }
        
    }
}
