using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{

    [Table("Permisos")]
    public class Permisos
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int EstadoID { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}