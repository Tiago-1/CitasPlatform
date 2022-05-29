using CitasPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CitasPlatform.Controllers.Error;
using Microsoft.AspNetCore.Routing;

namespace CitasPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.CitasPlatformContext _context;

       
        public HomeController(Data.CitasPlatformContext context)
        {
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
        // Login principal function
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {

            var redirect = RedirectToAction();

            try
            {
                var user = from m in _context.Usuario select m;
                user = user.Where(s => s.Correo == model.user && s.Pass == model.password);

                Console.WriteLine(user.First().UsuarioId);
                
                if(user.ToList().Count == 0)
                {
                    throw new AppException("Email or password is incorrect");
                }

                // if (existeusuario && contrase;a es correcta y esAlumno)
                if(user.ToList().First().Rol == 1)
                {
                    redirect.ActionName = ""; // or can use nameof("") like  nameof(YourAction);
                    redirect.ControllerName = "CitasAlumno"; // or can use nameof("") like  nameof(YourCtrl);
                                                             // return redirect;
                    return RedirectToAction("", new RouteValueDictionary(
                    new { controller = "CitasAlumno", action = "", Id = user.First().UsuarioId }));
                }
                else
                {
                    redirect.ActionName = ""; // or can use nameof("") like  nameof(YourAction);
                    redirect.ControllerName = "Admin"; // or can use nameof("") like  nameof(YourCtrl);
                    return redirect;
                }

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
