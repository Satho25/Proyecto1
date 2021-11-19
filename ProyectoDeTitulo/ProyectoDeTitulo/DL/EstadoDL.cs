using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProyectoDeTitulo.DBModels;

namespace ProyectoDeTitulo.DL
{
    public class EstadoDL
    {
        public static bool RegistrarEstado(Estado _Estado)
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

                        context.Estados.Add(_Estado);
                        context.SaveChanges();

                        //context.Estado.AddRange(Estado);
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
        public static bool ActualizarEstado(Estado _Estado)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    context.Database.ExecuteSqlCommand("UPDATE Estado SET " +
                        "Nombre = {0}" +
                        " WHERE ID = {1}",
                        _Estado.Nombre, _Estado.ID);
                }

                return true;
            }
        }
        public static bool EliminarEstado(string key)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM Estado WHERE ID = {0}", key);
                }

                return true;
            }
        }

        public static Estado GetEstado(int key)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            Estado Estado = new Estado();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection, true))
                {
                    Estado = context.Estados.Where(x => x.ID == key).FirstOrDefault();
                }

                return Estado;
            }
        }
        public static IEnumerable<Estado> GetEstadoList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            List<Estado> listEstados = new List<Estado>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection, true))
                {

                    //listEstados = context.Estados.Include(x => x.Permisos).Include(x => x.Estado).ToList();
                    listEstados = context.Estados.ToList();
                }

                return listEstados.AsEnumerable();
            }
        }
    }
}