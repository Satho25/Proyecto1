using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoDeTitulo.CustomAuthentication;
using ProyectoDeTitulo.Models;
using ProyectoDeTitulo.DBModels;

namespace ProyectoDeTitulo.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Base
        public ActionResult Index()
        {
            //testIni();

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogIn", "Account");


            return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void testIni()
        {

            //string connectionString = "server=localhost;port=3305;database=parking;uid=root";

            //DataContext dataContext = new DataContext()

            //127.0.0.1



        string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";

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

                        // DbSet.AddRange
                        Estado estado = new Estado() { Nombre = "EstadoTest" };
                        context.Estados.Add(estado);
                        context.SaveChanges();

                        Permisos permisos = new Permisos() { Nombre = "PermisoTest", EstadoID = 1 };
                        context.Permisos.Add(permisos);
                        context.SaveChanges();

                        Perfil perfil = new Perfil() { Nombre = "PerfilTest", EstadoID = 1, PermisoID = 1 };
                        context.Perfils.Add(perfil);
                        context.SaveChanges();

                        Usuario Usuario = new Usuario() { Nombre = "Pablo", Apellido = "Guerra", Contraseña="1234", Correo = "correo@correo.cl", RUT = "177353345", EstadoID = 1, PerfilID = 1 };
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
    }
}