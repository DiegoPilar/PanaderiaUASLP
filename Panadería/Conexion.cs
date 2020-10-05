using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Panadería
{
    class Conexion
    {
        private const string ConnectionString = "SERVER=DIEGO\\SQLEXPRESS01;DATABASE=Panaderia;integrated security=TRUE";
        public static SqlConnection Conectar()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("Error al conectar la base de datos", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return con;
        }
    }
}
