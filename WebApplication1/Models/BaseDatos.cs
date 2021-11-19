using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BaseDatos
    {
        MySqlConnection connection;

        public BaseDatos()
        {
            connection = new MySqlConnection("datasource = localhost; port = 3306; username = root; password=; database = facturas; SSLMode=none");
        }

        public string ejecutarSQL(string sql)
        {
            string resultado = "";
            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                int filas = cmd.ExecuteNonQuery();

                if (filas > -1)
                {
                    resultado = "Correcto";
                }
                else
                {
                    resultado = "Incorrecto";
                }
                connection.Close();

            }
            catch(Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }


        public DataTable getTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dt);
                connection.Close();
                adapter.Dispose();
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
    }
}
