using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    public class Contrasena
    {
        public int ID { get; set; }
        public string strContrasena { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public DateTime Fecha_expiracion { get; set; }
        public string RUT{ get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}