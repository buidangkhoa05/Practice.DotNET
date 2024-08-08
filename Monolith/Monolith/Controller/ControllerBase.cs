
using Domain.Common;
using Domain.Common.PagedList;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public override BadRequestObjectResult BadRequest(object error)
        {
            return base.BadRequest(error);
        }

        public OkObjectResult Ok<T>(T value)
        {   
            return base.Ok(ApiResponse.Success(value));
        }
        public OkObjectResult Ok<T>(IPagedList<T> value)
        {
            return base.Ok(ApiResponse.Success(value));
        }

        public override ObjectResult StatusCode(int statusCode, object value)
        {
            return base.StatusCode(statusCode, value);
        }
    }
}
