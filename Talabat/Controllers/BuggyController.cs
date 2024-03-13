using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Error;
using Talabat.Reopsitory.Date;

namespace Talabat.Controllers
{

    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

        [HttpGet("notFound")]
        public ActionResult GetNotFoundREqust()
        {
            var pro = _storeContext.products.Find(100);
            if (pro == null)
                return NotFound(new ApiErorrHandling(404));
            return Ok(pro);

        }

        [HttpGet("ServerErorr")]
        public ActionResult GeSreverErorr()
        {
            var pro = _storeContext.products.Find(100);
            var rPro = pro.ToString(); 
            return Ok(rPro);

        }

        [HttpGet("badRequst")]
        public ActionResult GebadRequst()
        {
            return BadRequest(new ApiErorrHandling(400));

        }



        [HttpGet("badRequst/{id}")]
        public ActionResult GebadRequst(int id)
        {
            return Ok();

        }





    }
}
