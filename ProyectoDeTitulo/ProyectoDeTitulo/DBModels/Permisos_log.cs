using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    public class Permisos_log
    {
        public int ID { get; set; }
        public int PermisosID{ get; set; }
        public virtual Permisos Permisos { get; set; }
        public DateTime Fecha_alta{ get; set; }
        public DateTime Fecha_creacion{ get; set; }
        public DateTime Fecha_baja { get; set; }
    }
}