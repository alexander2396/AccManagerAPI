using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccManager.Common.RequestResult;

namespace AccManager.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult ReturnResult(RequestResult result)
        {
            if (result.IsSuccess)
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            if (result.StatusCode == StatusCodes.Status406NotAcceptable)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, result.Message);
            }

            if (result.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, result.Message);
            }

            return result.StatusCode == StatusCodes.Status400BadRequest
                    ? BadRequest(result.Message) : StatusCode((int)HttpStatusCode.InternalServerError) as IActionResult;
        }

        protected IActionResult ReturnResult<T>(RequestResult<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Obj);
            }

            if (result.StatusCode == StatusCodes.Status406NotAcceptable)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, result.Message);
            }

            if (result.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, result.Message);
            }

            return result.StatusCode == StatusCodes.Status400BadRequest
                       ? BadRequest(result.Message) : StatusCode((int)HttpStatusCode.InternalServerError, result.Message) as IActionResult;
        }
    }
}
