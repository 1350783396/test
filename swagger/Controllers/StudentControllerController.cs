using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace swagger.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentControllerController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Tom";
        }

        [HttpPost]
        public ActionResult Post(string str)
        {
            return Ok(new { str });
        }
    }
}