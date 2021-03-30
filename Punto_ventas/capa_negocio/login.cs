using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using capa_datos;

namespace capa_negocio
{
    public class Login
    {
      
        private static string mensaje;

        public static string claveFromDB;

        public static string currentCuenta;

        public static string Mensaje
        {
            get { return mensaje; }
        }


        private static conexionBD conexion = new conexionBD();
        public static bool ExisteUsuario(string usuario)
        {

            conexion.Sentencia = "Exec ExisteUsuario '" + usuario + "'";

            if (!conexion.ConsultarUnico())
            {
                mensaje = conexion.Mensaje;
                return false;
            }
            if (conexion.unico == null)
            {
                mensaje = "El usuario ingresado no existe.";
                return false;
            }
            return true;
        }

        public static bool UsuarioActivo(string usuario)
        {
            conexion.Sentencia = "Exec UsuarioActivo '" + usuario + "'";

            if (!conexion.ConsultarUnico())
            {
                mensaje = conexion.Mensaje;
                return false;
            }
            if (conexion.unico == null)
            {
                mensaje = " El usuario ingresado está inhabilitado.\n Pongase en contacto con el administrador del sistema.";
                return false;
            }
            return true;
        }


        public static bool ValidarUsuario(string usuario, string clave)
        {

            conexion.Sentencia = " Exec ValidaUsuario '" + usuario + "','" + clave + "'";

            if (!conexion.ConsultarUnico())
            {
                mensaje = conexion.Mensaje;
                return false;
            }

            if (conexion.unico == null)
            {
                mensaje = "La contraseña introducida no es correcta.";
                return false;

            }

            currentCuenta = usuario;
            claveFromDB = clave;
            return true;

        }
        //public bool logearse()
        //{
        //    try
        //    {
        //        sentencia = string.Format("select * from login where usuario = '{0}' and contrasena = '{1}'", cuenta, password);

        //        DataSet ds = conexionBD.ejecutar(sentencia);

        //        if(ds.Tables[0].Rows[0]["usuario"].ToString() == cuenta && ds.Tables[0].Rows[0]["contrasena"].ToString() == password )
        //        {
        //            confirmarPass = ds.Tables[0].Rows[0]["contrasena"].ToString();

        //            return true;
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        mensaje= "Usuario o Contraseña incorrecta " + ex.Message;
        //        return false;
        //    }

        //    return true;
        //}

        //private bool existeUsuario(string user)
        //{
        //    sentencia = string.Format("select * from login where usuario = '{0}' and contrasena = '{1}'", cuenta, password);



        //}






    }
}
