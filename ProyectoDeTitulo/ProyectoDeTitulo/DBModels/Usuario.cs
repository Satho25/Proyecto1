using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    [Table("Usuario")]
    public class Usuario
    {
        /*When modified, execute menu > Tool > nuggetpackage manager >  PM console > run >  Add-Migration (and then) Update-Database*/
        [Key]
        public string RUT { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Ingrese Apellido")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Apellido debe tener minimo 2 caracteres")]
        public string Apellido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Correo electronico")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Correo electronico invalido")]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public int EstadoID { get; set; }

        [Required]
        public int PerfilID { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}