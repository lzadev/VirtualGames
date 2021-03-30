using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa_datos;

namespace capa_negocio
{
    public class Productos
    {
         /*
            @descripcion varchar(200),
            @categoria varchar(100),
            @costo decimal,
            @porciento decimal

         */


        public string descripcion { get; set; }
        public string idcategoria { get; set; }
        public double costo { get; set; }
        public double porciente_subir { get; set; }

        public int id { get; set; }

        public string mensaje { get; set; }

        conexionBD conexion = new conexionBD();

        public bool insertarProdcuto()
        {
            conexion.Sentencia = "exec sp_crear_producto '" + descripcion + "','" + idcategoria + "','" + costo + "','" + porciente_subir + "' ;";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }

            conexion = null;
            return true;
        }

        public bool actualizarProdcuto()
        {
            conexion.Sentencia = "exec sp_actualizar_producto '"+ id +"','"+ descripcion + "','" + idcategoria + "','" + costo + "','" + porciente_subir + "' ;";

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
