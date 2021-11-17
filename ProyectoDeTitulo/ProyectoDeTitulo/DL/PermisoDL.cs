using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProyectoDeTitulo.DBModels;

namespace ProyectoDeTitulo.DL
{
    public class PermisoDL
    {
        public static bool RegistrarPermisos(Permisos _Permisos)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
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

                        context.Permisos.Add(_Permisos);
                        context.SaveChanges();

                        //context.Permisos.AddRange(Permisos);
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
        public static bool ActualizarPermisos(Permisos _Permisos)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    context.Database.ExecuteSqlCommand("UPDATE Permisos SET " +
                        "Nombre = {0}," +
                        "EstadoID = {1}," +
                        " WHERE ID = {2}",
                        _Permisos.Nombre, _Permisos.EstadoID, _Permisos.ID);
                }

                return true;
            }
        }
        public static bool EliminarPermisos(string key)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM Permisos WHERE ID = {0}", key);
                }

                return true;
            }
        }

        public static Permisos GetPermisos(int key)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            Permisos Permisos = new Permisos();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection, true))
                {
                    Permisos = context.Permisos.Where(x => x.ID == key).FirstOrDefault();
                }

                return Permisos;
            }
        }
        public static IEnumerable<Permisos> GetPermisosList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            List<Permisos> listPermisoss = new List<Permisos>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection, true))
                {

                    //listPermisoss = context.Permisoss.Include(x => x.Permisoss).Include(x => x.Estado).ToList();
                    listPermisoss = context.Permisos.ToList();
                }

                return listPermisoss.AsEnumerable();
            }
        }
    }
}