using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DBModels
{
    public class Permisos
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int EstadoID { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}