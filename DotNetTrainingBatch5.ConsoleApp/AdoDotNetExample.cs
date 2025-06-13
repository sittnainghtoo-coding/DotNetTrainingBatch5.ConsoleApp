using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=12345;";
        public void Read()
        {

            Console.WriteLine("connection String: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("connection Opening...");
            connection.Open();
            Console.WriteLine("connection Opened.");

            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            Console.WriteLine("Connection Closing...");

            connection.Close();
            Console.WriteLine("Connection Closed");

            //DataSet -> DataTable -> DataRow -> DataColumn


            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);

            }
        }
        public void Create() 
        {

            Console.WriteLine("Enter BlogTitle:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter BlogAuthor:");
            string author = Console.ReadLine();
            Console.WriteLine("Enter BlogContent:");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                ([BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent],[DeleteFlag])
    VALUES
                (@BlogTitle,@BlogAuthor,@BlogContent,0

)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@blogContent", content);


            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");


        }

        public void Edit()
        {
            Console.Write("Blog Id:");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);   
            connection.Close();
            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                Console.WriteLine("Blog Title: " + dr["BlogTitle"]);
                Console.WriteLine("Blog Author: " + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content: " + dr["BlogContent"]);
            }
        }

        public void Update()
        {
            Console.WriteLine("Blog Id :");
            string id = Console.ReadLine();

            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author :");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content :");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
            connection.Close();
        }

        public void Delete()
        {
            Console.WriteLine("Blog Id :");
            string id = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            String query = $@"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");

        }
        
    }
}
