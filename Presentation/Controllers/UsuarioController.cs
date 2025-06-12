using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Application.Service;
using SADVO.Application.ViewModels.Usuario;
using SADVO.Domain.Enumns;

namespace SADVOWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuariosService _userServices;
        private readonly IUsuariosSeccion _userSeccion;

        public UsuarioController(IUsuariosService usuariosService, IUsuariosSeccion usuariosSeccion) 
        {
            _userServices = usuariosService;
            _userSeccion = usuariosSeccion;
        }
        // GET: HomeController1
        public async Task<IActionResult> Index()
        {
            if (!_userSeccion.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_userSeccion.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var usuarios = await _userServices.GetAllAsync();

            var usuariosViewModel = usuarios.Select(u => new UserViewModel()
            {
                Id = u.Id,
                Nombre = u.Nombre,
                EsActivo = u.EsActivo,
                Apellido = u.Apellido,
                Email = u.Email,
                Telefono = u.Telefono,
                Password = u.Password,
                ConfirmationPassword = u.ConfirmationPassword,  
                Foto = u.Foto,
                Role = u.Role
            }).ToList();

            return View(usuariosViewModel); 
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
