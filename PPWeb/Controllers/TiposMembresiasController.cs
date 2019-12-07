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
    public class TiposMembresiasController : Controller
    {
        private PPWebEntities1 db = new PPWebEntities1();
       
        // GET: TiposMembresias
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            return View(db.TiposMembresias.ToList());
        }

        // GET: TiposMembresias/Details/5
        public ActionResult Details(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposMembresias tiposMembresias = db.TiposMembresias.Find(id);
            if (tiposMembresias == null)
            {
                return HttpNotFound();
            }
            return View(tiposMembresias);
        }

        // GET: TiposMembresias/Create
        public ActionResult Create(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            
            return View();
        }

        // POST: TiposMembresias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MembresiaID,Nombre,Costo")] TiposMembresias tiposMembresias, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.TiposMembresias.Add(tiposMembresias);
                db.SaveChanges();
                return RedirectToAction("Index", "TiposMembresias",new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }

            return View(tiposMembresias);
        }

        // GET: TiposMembresias/Edit/5
        public ActionResult Edit(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
             
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposMembresias tiposMembresias = db.TiposMembresias.Find(id);
            if (tiposMembresias == null)
            {
                return HttpNotFound();
            }
            return View(tiposMembresias);
        }

        // POST: TiposMembresias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MembresiaID,Nombre,Costo")] TiposMembresias tiposMembresias, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Entry(tiposMembresias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","TiposMembresias", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo});
            }
            return View(tiposMembresias);
        }

        // GET: TiposMembresias/Delete/5
        public ActionResult Delete(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposMembresias tiposMembresias = db.TiposMembresias.Find(id);
            if (tiposMembresias == null)
            {
                return HttpNotFound();
            }
            return View(tiposMembresias);
        }

        // POST: TiposMembresias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            TiposMembresias tiposMembresias = db.TiposMembresias.Find(id);
            db.TiposMembresias.Remove(tiposMembresias);
            db.SaveChanges();
            return RedirectToAction("Index","TiposMembresias", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
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
