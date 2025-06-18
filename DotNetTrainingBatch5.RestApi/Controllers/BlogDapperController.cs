using Dapper;
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
    public class BlogDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=12345;TrustServerCertificate=True";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0";
            using IDbConnection db = new SqlConnection(_connectionString);
            var blogs = db.Query<BlogDataModel>(query).ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
           string query = "SELECT * FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0 AND BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(_connectionString);
            var blog = db.Query<BlogDataModel>(query, new BlogDataModel { BlogId = id }).FirstOrDefault();

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
        {
            string query = "INSERT INTO [dbo].[Tbl_Blog] (BlogTitle, BlogAuthor, BlogContent, DeleteFlag) VALUES (@BlogTitle, @BlogAuthor, @BlogContent, 0)";
            using IDbConnection db = new SqlConnection(_connectionString);
            var result = db.Execute(query, new BlogDataModel
            {
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content
            });
            return Ok(result == 1 ? "Create Successful" : "Create Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blogs)
        {
            string query = "UPDATE [dbo].[Tbl_Blog] SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(_connectionString);
            var blog = new BlogDataModel
            {
                BlogId = id,
                BlogTitle = blogs.Title, // Replace with actual data from request
                BlogAuthor = blogs.Author, // Replace with actual data from request
                BlogContent = blogs.Content // Replace with actual data from request
            };
            var result = db.Execute(query,blog);

            return Ok(result == 1 ? "Update successful" : "Update Failed");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id ,BlogViewModel blogs)
        {
            string condition = "";
            if(!string.IsNullOrEmpty(blogs.Title))
            {
                condition += "BlogTitle = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blogs.Author))
            {
                condition += "BlogAuthor = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blogs.Content))
            {
                condition += "BlogContent = @BlogContent, ";
            }
            if (string.IsNullOrEmpty(condition))
            {
                return BadRequest("No fields to update");
            }
            condition = condition.Substring(0, condition.Length - 2); // Remove the last comma and space

            string query = @$"UPDATE [dbo].[Tbl_Blog] SET {condition} WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(_connectionString);
            var blog = new BlogDataModel
            {
                BlogId = id,
                BlogTitle = blogs.Title,
                BlogAuthor = blogs.Author,
                BlogContent = blogs.Content
            };
            int result = db.Execute(query, blog);


            // Implementation for patching a blog using Dapper
            return Ok(result == 1 ? "Update successful" : "Update failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = "UPDATE [dbo].[Tbl_Blog] SET DeleteFlag = 1 WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, new { BlogId = id });
            return Ok(result == 1 ? "Delete successful" : "Delete failed");
        }
    }
}
