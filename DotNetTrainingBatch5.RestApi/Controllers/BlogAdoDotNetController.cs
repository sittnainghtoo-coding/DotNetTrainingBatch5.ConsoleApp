using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.RestApi.DataModels;
using DotNetTrainingBatch5.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {

        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=12345;TrustServerCertificate=True";
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query ,connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            if(dt.Rows.Count == 0)
            {
                return NotFound("No blogs found.");
            }
            foreach(DataRow reader in dt.Rows)
            {
                BlogViewModel blog = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = reader["BlogTitle"].ToString(),
                    Author = reader["BlogAuthor"].ToString(),
                    Content = reader["BlogContent"].ToString(),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };
                lst.Add(blog);
            }
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where BlogId = @BlogId and DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            if (dt.Rows.Count == 0)
            {
                return NotFound("No blog found");
            }

            BlogViewModel blog = new BlogViewModel {
                Id = Convert.ToInt32(dt.Rows[0]["BlogId"]),
                Title = dt.Rows[0]["BlogTitle"].ToString(),
                Author = dt.Rows[0]["BlogAuthor"].ToString(),
                Content = dt.Rows[0]["BlogContent"].ToString(),
                DeleteFlag = Convert.ToBoolean(dt.Rows[0]["DeleteFlag"])
            };
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult Create(BlogViewModel blogs)
        {
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
            cmd.Parameters.AddWithValue("@BlogTitle", blogs.Id);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogs.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blogs.Content);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
           
            return Ok(result > 0 ? "Create successful" : "Create Failed");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,BlogViewModel blogs)
        {
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
            cmd.Parameters.AddWithValue("@BlogTitle", blogs.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogs.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blogs.Content);
            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            if(result > 0)
            {
                return Ok("Update Successful");
            }
            else
            {
                return BadRequest("Update Failed");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id , BlogViewModel blog)
        {

            string conditions = "";
            if(!string.IsNullOrEmpty(blog.Title))
            {
                conditions += "BlogTitle = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += "BlogAuthor = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += "BlogContent = @BlogContent, ";
            }

            if(conditions.Length == 0)
            {
                //conditions = conditions.TrimEnd(',', ' ');
                return BadRequest("Please provide at least one field to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2); // Remove the last comma and space


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);

            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            return Ok(result>0 ? "Updateing Successful" : "Updating Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"DELETE FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            return Ok(result > 0 ? "Delete Successful" : "Delete Failed");
        }
    }
}
