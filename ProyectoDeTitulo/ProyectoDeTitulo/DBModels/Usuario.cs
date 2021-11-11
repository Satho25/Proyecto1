using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    public class Usuario
    {
        public int ID { get; set; }
        public string RUT { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int EstadoID { get; set; }
        public int PerfilID { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}