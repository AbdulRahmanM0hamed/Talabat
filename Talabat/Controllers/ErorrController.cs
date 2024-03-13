using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Error;

namespace Talabat.Controllers
{
    [Route("erorrs/{code}")]
    [ApiController]
    [ApiExplorerSettings (IgnoreApi =true)]
    public class ErorrController : ControllerBase
    {
        public ActionResult Eroor(int code)
        {
            return NotFound(new ApiErorrHandling(code));
        }
    }
}
