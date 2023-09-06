using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.EnterpriseServices;
using System.Xml.Linq;

namespace Store
{
    public class ClsBasic
    {
        SqlConnection sqlConnection;
        SqlCommand sqlDbCommand;
        SqlDataAdapter sqlDbDataAdapter;
        DataTable dataTable;
        SqlConnection OpenDataBase()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbUsGroupKw"].ConnectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            return sqlConnection;
        }
        void CloseDataBase(SqlConnection oleDb)
        {
            if (oleDb.State == ConnectionState.Open)
            {
                oleDb.Close();
            }
        }
        public DataTable SelectData(string column, string table)
        {
            dataTable = new DataTable();
            var conDb = OpenDataBase();
            sqlDbCommand = new SqlCommand($"SELECT {column} FROM {table}", conDb);
            sqlDbDataAdapter = new SqlDataAdapter(sqlDbCommand);
            sqlDbDataAdapter.Fill(dataTable);
            CloseDataBase(conDb);
            return dataTable;
        }

        public void NewDept(string name, byte[] image)
        {
            var conDb = OpenDataBase();
            sqlDbCommand = new SqlCommand("INSERT INTO Dept(Name, Photo) VALUES(@name, @pic);" ,conDb);
            sqlDbCommand.Parameters.AddWithValue("@name", name);
            sqlDbCommand.Parameters.AddWithValue("@pic", image);
            sqlDbCommand.ExecuteNonQuery();
            CloseDataBase(conDb);
        }

        public void EditUser(string name, string email, string pswd)
        {
            var conDb = OpenDataBase();
            sqlDbCommand = new SqlCommand($"UPDATE [Login] SET Name = '{name}', Email = '{email}', Password = '{pswd}' WHERE Id = 1", conDb);
            sqlDbCommand.ExecuteNonQuery();
            conDb.Close();
        }
    }
}