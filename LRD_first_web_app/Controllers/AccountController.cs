using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using LRD_first_web_app.Models;

namespace LRD_first_web_app.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public UsuarioDBEntities _Entities=new UsuarioDBEntities();
        public int IdUsuarioDeLaBaseDeDatos=0;
        
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
          
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var comprobarUsuario = _Entities.Usuario.ToList();
                foreach (var itera in comprobarUsuario)
                {
                    if (model.UserName.Equals(itera.Nombre))
                    {

                        IdUsuarioDeLaBaseDeDatos = itera.Id;
                        //si existe entonces hay que cambiar a la vista de las fotos de este usuario
                        return RedirectToAction("MostrarGaleria", "Account");
                    }

                }
                
          

                return RedirectToAction("Index", "Home");



            }


            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comprobarUsuario = _Entities.Usuario.ToList();
                foreach (var itera in comprobarUsuario)
                {
                    if (model.UserName.Equals(itera.Nombre))
                    {
                       // Console.WriteLine("el usuario ya existe");
                        return RedirectToAction("Register", "Account");
                    }

                }
                var us = new Usuario();
                us.Nombre = model.UserName;
                us.Password = model.ConfirmPassword;
                _Entities.Usuario.Add(us);
                _Entities.SaveChanges();
                //el usuario se creo satisfactoriamente hay que cambiar el 
                
              
                 return RedirectToAction("Login", "Account");
        


            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult MostrarGaleria()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult MostrarGaleria(RegisterViewModel model)
        {
            return RedirectToAction("MostrarGaleria", "Account");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //borrar los datos de la cuenta y regresar al inicio
            return RedirectToAction("Index", "Home");
        }

      
    }
}