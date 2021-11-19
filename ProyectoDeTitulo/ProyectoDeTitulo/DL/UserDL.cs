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
    public class UserDL
    {
        public static bool RegistrarUsuario(Usuario _usuario)
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
        public static bool ActualizarUsuario(Usuario _usuario)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString(); 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    /* -> por alguna razon actualiza todos los registros !! error 
                    var _upd = context.Usuarios.SingleOrDefault(x => x.ID == _usuario.ID);

                    if(_upd != null)
                    {
                        _upd.Nombre = _usuario.Nombre;
                        _upd.Apellido = _usuario.Apellido;
                        _upd.Correo = _usuario.Correo;
                        _upd.Contraseña = _usuario.Contraseña;
                        _upd.EstadoID = _usuario.EstadoID;
                        _upd.PerfilID = _usuario.PerfilID;
                        context.SaveChanges();
                    } */

                    context.Database.ExecuteSqlCommand("UPDATE Usuario SET " +
                        "Nombre = {0}," +
                        "Apellido = {1}," +
                        "Correo = {2}," +
                        "Contraseña = {3}," +
                        "EstadoID = {4}," +
                        "PerfilID = {5}" +
                        " WHERE RUT = {6}", 
                        _usuario.Nombre,_usuario.Apellido,_usuario.Correo, _usuario.Contraseña, _usuario.EstadoID, _usuario.PerfilID,_usuario.RUT);
                       
                }

                return true;
            }            
        }
        public static bool EliminarUsuario(string key)
        {
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString(); 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (DataContext context = new DataContext(connection, false))
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM Usuario WHERE RUT = {0}", key);
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
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection,true))
                {                    
                    usuario = context.Usuarios.Where(x => x.RUT == key).Include(x => x.Perfil).Include(x => x.Estado).FirstOrDefault();
                }

                return usuario;
            }            
        }
        public static IEnumerable<Usuario> GetUsuarioList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyContextDB"].ToString();
            List<Usuario> listUsuarios = new List<Usuario>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // DbConnection that is already opened
                using (DataContext context = new DataContext(connection, true))
                {
                    //listUsuarios = context.Usuarios.Include("Perfil.Estado").ToList();
                    listUsuarios = context.Usuarios.Include(x => x.Perfil).Include(x => x.Estado).ToList();
                }

                return listUsuarios.AsEnumerable();
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