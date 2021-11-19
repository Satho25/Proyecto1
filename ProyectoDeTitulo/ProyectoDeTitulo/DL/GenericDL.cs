using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProyectoDeTitulo.DBModels;

namespace ProyectoDeTitulo.DL
{
    public class GenericDL
    {

        public static bool CheckDB()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (DataContext contextDB = new DataContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }
                
                return true;
            }
        }

        private static void IniData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (DataContext context = new DataContext(connection, false))
                    {

                        // Interception/SQL logging
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        // Passing an existing transaction to the context
                        context.Database.UseTransaction(transaction);

                        // DbSet.AddRange
                        Estado estado = new Estado() { Nombre = "EstadoTest" };
                        context.Estados.Add(estado);
                        context.SaveChanges();

                        Permisos permisos = new Permisos() { Nombre = "PermisoTest", EstadoID = 1 };
                        context.Permisos.Add(permisos);
                        context.SaveChanges();

                        Perfil perfil = new Perfil() { Nombre = "PerfilTest", EstadoID = 1 };
                        context.Perfils.Add(perfil);
                        context.SaveChanges();

                        Usuario Usuario = new Usuario() { Nombre = "Pablo", Apellido = "Guerra", Contraseña = "1234", Correo = "correo@correo.cl", RUT = "177353345", EstadoID = 1, PerfilID = 1 };
                        context.Usuarios.Add(Usuario);
                        context.SaveChanges();

                        //context.Usuario.AddRange(Usuario);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }

        /*
        public static Estado GetEstado(int id, DataContext context)
        {
            return context.Estados.Where(x => x.ID == id).FirstOrDefault();
        }
        public static Perfil GetPerfil(int id, DataContext context)
        {
            return context.Perfils.Where(x => x.ID == id).FirstOrDefault();
        }*/
    }
}