using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitasPlatform.Controllers
{
    public class Admin : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

    }
}
