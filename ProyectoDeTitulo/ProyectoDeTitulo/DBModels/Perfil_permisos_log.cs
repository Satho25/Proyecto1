using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    [Table("Perfil_permisos_log")]
    public class Perfil_permisos_log
    {
        [Key]
        public int ID { get; set; }
        public int PermisosID { get; set; }
        public int PerfilID { get; set; }
        public virtual Permisos Permisos { get; set; }
        public virtual Perfil Perfil { get; set; }
        public DateTime Fecha_alta { get; set; }
        public DateTime Fecha_baja { get; set; }
    }
}