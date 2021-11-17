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
            //string connectionString = "server=127.0.0.1;port=3306;database=proyectodetitulo4;uid=root;Pwd=1234";
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