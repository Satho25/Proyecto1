using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    [Table("Contrasena")]
    public class Contrasena
    {
        [Key]
        public int ID { get; set; }
        public string strContrasena { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public DateTime Fecha_expiracion { get; set; }
        public string RUT{ get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}