using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    [Table("Estado")]
    public class Estado
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}