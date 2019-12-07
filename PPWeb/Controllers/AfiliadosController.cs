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
    public class AfiliadosController : Controller
    {
        private PPWebEntities1 db = new PPWebEntities1();
       
        // GET: Afiliados
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            return View(db.Afiliados.ToList());
        }

        // GET: Afiliados/Details/5
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
            Afiliados afiliados = db.Afiliados.Find(id);
            if (afiliados == null)
            {
                return HttpNotFound();
            }
            return View(afiliados);
        }

        // GET: Afiliados/Create
        public ActionResult Create(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
           
            return View();
        }

        // POST: Afiliados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AfiliadosID,Nombre")] Afiliados afiliados, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Afiliados.Add(afiliados);
                db.SaveChanges();
                return RedirectToAction("Index","Afiliados", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }

            return View(afiliados);
        }

        // GET: Afiliados/Edit/5
        public ActionResult Edit(int? id,int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliados afiliados = db.Afiliados.Find(id);
            if (afiliados == null)
            {
                return HttpNotFound();
            }

            return View(afiliados);
        }

        // POST: Afiliados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AfiliadosID,Nombre")] Afiliados afiliados, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Entry(afiliados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Afiliados", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }
            return View(afiliados);
        }

        // GET: Afiliados/Delete/5
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
            Afiliados afiliados = db.Afiliados.Find(id);
            if (afiliados == null)
            {
                return HttpNotFound();
            }
            return View(afiliados);
        }

        // POST: Afiliados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            Afiliados afiliados = db.Afiliados.Find(id);
            db.Afiliados.Remove(afiliados);
            db.SaveChanges();
            return RedirectToAction("Index","Afiliados", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
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
