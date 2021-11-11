using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using ProyectoDeTitulo.DBModels;
using System.Data.Common;

namespace ProyectoDeTitulo
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : DbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Contrasena> Contrasenas { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Perfil_log> Perfil_Logs { get; set; }
        public virtual DbSet<Perfil_permisos_log> Perfil_Permisos_Logs { get; set; }
        public virtual DbSet<Permisos_log> Permisos_Logs { get; set; }

        public DataContext() : base("MyContextDB")
        {

        }

        // Constructor to use on a DbConnection that is already opened
        public DataContext(DbConnection existingConnection, bool contextOwnsConnection)
        : base(existingConnection, contextOwnsConnection)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().MapToStoredProcedures();
            modelBuilder.Entity<Perfil>().MapToStoredProcedures();
            modelBuilder.Entity<Permisos>().MapToStoredProcedures();
            modelBuilder.Entity<Contrasena>().MapToStoredProcedures();
            modelBuilder.Entity<Estado>().MapToStoredProcedures();
            modelBuilder.Entity<Perfil_log>().MapToStoredProcedures();
            modelBuilder.Entity<Perfil_permisos_log>().MapToStoredProcedures();
            modelBuilder.Entity<Permisos_log>().MapToStoredProcedures();
        }

    }
}