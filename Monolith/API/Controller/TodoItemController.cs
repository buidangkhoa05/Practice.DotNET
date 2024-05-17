using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    public class TodoItemController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Get TodoItem");
        }
    }
}
