using Dapper;
using DotNetTrainingBatch5.ConsoleApp.Models;
using DotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True";
        private readonly DapperService _dapperService;
        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach (var blog in lst)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
            }
        }
        public void Edit(int id)
        {
            string query = $@"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new { BlogId = id });
            if (item is null)
            {
                Console.WriteLine("Blog not found.");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
   
        }   
        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                ([BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent],[DeleteFlag])
             VALUES
                (@BlogTitle,@BlogAuthor,@BlogContent,0
                )";
            int result = _dapperService.ExecuteQuery(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
        }
        

    }
}
