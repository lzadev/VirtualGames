using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capa_negocio;

namespace Punto_ventas
{
    public partial class frmClientes : Form
    {
        int oldOrNew;
        Clientes cliente;
        mostrarDatosEnGrid mt = new mostrarDatosEnGrid();
        public frmClientes()
        {
            InitializeComponent();
            estadoIniciarBotones();
            cargarDatosGrid(dataGridViewClientes);


        }

        private void cargarDatosGrid(DataGridView dataGrid)
        {
            mt.Sentencia = "exec sp_listar_clientes";

            if (!mt.cargarGrid(dataGrid))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buscar()
        {
            mt.Sentencia = "exec sp_buscar_cliente'" + txtBuscar.Text +"' ;";

            if (!mt.cargarGrid(dataGridViewClientes))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void estadoIniciarBotones()
        {
            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            txtNombre.Enabled = false;
            txtApeliido.Enabled = false;
            txtCedula.Enabled = false;
            txtTelefono.Enabled = false;
            txtDireccion.Enabled = false;   
        }

        private void activarControles()
        {
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            txtNombre.Enabled = true;
            txtApeliido.Enabled = true;
            txtCedula.Enabled = true;
            txtTelefono.Enabled = true;
            txtDireccion.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            oldOrNew = 1;
            btnNuevo.Enabled = false;
            activarControles();
        }

        private void reiniciarEstado()
        {
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            txtNombre.Enabled = false;
            txtApeliido.Enabled = false;
            txtCedula.Enabled = false;
            txtTelefono.Enabled = false;
            txtDireccion.Enabled = false;

            txtNombre.Text = "";
            txtApeliido.Text = "";
            txtCedula.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                reiniciarEstado();
            else
            {
                return;
            }
            
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

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se aceptan numero en el campo cedula", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se aceptan numero en el campo cedula", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Login user = new Login();
            frmMensaje f = new frmMensaje();

            string current_user = Login.currentCuenta;
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApeliido.Text.Trim();
            string cedula = txtCedula.Text;
            string telefeno = txtTelefono.Text;
            string direccion = txtDireccion.Text.Trim();

            cliente = new Clientes();


            if (oldOrNew == 0)
            {
               cliente.id = int.Parse(dataGridViewClientes.CurrentRow.Cells[0].Value.ToString());
            }


            cliente.nombre = nombre;
            cliente.apellido = apellido;
            cliente.cedula = cedula;
            cliente.telefono = telefeno;
            cliente.direccion = direccion;
            cliente.usuario_responsable = current_user;

            if(!string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(apellido) || !string.IsNullOrEmpty(cedula)
               || !string.IsNullOrEmpty(telefeno) || !string.IsNullOrEmpty(direccion))
            {
                if (cedula.Length < 11)
                {
                    f.txtMensaje.Text = "Debe de introducir un número de cedúla valido";
                    f.Show();
                    return;
                }

                if(telefeno.Length == 10)
                {
                    if (oldOrNew == 1)
                    {
                        if (!cliente.InsertarCliente())
                        {
                            f.txtMensaje.Text = cliente.mensaje;
                            f.Show();
                            return;
                        }
                        else
                        {
                            f.txtMensaje.Text = "Cliente creado Correctamente...!!!!";
                            f.Show();
                        }
                    }

                    if (oldOrNew == 0)
                    {
                        if (!cliente.actualizaCliente())
                        {
                            f.txtMensaje.Text = cliente.mensaje;
                            f.Show();
                            return;
                        }
                        else
                        {
                            f.txtMensaje.Text = "Cliente actualizado Correctamente...!!!!";
                            f.Show();
                        }
                    }

                }
                else
                {
                    f.txtMensaje.Text = "Debe de introducir un número de teléfono valido";
                    f.Show();
                    return;
                }
            }
            else
            {
                f.txtMensaje.Text = "LLenar todos los campos es obligatorio.";
                f.Show();
                return;
            }
            cargarDatosGrid(dataGridViewClientes);
            reiniciarEstado();
        }

        private void dataGridViewClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cliente = new Clientes();

            txtNombre.Text = dataGridViewClientes.CurrentRow.Cells[1].Value.ToString();
            txtApeliido.Text = dataGridViewClientes.CurrentRow.Cells[2].Value.ToString();
            txtCedula.Text = dataGridViewClientes.CurrentRow.Cells[3].Value.ToString();
            txtTelefono.Text = dataGridViewClientes.CurrentRow.Cells[4].Value.ToString();
            txtDireccion.Text = dataGridViewClientes.CurrentRow.Cells[5].Value.ToString();
            btnEditar.Enabled = true;
            oldOrNew = 0;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            activarControles();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
    }
}
