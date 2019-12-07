﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPWeb.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult CerrarSeccion()
        {
            ViewBag.Rol = 0;
            ViewBag.UsuarioActual = 0;
            return View("Index");
        }
        
        public ActionResult Login(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            return View("Index");
        }


        public ActionResult About (int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}