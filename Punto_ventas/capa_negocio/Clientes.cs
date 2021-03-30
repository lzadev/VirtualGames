using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_datos;

namespace capa_negocio
{
    public class Clientes
    {
        conexionBD conexion = new conexionBD();
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string telefono{ get; set; }
        public string direccion { get; set; }
        public string mensaje { get; set; }

        public string usuario_responsable;

        public bool InsertarCliente()
        {
            conexion.Sentencia = "exec sp_inserta_clientes '" + nombre + "','" + apellido + "','" + cedula + "','" + telefono + "' , '" + direccion+ "' , '" + usuario_responsable + "' ;";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }


            conexion = null;
            return true;
        }

        public bool actualizaCliente()
        {
            conexion.Sentencia = "exec sp_actualizar_cliente '" + id + "','" + nombre + "','" + apellido + "','" + cedula + "','" + telefono + "' , '" + direccion + "' , '" + usuario_responsable + "' ;";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }


            conexion = null;
            return true;
        }
    }
}
