using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.EnterpriseServices;
using System.Xml.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Store
{
    public class ClsBasic
    {
        SqlConnection sqlConnection;
        SqlCommand sqlDbCommand;
        SqlDataAdapter sqlDbDataAdapter;
        DataTable dataTable;
        DataSet dataSet;
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

        public DataSet SelectMultiple(string queries)
        {
            dataSet = new DataSet();
            var conDb = OpenDataBase();
            sqlDbCommand = new SqlCommand(queries, conDb);
            sqlDbDataAdapter = new SqlDataAdapter(sqlDbCommand);
            sqlDbDataAdapter.Fill(dataSet);
            CloseDataBase(conDb);
            return dataSet;
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

        public void EditUser(int id, string name, string email, string pswd)
        {
            var conDb = OpenDataBase();
            sqlDbCommand = new SqlCommand($"UPDATE [Login] SET Name = '{name}', Email = '{email}', Password = '{pswd}' WHERE Id = " + id, conDb);
            sqlDbCommand.ExecuteNonQuery();
            conDb.Close();
        }

        public void newOrder(string dvInfoProduct, List<string> list)
        {
            var conDb = OpenDataBase();
            sqlDbCommand = new SqlCommand("INSERT INTO Client([Name], [Phone], [Address], [DateOr], Piece, Home, Street) VALUES(@name, @phone, @address, GETDATE(), @piece, @home, @street); SELECT SCOPE_IDENTITY();", conDb);
            sqlDbCommand.Parameters.AddWithValue("@name", list[0]);
            sqlDbCommand.Parameters.AddWithValue("@phone", list[1]);
            sqlDbCommand.Parameters.AddWithValue("@address", list[2]);
            sqlDbCommand.Parameters.AddWithValue("@piece", list[3]);
            sqlDbCommand.Parameters.AddWithValue("@home", list[4]);
            sqlDbCommand.Parameters.AddWithValue("@street", list[5]);
            string pkClient = sqlDbCommand.ExecuteScalar().ToString();
            sqlDbCommand = new SqlCommand("INSERT INTO Orders([Cnt], [Total], [PId], [CId]) VALUES(@cnt, @total, @pid, @cid);", conDb);
            SqlCommand sqlCommand = new SqlCommand("UPDATE Product SET Cnt = Cnt - @cnt WHERE Id = @pid", conDb);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(dvInfoProduct);
            var headers = document.DocumentNode.SelectNodes("//div[@class='elementCart flex mt-2']");
            if (headers != null)
            {
                string dataId, count, total;
                foreach (HtmlNode header in headers)
                {
                    dataId = header.Attributes["data-id"].Value;
                    try
                    {
                        count = header.SelectSingleNode($".//div//div//div[@class='flex flex-row qty h-9']//span[@class='flex justify-center items-center w-11 text-center numberQty']").InnerText;
                    }
                    catch
                    {
                        count = header.SelectSingleNode($".//div//div//div[@class='flex flex-row-reverse qty h-9']//span[@class='flex justify-center items-center w-11 text-center numberQty']").InnerText;
                    }
                    total = header.SelectSingleNode(".//div//div//div[@class='suptotalTotalPrice']//span").InnerText;
                    sqlDbCommand.Parameters.AddWithValue("@cnt", count);
                    sqlDbCommand.Parameters.AddWithValue("@total", total);
                    sqlDbCommand.Parameters.AddWithValue("@pid", dataId);
                    sqlDbCommand.Parameters.AddWithValue("@cid", pkClient);
                    sqlDbCommand.ExecuteNonQuery();
                    sqlDbCommand.Parameters.Clear();

                    sqlCommand.Parameters.AddWithValue("@cnt", count);
                    sqlCommand.Parameters.AddWithValue("@pid", dataId);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                }
            }
            conDb.Close();
        }
    }
}