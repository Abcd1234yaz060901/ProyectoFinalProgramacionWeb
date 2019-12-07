using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PPWeb.Models;

namespace PPWeb.Controllers
{
    public class SociosController : Controller
    {
        private PPWebEntities1 db = new PPWebEntities1();

        // GET: Socios
        // GET: Socios
       

        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            var socios = db.Socios.Include(s => s.Afiliados).Include(s => s.TiposMembresias);
            return View(socios.ToList());
        }

        // GET: Socios/Details/5
        public ActionResult Details(int? id,int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;



            Socios socios = db.Socios.Find(id);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // GET: Socios/Create
        public ActionResult Create(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
           
            ViewBag.AfiliadosID = new SelectList(db.Afiliados, "AfiliadosID", "Nombre");
            ViewBag.MembresiaID = new SelectList(db.TiposMembresias, "MembresiaID", "Nombre");
            return View();
        }

        // POST: Socios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SocioId,Nombre,Apellido,Cedula,Foto,Direccion,Telefonos,Sexo,Edad,FechaNacimiento,AfiliadosID,NombreAfiliado,MembresiaID,LugarDeTrabajo,DireccionOficina,TelefonoOficina,Estado,FechaIngreso,FechaSalida")] Socios socios, int rol, int usuario, string NombreUsuario, string Correo)
        {

            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            HttpPostedFileBase FileBase = Request.Files[0];//leeme el archivo en la posicion 0
                                                           //HttpFileCollectionBase collectionBase = Request.Files;
                                                           //el request le permite al servidor o al asp.net le permite leer los valores del http
                                                           //filebase nos proporciona acceso al archivo
            if (FileBase.ContentLength == 0)
            {
                ModelState.AddModelError("Foto", "El campo necesario seleccionar una imagen.");

            }
            else
            {
                if (FileBase.FileName.EndsWith(".jpg"))
                {
                    //ahora esta clase nos permite administrar la imagen

                    System.Web.Helpers.WebImage image = new WebImage(FileBase.InputStream);

                    socios.Foto = image.GetBytes(); //aqui se obtienen los bytes de nuestra imagen


                }
                else
                {
                    ModelState.AddModelError("Foto", "El sistema solo acepta un formato.JPG");
                }

            }

            if (ModelState.IsValid)
            {
                db.Socios.Add(socios);
                db.SaveChanges();
                return RedirectToAction("Index","Socios", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }

            ViewBag.AfiliadosID = new SelectList(db.Afiliados, "AfiliadosID", "Nombre", socios.AfiliadosID);
            ViewBag.MembresiaID = new SelectList(db.TiposMembresias, "MembresiaID", "Nombre", socios.MembresiaID);
            return View(socios);



        }


        // GET: Socios/Edit/5
        // GET: Socios/Edit/5
        public ActionResult Edit(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            ViewBag.AfiliadosID = new SelectList(db.Afiliados, "AfiliadosID", "Nombre", socios.AfiliadosID);
            ViewBag.MembresiaID = new SelectList(db.TiposMembresias, "MembresiaID", "Nombre", socios.MembresiaID);
            return View(socios);
        }

        // POST: Socios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SocioId,Nombre,Apellido,Cedula,Foto,Direccion,Telefonos,Sexo,Edad,FechaNacimiento,AfiliadosID,NombreAfiliado,MembresiaID,LugarDeTrabajo,DireccionOficina,TelefonoOficina,Estado,FechaIngreso,FechaSalida")] Socios socios,int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            byte[] imagenActual = null;
            HttpPostedFileBase FileBase= Request.Files[0];

            
            //Socios _socios = new Socios();
            //HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase == null)
            {
                imagenActual = db.Socios.SingleOrDefault(t=>t.SocioId==socios.SocioId).Foto;

            }
            else
            {

                WebImage image = new WebImage(FileBase.InputStream);
                socios.Foto = image.GetBytes();
            }

            //if (FileBase.ContentLength == 0)
            //{


            //    _socios = db.Socios.Find(socios.SocioId);
            //    socios.Foto = _socios.Foto;
            //}
            //else
            //{

            //    if (FileBase.FileName.EndsWith(".jpg"))
            //    {
            //        //ahora esta clase nos permite administrar la imagen

            //        WebImage image = new WebImage(FileBase.InputStream);

            //        socios.Foto = image.GetBytes(); //aqui se obtienen los bytes de nuestra imagen


            //    }
            //    else
            //    {
            //        ModelState.AddModelError("Foto", "El sistema solo acepta un formato.JPG");
            //    }



            //}
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            if (ModelState.IsValid)
            {
                db.Entry(socios).State = EntityState.Detached;
                db.Entry(socios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Socios", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }
            ViewBag.AfiliadosID = new SelectList(db.Afiliados, "AfiliadosID", "Nombre", socios.AfiliadosID);
            ViewBag.MembresiaID = new SelectList(db.TiposMembresias, "MembresiaID", "Nombre", socios.MembresiaID);
            return View(socios);
        }
        // GET: Socios/Delete/5
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
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return HttpNotFound();
            }
            return View(socios);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            Socios socios = db.Socios.Find(id);
            db.Socios.Remove(socios);
            db.SaveChanges();
            return RedirectToAction("Index","Socios", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //agregamos un nuevo metodo para get image

        public ActionResult getImage(int id)
        {
            Socios socios = db.Socios.Find(id);
            byte[] byteImage = socios.Foto;

            System.IO.MemoryStream memoryStream = new MemoryStream(byteImage);

            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }
    }
}
