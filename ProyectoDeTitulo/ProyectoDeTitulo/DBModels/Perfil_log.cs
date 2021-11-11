using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    public class Perfil_log
    {
        public int ID { get; set; }
        public int PerfilID{ get; set; }
        public virtual Perfil Perfil { get; set; }
        public DateTime Fecha_alta { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public DateTime Fecha_baja { get; set; }
        public DateTime Fecha_modificacion { get; set; }
    }
}