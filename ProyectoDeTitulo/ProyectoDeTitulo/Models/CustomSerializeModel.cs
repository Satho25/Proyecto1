using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.Models
{
    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public List<string> RoleName { get; set; }
        public string DefaultView { get; set; }
    }
}