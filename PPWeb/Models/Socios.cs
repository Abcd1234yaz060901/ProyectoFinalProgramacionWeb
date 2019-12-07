//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Socios
    {
        public int SocioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long Cedula { get; set; }
        public byte[] Foto { get; set; }
        public string Direccion { get; set; }
        public string Telefonos { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> Edad { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public Nullable<int> AfiliadosID { get; set; }
        public string NombreAfiliado { get; set; }
        public int MembresiaID { get; set; }
        public string LugarDeTrabajo { get; set; }
        public string DireccionOficina { get; set; }
        public string TelefonoOficina { get; set; }
        public bool Estado { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public Nullable<System.DateTime> FechaSalida { get; set; }
    
        public virtual Afiliados Afiliados { get; set; }
        public virtual TiposMembresias TiposMembresias { get; set; }
    }
}
