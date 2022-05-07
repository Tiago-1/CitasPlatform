using CitasPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CitasPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Data.CitasPlatformContext _context;

       
        public HomeController(ILogger<HomeController> logger, Data.CitasPlatformContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // Home/Login
        public string Login()
        {
            // Procesa los parametros que te dieron
            // Consulta la BD
            // Has la verificacion de cuenta
            // Si hay usuario -> manda la vista
            // Si no hay usuario -> regresa un mensaje
            Console.WriteLine("Intentemos esto");
            return "Esto esta padre";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            Console.WriteLine(model.user);
            var redirect = RedirectToAction();
            try
            {
                var user = from m in _context.Usuario
                             select m;

                user = user.Where(s => s.Correo == model.user);

                Console.WriteLine(user.ToList().Count());
                
                if(user.ToList().Count == 0)
                {
                    return View();
                }

                // if (existeusuario && contrase;a es correcta y esAlumno)

                redirect.ActionName = ""; // or can use nameof("") like  nameof(YourAction);
                redirect.ControllerName = "CitasAlumno"; // or can use nameof("") like  nameof(YourCtrl);
                return redirect;
                //else if (existeusuario && contrase;a es correcta y esPsicologa)
                // else 
                //return view

            }
            catch
            {
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
