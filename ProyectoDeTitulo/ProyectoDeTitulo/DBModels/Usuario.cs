using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    public class Usuario
    {
        /*When modified, execute menu > Tool > nuggetpackage manager >  PM console > run >  Add-Migration (and then) Update-Database*/
        public int ID { get; set; }
        [Required]
        public string RUT { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public int EstadoID { get; set; }
        public int PerfilID { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}