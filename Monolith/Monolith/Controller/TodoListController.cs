using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    public class TodoListController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Get TodoList");
        }
    }
}
