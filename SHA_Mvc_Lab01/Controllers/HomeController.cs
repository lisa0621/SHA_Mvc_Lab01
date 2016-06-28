using MotoGP.Service.Interface;
using System.Globalization;
using System.Web.Mvc;

namespace SHA_Mvc_Lab01.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRiderService riderService;

        public HomeController(
            IRiderService riderService)
        {
            this.riderService = riderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var data = riderService.GetMotogpGroupList();

            return View(data);
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            ViewBag.Message = MotoGP.Resources.Views.Shared.Shared.Title;

            return View();
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
              Session["Culture"] = new CultureInfo(lang);
              return RedirectToAction("Index", "Home");
        }
}
}