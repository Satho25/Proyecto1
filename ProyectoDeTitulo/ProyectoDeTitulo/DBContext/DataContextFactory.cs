using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace ProyectoDeTitulo
{
    public class DataContextFactory : IDbContextFactory<DataContext>
    {
        public DataContext Create()
        {
            return new DataContext();
        }
    }
}