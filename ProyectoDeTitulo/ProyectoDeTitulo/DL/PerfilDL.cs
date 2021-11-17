using MySql.Data.MySqlClient;
using ProyectoDeTitulo.DBModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoDeTitulo.DL
{
    public class PerfilDL
    {
        public static bool RegistrarPerfil(Perfil _Perfil)
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

                        context.Perfils.Add(_Perfil);
                        context.SaveChanges();

                        //context.Perfil.AddRange(Perfil);
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
        public static bool ActualizarPerfil(Perfil _Perfil)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    context.Database.ExecuteSqlCommand("UPDATE Perfils SET " +
                        "Nombre = {0}," +
                        "EstadoID = {1}," +
                        "PermisoID = {2}" +
                        " WHERE ID = {3}",
                        _Perfil.Nombre, _Perfil.EstadoID, _Perfil.PermisoID, _Perfil.ID);
                }

                return true;
            }
        }
        public static bool EliminarPerfil(string key)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM Perfils WHERE ID = {0}", key);
                }

                return true;
            }
        }

        public static Perfil GetPerfil(int key)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            Perfil Perfil = new Perfil();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection, true))
                {
                    Perfil = context.Perfils.Where(x => x.ID == key).Include(x => x.Permisos).Include(x => x.Estado).FirstOrDefault();
                }

                return Perfil;
            }
        }
        public static IEnumerable<Perfil> GetPerfilList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            List<Perfil> listPerfils = new List<Perfil>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection, true)) { 

                    //listPerfils = context.Perfils.Include(x => x.Permisos).Include(x => x.Estado).ToList();
                    listPerfils = context.Perfils.ToList();
                }

                return listPerfils.AsEnumerable();
            }
        }
    }
}