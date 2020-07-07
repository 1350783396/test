using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult test() {
            decimal aa = Math.Round((decimal)5 / (decimal)3, 2);

           var cc = Math.Round((double)0/ (double)0, 2);

            List<DateTime> dateTimes = new List<DateTime>();

            dateTimes.Add(DateTime.Now);
            dateTimes.Add(DateTime.Now.AddDays(-1));
            dateTimes.Add(DateTime.Now.AddDays(-2));
           var ccc = dateTimes.Min(u => u.Date);
            return Content(cc.ToString()) ;
        }


        public ActionResult GetJson() {
            Dictionary<string, int> keyValuePairs1 = new Dictionary<string, int>() { { "asd", 1 }, { "ads", 2 } };
           // var json1 = JsonConvert.SerializeObject(keyValuePairs1);
            return Json(new {data= keyValuePairs1 , code=0});
        }

    }
}