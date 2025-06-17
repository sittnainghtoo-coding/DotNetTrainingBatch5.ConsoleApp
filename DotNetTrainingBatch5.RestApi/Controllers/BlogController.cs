using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok(new {message = "getBlogs"});
        }

        [HttpPost]
        public IActionResult CreateBlog()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBlog()
        {
            return Ok();
        }

        [HttpPatch]
         public IActionResult PatchBlog()
        {
            return Ok();
        }

        [HttpDelete]

        public IActionResult DeleteBlog()
        {
            return Ok();
        }
    }
}
