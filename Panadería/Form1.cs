using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Panadería
{
    public partial class Panadería : Form
    {
        int pos =0;
        public Panadería()
        {
            InitializeComponent();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string insertar = "INSERT INTO dbo.Proveedor (Nombre,RFC,Direccion,Email)" + "VALUES (@Nombre,@RFC,@Direccion,@Email)";
            SqlCommand cm = new SqlCommand(insertar, Conexion.Conectar());
            cm.Parameters.AddWithValue("@Nombre", provedorcaptura.Rows[0].Cells[0].Value);
            cm.Parameters.AddWithValue("@RFC", provedorcaptura.Rows[0].Cells[1].Value);
            cm.Parameters.AddWithValue("@Direccion", provedorcaptura.Rows[0].Cells[2].Value);
            cm.Parameters.AddWithValue("@Email", provedorcaptura.Rows[0].Cells[3].Value);
            try
            {
                cm.ExecuteNonQuery();
                provedorcaptura.Rows.Clear();
            }
            catch
            {
                MessageBox.Show("No se puede registrar debido a que existe algun provedor con esta información", "Error al dar de alta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            provedorregistros.DataSource = llenar_grid('p');
            
        }
        public DataTable llenar_grid(char t)
        {
            DataTable dt = new DataTable();
            switch (t)
            {
                case 'p':
                    string consulta = "SELECT * FROM dbo.Proveedor";
                    SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    break;
                case 'm':
                    consulta = "SELECT * FROM dbo.Material";
                    cmd = new SqlCommand(consulta, Conexion.Conectar());
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    break;
            }
            return dt;
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            provedorregistros.DataSource = llenar_grid('p');
            materialregistros.DataSource = llenar_grid('m');
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            provedorregistros.DataSource = llenar_grid('p');
            materialregistros.DataSource = llenar_grid('m');
        }

        private void provedorregistros_MouseClick(object sender, MouseEventArgs e)
        {
            pos = provedorregistros.CurrentRow.Index;
            provedorcaptura.Rows[0].Cells[0].Value = provedorregistros.Rows[pos].Cells[1].Value.ToString();
            provedorcaptura.Rows[0].Cells[1].Value = provedorregistros.Rows[pos].Cells[2].Value.ToString();
            provedorcaptura.Rows[0].Cells[2].Value = provedorregistros.Rows[pos].Cells[3].Value.ToString();
            provedorcaptura.Rows[0].Cells[3].Value = provedorregistros.Rows[pos].Cells[4].Value.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string insertar = "UPDATE dbo.Proveedor SET Nombre=@Nombre,RFC=@RFC,Direccion=@Direccion,Email=@Email WHERE idProveedor=@id";
            SqlCommand cm = new SqlCommand(insertar, Conexion.Conectar());
            cm.Parameters.AddWithValue("@Nombre", provedorcaptura.Rows[0].Cells[0].Value);
            cm.Parameters.AddWithValue("@RFC", provedorcaptura.Rows[0].Cells[1].Value);
            cm.Parameters.AddWithValue("@Direccion", provedorcaptura.Rows[0].Cells[2].Value);
            cm.Parameters.AddWithValue("@Email", provedorcaptura.Rows[0].Cells[3].Value);
            cm.Parameters.AddWithValue("@id", provedorregistros.Rows[pos].Cells[0].Value);
            try
            {
                cm.ExecuteNonQuery();
                provedorcaptura.Rows.Clear();
            }
            catch
            {
                MessageBox.Show("No se puede modificar debido a que existe algun provedor con esta información", "Error al dar de alta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            provedorregistros.DataSource = llenar_grid('p');
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string insertar = "DELETE FROM dbo.Proveedor WHERE idProveedor=@id";
            SqlCommand cm = new SqlCommand(insertar, Conexion.Conectar());
            cm.Parameters.AddWithValue("@id", provedorregistros.Rows[pos].Cells[0].Value);
            cm.ExecuteNonQuery();
            provedorregistros.DataSource = llenar_grid('p');
            provedorcaptura.Rows.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string insertar = "INSERT INTO dbo.Material (Nombre,Precio,Existencia)" + "VALUES (@Nombre,@Precio,@Existencia)";
            SqlCommand cm = new SqlCommand(insertar, Conexion.Conectar());
            cm.Parameters.AddWithValue("@Nombre", materialcaptura.Rows[0].Cells[0].Value);
            cm.Parameters.AddWithValue("@Precio", materialcaptura.Rows[0].Cells[1].Value);
            cm.Parameters.AddWithValue("@Existencia", 0);
            try
            {
                cm.ExecuteNonQuery();
                materialcaptura.Rows.Clear();
            }
            catch
            {
                MessageBox.Show("No se puede registrar debido a que existe algun material con esta información", "Error al dar de alta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            materialregistros.DataSource = llenar_grid('m');
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string insertar = "DELETE FROM dbo.Material WHERE idMaterial=@id";
            SqlCommand cm = new SqlCommand(insertar, Conexion.Conectar());
            cm.Parameters.AddWithValue("@id", materialregistros.Rows[pos].Cells[0].Value);
            cm.ExecuteNonQuery();
            materialregistros.DataSource = llenar_grid('m');
            provedorcaptura.Rows.Clear();
        }

        private void materialregistros_MouseClick(object sender, MouseEventArgs e)
        {
            pos = materialregistros.CurrentRow.Index;
            materialcaptura.Rows[0].Cells[0].Value = materialregistros.Rows[pos].Cells[1].Value.ToString();
            materialcaptura.Rows[0].Cells[1].Value = materialregistros.Rows[pos].Cells[2].Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string insertar = "UPDATE dbo.Material SET Nombre=@Nombre,Precio=@Precio,Existencia=@Existencia WHERE idMaterial=@id";
            SqlCommand cm = new SqlCommand(insertar, Conexion.Conectar());
            cm.Parameters.AddWithValue("@Nombre", materialcaptura.Rows[0].Cells[0].Value);
            cm.Parameters.AddWithValue("@Precio", materialcaptura.Rows[0].Cells[1].Value);
            cm.Parameters.AddWithValue("@Existencia", materialregistros.Rows[pos].Cells[3].Value);
            cm.Parameters.AddWithValue("@id", materialregistros.Rows[pos].Cells[0].Value);
            try
            {
                cm.ExecuteNonQuery();
                provedorcaptura.Rows.Clear();
            }
            catch
            {
                MessageBox.Show("No se puede modificar debido a que existe algun material con esta información", "Error al dar de alta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            materialregistros.DataSource = llenar_grid('m');
        }
    }
}
