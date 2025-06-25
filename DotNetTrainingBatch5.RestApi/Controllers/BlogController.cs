using DotNetTrainingBatch5.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);


        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound($"Blog with ID {id} not found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound();
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }

        [HttpPatch("{id}")]
         public IActionResult PatchBlog(int id,TblBlog blog)
        {
            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(item.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(item.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(item.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBlog(int id)
        {

            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound();
            }

            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Modified;
            //_db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok();
        }
    }
}
