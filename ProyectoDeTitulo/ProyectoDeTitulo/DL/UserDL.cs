using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProyectoDeTitulo.DBModels;

namespace ProyectoDeTitulo.DL
{
    public class UserDL
    {
        public static bool RegistrarUsuario(Usuario _usuario)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString(); 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (DataContext contextDB = new DataContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }

                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (DataContext context = new DataContext(connection,false))
                    {

                        // Interception/SQL logging
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        // Passing an existing transaction to the context
                        context.Database.UseTransaction(transaction);
                        
                        context.Usuarios.Add(_usuario);
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

                return true;
            }            
        }
        public static Usuario GetUsuario(string key)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            Usuario usuario = new Usuario();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (DataContext contextDB = new DataContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }

                connection.Open();
                //MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (DataContext context = new DataContext(connection,false))
                    {

                        // Interception/SQL logging
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        // Passing an existing transaction to the context
                        //context.Database.UseTransaction(transaction);

                        usuario = context.Usuarios.First(x => x.RUT == key);
                        //context.SaveChanges();

                        //context.Usuario.AddRange(Usuario);
                    }

                    //transaction.Commit();
                }
                catch
                {
                    //transaction.Rollback();
                    throw;
                }

                return usuario;
            }            
        }
        public static IEnumerable<Usuario> GetUsuarioList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            IEnumerable<Usuario> listUsuarios = new List<Usuario>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (DataContext contextDB = new DataContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }

                connection.Open();
                //MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (DataContext context = new DataContext(connection,false))
                    {

                        // Interception/SQL logging
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        // Passing an existing transaction to the context
                        //context.Database.UseTransaction(transaction);
                        
                        listUsuarios = context.Usuarios.AsEnumerable();
                    }

                    //transaction.Commit();
                }
                catch
                {
                    //transaction.Rollback();
                    throw;
                }

                return listUsuarios;
            }            
        }
        public static IEnumerable<Permisos> GetPermisos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            IEnumerable<Permisos> listPermisos = new List<Permisos>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (DataContext contextDB = new DataContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }

                connection.Open();
                //MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (DataContext context = new DataContext(connection,false))
                    {

                        // Interception/SQL logging
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        // Passing an existing transaction to the context
                        //context.Database.UseTransaction(transaction);

                        listPermisos = context.Permisos.AsEnumerable();
                    }

                    //transaction.Commit();
                }
                catch
                {
                    //transaction.Rollback();
                    throw;
                }

                return listPermisos;
            }            
        }
    }
}