﻿using Dapper;
using DotNetTrainingBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=12345;";
        private List<dynamic> lst;

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM [dbo].[Tbl_Blog]  WHERE DeleteFlag = 0";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);

                }
            }

            //DTO => Data Transfer Object

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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
            }
        }

       public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0 and BlogId = @BlogId";
                //var lst = db.QueryFirstOrDefault<BlogDataModel>(query, new { BlogId = id });
                var item = db.Query<BlogDataModel>(query, new BlogDataModel { BlogId = id }).FirstOrDefault();
                if (item is null)
                {

                    Console.WriteLine("No Data Found");
                    return;
                }
                else
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
        }

        public void Update(int id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
            }
        
        }

        public void Delete(int id)
        {
            String query = $@"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId
";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel { BlogId = id });
                Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
            }
        }
    }
}
