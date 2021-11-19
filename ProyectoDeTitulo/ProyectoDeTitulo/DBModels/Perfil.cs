using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int EstadoID { get; set; }
        public List<int> PermisosID { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Permisos> Permisos { get; set; }

        public Perfil()
        {
            this.Permisos = new List<Permisos>();
        }
    }
}