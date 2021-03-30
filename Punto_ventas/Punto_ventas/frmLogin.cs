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
    public partial class frmLogin : Form
    {
        //Login datos; 

        public frmLogin()
        {
            InitializeComponent();
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

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {   
                if (txtUsuario.Text != string.Empty)
                {
                    if (txtPass.Text != string.Empty)
                    {
                        if (!Login.ExisteUsuario(txtUsuario.Text.Trim()))
                        {
                            MessageBox.Show(Login.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        if (!Login.UsuarioActivo(txtUsuario.Text.Trim()))
                        {
                            MessageBox.Show(Login.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        if (Login.ValidarUsuario(txtUsuario.Text.Trim(),encriptar(txtPass.Text.Trim())))
                        {

                            frmPrincipal principal = new frmPrincipal();
                            principal.labelUser.Text = txtUsuario.Text;
                            principal.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show(Login.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar una contraseña");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Debe Ingresar un usuario");
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
