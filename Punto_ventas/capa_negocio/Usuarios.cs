using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_datos;

namespace capa_negocio
{
    public class Usuarios
    {
        conexionBD conexion = new conexionBD();
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public bool status { get; set; }

        public int id { get; set; }

        public string mensaje { get; set; }

        public bool InsertarUsuario()
        {
            conexion.Sentencia = "exec sp_usuario_insertar '" + nombre + "','" + apellido + "','" + cedula + "','" + usuario + "' , '" + password + "' , '" + status+ "' ;";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }

            conexion = null;
            return true;
        }

        public bool actualizarUsuario()
        {
            conexion.Sentencia = "exec sp_actualiza_usuario '"+ id + "','" + nombre + "','" + apellido + "','" + cedula + "','" + usuario + "' , '" + password + "' ;";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }

            conexion = null;
            return true;
        }

        public bool activarDesactivar()
        {
            conexion.Sentencia = "sp_activa_desactiva_usuario '" + id + "' ;";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }

            conexion = null;
            return true;
        }

        public bool cambiarClave(string nameUser ,string  clave)
        {
            conexion.Sentencia = "sp_cambiar_clave '" + nameUser + "','" + clave + "';";

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
