// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.WriteLine();

//string connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=12345;";
//Console.WriteLine("connection String: " + connectionString);
//SqlConnection connection = new SqlConnection(connectionString);

//Console.WriteLine("connection Opening...");
//connection.Open();
//Console.WriteLine("connection Opened.");

//string query = @"SELECT [BlogId]
//      ,[BlogTitle]
//      ,[BlogAuthor]
//      ,[BlogContent]
//      ,[DeleteFlag]
//  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//adapter.Fill(dt);


//Console.WriteLine("Connection Closing...");

//connection.Close();
//Console.WriteLine("Connection Closed");

////DataSet -> DataTable -> DataRow -> DataColumn


//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogId"]);
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);

//}


string connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=12345";
SqlConnection connection = new SqlConnection(connectionString);

connection.Open();

String query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

SqlCommand cmd = new SqlCommand(query, connection);
SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine("BlogId: " + reader["BlogId"]);
    Console.WriteLine("BlogTitle: " + reader["BlogTitle"]);
    Console.WriteLine("BlogAuthor: " + reader["BlogAuthor"]);
    Console.WriteLine("BlogContent: " + reader["BlogContent"]);
    Console.WriteLine("-----------------------------");
}

connection.Close();



Console.ReadKey();
