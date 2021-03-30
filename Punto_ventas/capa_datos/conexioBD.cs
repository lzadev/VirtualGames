using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace capa_datos
{
    public class conexionBD
    {
        private SqlDataReader dr;
        private SqlConnection cnn;
        private SqlDataAdapter da;
        private DataTable dt;
        private DataSet ds;
        private SqlCommand comand;
        private bool conect;
        private string sentencia;
        private string mensaje;
        public object unico;


       
        public SqlDataReader Dr
        {
            get { return dr; }
        }
        public SqlConnection Cnn
        {
            get { return cnn; }
        }
        public SqlDataAdapter Da
        {
            get { return da; }
        }
        public DataTable Dt
        {
            get { return dt; }
        }

        public DataSet Ds
        {
            get { return ds; }
        }

        public string Sentencia
        {
            set { sentencia = value; }
        }

        public string Mensaje
        {
            get { return mensaje; }
        }



        public conexionBD()
        {
            da = new SqlDataAdapter();
            cnn = new SqlConnection();
            dt = new DataTable();
            ds = new DataSet();
            comand = new SqlCommand();
            sentencia = string.Empty;
            conect = false;
            mensaje = string.Empty;
            unico = null;
        }
        
        
       public string conexion = "Data Source=MSI; Initial Catalog=PuntoVenta; Integrated Security=True";

        private bool abriendoConexion()
        {
            cnn.ConnectionString = conexion;

            try
            {
                cnn.Open();
                conect = true;
                return true;
            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
                conect = false;
                return false;
            }
        }

        private void cerrar()
        {
            try
            {
                cnn.Close();
                conect = false;
            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public bool consultar()
        {
            try
            {
                if(string.IsNullOrEmpty(sentencia))
                {
                    mensaje = "the system dot not found a valid sentence";
                    return false;
                }
                if (!conect)
                {
                    if (!abriendoConexion())
                    {
                        mensaje = "The system can not get a valid conection";
                        return false;
                    }
                }

                dt = null;
                da = new SqlDataAdapter(sentencia, cnn);
                ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                return true;

            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open) cerrar();
            }

        }

        public bool EjecutarSentencia()
        {
            try
            {
                if (string.IsNullOrEmpty(sentencia))
                {
                    sentencia = "No definió la instrucción SQL";
                    return false;
                }
                if (!conect)
                {
                    if (!abriendoConexion()) return false;
                }

                comand.Connection = cnn;
                comand.CommandText = sentencia;
                comand.CommandType = CommandType.Text;
                comand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                ///if (cnn.State == ConnectionState.Open) cerrar();
            }
        }

        public bool ConsultarUnico()
        {
            try
            {
                if (string.IsNullOrEmpty(sentencia))
                {
                    mensaje = "No definió la instrucción SQL.";
                    return false;
                }
                if (!conect)
                {
                    if (!abriendoConexion())
                    {
                        mensaje = "No hay conexión con la Base de Datos.";
                        return false;
                    }
                }

                comand.Connection = cnn;
                comand.CommandText = sentencia;
                comand.CommandType = CommandType.Text;
                unico = comand.ExecuteScalar();

                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open) cerrar();
            }
        }

        public bool mostrarDatos()
        {
            try
            {
                if (string.IsNullOrEmpty(sentencia))
                {
                    sentencia = "No definió la instrucción SQL";
                    return false;
                }
                if (!conect)
                {
                    if (!abriendoConexion()) return false;
                }

                //Preparar el Comando para el DataAdapter
                comand.Connection = cnn;
                comand.CommandText = sentencia;
                // if (blnParametros)
                //    cmd.CommandType = CommandType.StoredProcedure;
                //else
                comand.CommandType = CommandType.Text;

                //Preparar el DataAdapter parael uso del comando en la BD
                da.SelectCommand = comand; //El DataAdapter Utiliza el Command para la transacción

                //Realizar la transacción en la BD y el llenado del DataSet/Datatable
                ds.Clear();
                da.Fill(ds);
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open) cerrar();
            }
        }




        //public static DataSet ejecutar(string sentencia)
        //{
        //    SqlConnection conex = new SqlConnection(conexion);

        //    conex.Open();

        //    DataSet ds = new DataSet();

        //    SqlDataAdapter ad = new SqlDataAdapter(sentencia, conex);

        //    ad.Fill(ds);

        //    conex.Close();

        //    return ds;
        //}
    }
}
