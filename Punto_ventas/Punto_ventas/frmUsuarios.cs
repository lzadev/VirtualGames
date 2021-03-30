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
    public partial class frmUsuarios : Form
    {
        mostrarDatosEnGrid mt = new mostrarDatosEnGrid();

        int oldOrNew;

        Usuarios user;

        public frmUsuarios()
        {
            InitializeComponent();
            cargarDatosGrid(dataGridViewUsuarios);
            estadoIniciarBotones();
            desactivaControles();
        }

        private void frmUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtApeliido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten Letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }


        private void cargarDatosGrid( DataGridView dataGrid)
        {
            mt.Sentencia = "exec sp_mostrar_usuarios";

            if (!mt.cargarGrid(dataGrid))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //metodo que desencripta la contrasena
        public static string Desencriptar(string textoEncriptado)
        {
            try
            {
                string key = "qualityinfosolutions";
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception)
            {

            }
            return textoEncriptado;
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
        private void frmUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();

            string nombre = txtNombre.Text.Trim();
            string apellido = txtApeliido.Text.Trim();
            string cedula = txtCedula.Text;
            string usuario = txtUsuario.Text.Trim();
            string password = encriptar(txtContrasena.Text.Trim());
            bool status = checkBoxStatus.Checked;

            user = new Usuarios();

            if (oldOrNew == 0)
            {
                user.id = int.Parse(dataGridViewUsuarios.CurrentRow.Cells[0].Value.ToString());
            }

            user.nombre = nombre;
            user.apellido = apellido;
            user.cedula = cedula;
            user.usuario = usuario;
            user.password = password.Trim();
            user.status = status;

            if (txtNombre.Text == string.Empty || txtApeliido.Text == string.Empty || txtCedula.Text == string.Empty ||
                txtUsuario.Text.Trim() == string.Empty || txtContrasena.Text.Trim() == string.Empty || txtConfirmar.Text.Trim() == string.Empty)
            {
                f.txtMensaje.Text = "Debe llenar todo los campos obligatoriamente";
                f.Show();
                return;
            }

            if (txtCedula.Text.Trim().Length != 11)
            {
                f.txtMensaje.Text = "Debe de introducir un numero de cedula valido";
                f.Show();
                return;
            }

            if (txtContrasena.Text.Length >= 6 && txtContrasena.Text.Length <= 10)
            {

                if (txtContrasena.Text.Trim() == txtConfirmar.Text.Trim())
                {
                    if (oldOrNew == 1)
                    {
                        if (!user.InsertarUsuario())
                        {
                            f.txtMensaje.Text = user.mensaje;
                            f.Show();
                            return;
                        }
                        else
                        {
                            f.txtMensaje.Text = "Usuario creado Correctamente...!!!!";
                            f.Show();
                        }
                    }

                    if(oldOrNew == 0)
                    {
                        if (!user.actualizarUsuario())
                        {
                            f.txtMensaje.Text = user.mensaje;
                            f.Show();
                            return;
                        }
                        else
                        {
                            f.txtMensaje.Text = "Usuario actualizado Correctamente...!!!!";
                            f.Show();
                        }

                    }

                }
                else
                {
                    lblContrasena.Text = "Las contraseñas no coinciden";
                    lblConfirmar.Text = "Las contraseñas no coinciden";
                    return;
                }
            }
            else
            {
                f.txtMensaje.Text = "La contrasena debe tener entre 6 y 10 caracteres";
                f.Show();
                return;
            }
            //refresca el datagrip mostrando le nuevo usuario creado
            cargarDatosGrid(dataGridViewUsuarios);

            desactivaControles();
            estadoIniciarBotones();
            limpiarControles();
            btnNuevo.Enabled = true;
            lblConfirmar.Text = "";
            lblContrasena.Text = "";

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            //condicion que evalua que no sean introducidos espacios en blancos ni caracteres extra;os
            if (!(char.IsLetterOrDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("No se permiten espacios en blanco ni caracteres extraños", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
                //esta condicion lanza una advertencia al usuario cuando excede el maximo de caracteres permiticos en en el compo contrasena
            if(this.txtContrasena.Text.Length ==10)
            {
                lblContrasena.Text = "Esta exediendo el maximo de caracteres permitidos";
            }
        }

        //evaluando que solo se introduzcan numeros en el campo cedula
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            frmMensaje fm = new frmMensaje();
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se aceptan numero como cantidad", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        //metodo que activa los controles necesarios para crear un nuevo usuario
        private void activaControles()
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            txtNombre.Enabled = true;
            txtApeliido.Enabled = true;
            txtCedula.Enabled = true;
            txtUsuario.Enabled = true;
            txtContrasena.Enabled = true;
            txtConfirmar.Enabled = true;
        }

        //metodo que desactiva los controles 
        private void desactivaControles()
        {
            txtNombre.Enabled = false;
            txtApeliido.Enabled = false;
            txtCedula.Enabled = false;
            txtUsuario.Enabled = false;
            txtContrasena.Enabled = false;
            txtConfirmar.Enabled = false;
            checkBoxStatus.Enabled = false;
        }

        // metodo que le da estado iniciar a los botones
        private void estadoIniciarBotones()
        {
            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            btnStatus.Enabled = false;
        }

        //boton nuevo que activa todo los controles para crear un nuevo usuario
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            oldOrNew = 1;
            checkBoxStatus.Enabled = true;
            activaControles();
        }

        // boton cancelar que vuelve el formulario a su estado iniciar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            estadoIniciarBotones();
            desactivaControles();
            limpiarControles();
            btnNuevo.Enabled = true;
        }

        //metodo que activa los textbox del formulario
        private void limpiarControles()
        {
            txtNombre.Text = "";
            txtApeliido.Text = "";
            txtCedula.Text = "";
            txtUsuario.Text = "";
            txtContrasena.Text = "";
            txtConfirmar.Text = "";
            checkBoxStatus.Checked = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            oldOrNew = 0;
            activaControles();
            btnStatus.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void dataGridViewUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            user = new Usuarios();

            user.id = int.Parse(dataGridViewUsuarios.CurrentRow.Cells[0].Value.ToString());

            txtNombre.Text = dataGridViewUsuarios.CurrentRow.Cells[1].Value.ToString();
            txtApeliido.Text = dataGridViewUsuarios.CurrentRow.Cells[2].Value.ToString();
            txtCedula.Text = dataGridViewUsuarios.CurrentRow.Cells[3].Value.ToString();
            txtUsuario.Text = dataGridViewUsuarios.CurrentRow.Cells[4].Value.ToString().Trim();
            txtContrasena.Text = Desencriptar(dataGridViewUsuarios.CurrentRow.Cells[5].Value.ToString());
            txtConfirmar.Text = Desencriptar(dataGridViewUsuarios.CurrentRow.Cells[5].Value.ToString());
            checkBoxStatus.Checked = bool.Parse(dataGridViewUsuarios.CurrentRow.Cells[6].Value.ToString());
            desactivaControles();
            btnEditar.Enabled = true;
            btnStatus.Enabled = true;
            btnGuardar.Enabled = false;
           
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();


            if(checkBoxStatus.Checked == true)
            {
                if (!user.activarDesactivar())
                {
                    f.txtMensaje.Text = user.mensaje;
                    f.Show();
                    return;
                }
                else
                {
                    f.txtMensaje.Text = "El usuario ha sido desactivado correctamente!!!".ToUpper();
                    f.Show();
                }
            }

            if(checkBoxStatus.Checked == false)
            {
                if (!user.activarDesactivar())
                {
                    f.txtMensaje.Text = user.mensaje;
                    f.Show();
                    return;
                }
                else
                {
                    f.txtMensaje.Text = "El usuario ha sido activado correctamente!!!".ToUpper();
                    f.Show();
                }
            }

            estadoIniciarBotones();
            limpiarControles();
            //refresca el datagrip mostrando le nuevo usuario creado
            cargarDatosGrid(dataGridViewUsuarios);

        }

        private void txtConfirmar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //condicion que evalua que no sean introducidos espacios en blancos ni caracteres extra;os
            if (!(char.IsLetterOrDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("No se permiten espacios en blanco ni caracteres extraños", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
            //esta condicion lanza una advertencia al usuario cuando excede el maximo de caracteres permiticos en en el compo contrasena
            if (this.txtContrasena.Text.Length == 10)
            {
                lblConfirmar.Text = "Esta exediendo el maximo de caracteres permitidos";
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //condicion que evalua que no sean introducidos espacios en blancos ni caracteres extra;os
            if (!(char.IsLetterOrDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("No se permiten espacios en blanco ni caracteres extraños", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void buscar()
        {
            mt.Sentencia = "exec sp_buscar_usuarios'" + txtBuscar.Text + "' ;";

            if (!mt.cargarGrid(dataGridViewUsuarios))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
    }
    
}
