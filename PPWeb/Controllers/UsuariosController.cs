using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PPWeb.Models;

namespace PPWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private PPWebEntities1 db = new PPWebEntities1();

        // GET: Usuarios
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            var usuarios = db.Usuarios.Include(u => u.ROLES);
            return View(usuarios.ToList());
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ValidarLogin(Usuarios usuario)
        {
            try
            {

     int IdUsuario =
   (from USR in db.Usuarios.ToList()
    where USR.NombreUsuario == usuario.NombreUsuario && USR.Clave == usuario.Clave
    select USR.Id).First();

      int IdRol =
    (from USR in db.Usuarios.ToList()
     where USR.Id == IdUsuario
     select USR.IdRol).First();

            string Email =
   (from Correo in db.Usuarios.ToList()
    where Correo.Id == IdUsuario
    select Correo.Email).First();

            if (IdUsuario > 0)
            {

                return RedirectToAction("Login", "Home", new { rol = IdRol, usuario = IdUsuario , NombreUsuario = usuario.NombreUsuario, Correo = Email});
            }
            else
            {
                // el usuario es invalido
                return RedirectToAction("Login", "Usuarios");
            }


            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return RedirectToAction("Login", "Usuarios");
            }

        }


        // GET: Usuarios/Details/5
        public ActionResult Details(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create(int rol=0, int usuario=0, string NombreUsuario = "", string Correo="")
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            
            ViewBag.IdRol = new SelectList(db.ROLES, "Id", "DescripcionRol");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,NombreUsuario,Clave,IdRol")] Usuarios usuarios, int rol= 0, int usuario = 0, string NombreUsuario = "", string Correo = "")
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                ViewBag.Rol = 0;
                ViewBag.UsuarioActual = 0;
                ViewBag.nombre = "";
                ViewBag.correo = "";

                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index","Usuarios", new {  rol, usuario,  NombreUsuario,  Correo });
            }

            ViewBag.IdRol = new SelectList(db.ROLES, "Id", "DescripcionRol", usuarios.IdRol);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit( int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.ROLES, "Id", "DescripcionRol", usuarios.IdRol);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,NombreUsuario,Clave,IdRol")] Usuarios usuarios, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Usuarios", new { rol, usuario,  NombreUsuario, Correo });
            }
            ViewBag.IdRol = new SelectList(db.ROLES, "Id", "DescripcionRol", usuarios.IdRol);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario = 0, string NombreUsuario = "", string Correo = "")
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index","Usuarios", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}