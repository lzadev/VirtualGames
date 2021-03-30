using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capa_negocio;

namespace Punto_ventas
{
    public partial class frmCambiarClave : Form
    {
        public frmCambiarClave()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string encriptar(string criptar) //encriptando la clave
        {
            try
            {

                string key = "qualityinfosolutions";

                byte[] keyArray;

                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(criptar);


                System.Security.Cryptography.MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                criptar = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

            }
            catch (Exception)
            {

            }
            return criptar;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();
            Usuarios user = new Usuarios();
            string cuenta = Login.currentCuenta;
            string claveACtualDB = Login.claveFromDB.Trim();
            string claveActualTXT = encriptar(txtActual.Text.Trim());

            try
            {
                if (txtNueva.Text.Length >= 6 && txtNueva.Text.Length <= 10)
                {
                    // do something here later
                    if (claveACtualDB.Trim() == claveActualTXT.Trim())
                    {
                        if (txtNueva.Text.Trim() == txtConfirmar.Text.Trim())
                        {
                            if (!user.cambiarClave(cuenta, encriptar(txtNueva.Text.Trim())))
                            {
                                f.txtMensaje.Text = user.mensaje;
                                f.Show();
                                return;
                            }
                            else
                            {
                                f.txtMensaje.Text = "Clave actualizada...!!!!";
                                f.Show();
                            }
                        }
                        else
                        {
                            f.txtMensaje.Text = "Las claves no coinciden...!!!!";
                            f.Show();
                            return;
                        }

                    }
                    else
                    {
                        f.txtMensaje.Text = "Clave actual no coincide...!!!!";
                        f.Show();
                        return;
                    }
                }
                else
                {
                    f.txtMensaje.Text = "La contrasena debe tener entre 6 y 10 caracteres";
                    f.Show();
                    return;
                }
            }
            catch(Exception ex)
            {
                f.txtMensaje.Text = ex.ToString();
                f.Show();
                return;
            }

            txtActual.Text = "";
            txtNueva.Text = "";
            txtConfirmar.Text = "";

        }

        
    }
}
