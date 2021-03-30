using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_datos;
using System.Windows.Forms;


namespace capa_negocio
{
    public class mostrarDatosEnGrid
    {
        private string sentencia;
        private string mensaje;

        public string Sentencia
        {
            set { sentencia = value; }
        }

        public string Mensaje
        {
             get { return mensaje; }
        }


        public bool cargarGrid(DataGridView grid)
        {
            if (string.IsNullOrEmpty(sentencia))
            {
                mensaje = "You have fogotten define a Sql sentence";
                return false; 
            } 

            conexionBD conexion = new conexionBD();
            conexion.Sentencia = sentencia;

            if (!conexion.mostrarDatos())
            {
                mensaje = conexion.Mensaje;
                conexion = null;
                return false;
            }
            grid.DataSource = conexion.Ds.Tables[0];
            grid.ClearSelection();
            conexion = null;
            return true;
        }

    }
}
