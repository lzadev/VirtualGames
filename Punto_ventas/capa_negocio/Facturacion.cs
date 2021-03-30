using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using capa_datos;
using System.Data;

namespace capa_negocio
{
    public class Facturacion
    {
        conexionBD conexion = new conexionBD();
        public int idCliente { get; set; }
        public string usuario { get; set; }

        public string mensaje { get; set; }

        public int idProducto { get; set; }
        public int candidad { get; set; }
        public double precio { get; set; }

        public int numeroFactura;


        public bool InsertarFactura()
        {
            conexion.Sentencia = "exec sp_actualiza_venta '" + idCliente + "','" + usuario+ "';";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }
            return true;
        }

        /*@idcliente int,
        @id_producto int,
        @cantidad int,
        @precio decimal(18,2)
        */

        public bool facturar(int idProducto,int cantidad,double precio)
        {
            conexion.Sentencia = "exec sp_actualiza_detalle'" + idProducto+ "','" +cantidad+ "','" + precio + "';";

            if (!conexion.EjecutarSentencia())
            {
                mensaje = conexion.Mensaje;
                return false;
            }

            return true;
        }


    }
}
