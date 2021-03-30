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
    public partial class frmProductos : Form
    {
        mostrarDatosEnGrid mt = new mostrarDatosEnGrid();

        int oldOrNew;
        public frmProductos()
        {
            InitializeComponent();
            cargarDatosGrid(dataGridViewProductos);
            estadoControlesIniciar();
            dataGridViewProductos.Columns[3].DefaultCellStyle.Format = "N2";
            dataGridViewProductos.Columns[4].DefaultCellStyle.Format = "N2";


        }


        private void estadoControlesIniciar()
        {
            txtDescripcion.Enabled = false;
            comboBoxCategoria.Enabled = false;
            txtCosto.Enabled = false;
            txtPorciento.Enabled = false;

            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = true;
        }

        private void cargarDatosGrid(DataGridView dataGrid)
        {
            mt.Sentencia = "exec sp_listar_productos";

            if (!mt.cargarGrid(dataGrid))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buscar()
        {
            mt.Sentencia = "exec sp_buscar_producto'" + txtBuscar.Text + "' ;";

            if (!mt.cargarGrid(dataGridViewProductos))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }






        private void frmProductos_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Productos producto = new Productos();
            frmMensaje f = new frmMensaje();

            if (oldOrNew == 0)
            {
                producto.id = int.Parse(dataGridViewProductos.CurrentRow.Cells[0].Value.ToString());
            }


            if (txtDescripcion.Text.Trim() != string.Empty)
            {
                producto.descripcion = txtDescripcion.Text;
                if (comboBoxCategoria.SelectedIndex >= 0)
                {
                    producto.idcategoria = comboBoxCategoria.Text;
                    if (txtCosto.Text.Trim() != string.Empty)
                    {
                        producto.costo = double.Parse(txtCosto.Text);
                        if (txtPorciento.Text !=string.Empty)
                        {
                            producto.porciente_subir = double.Parse(txtPorciento.Text);
                            if (oldOrNew == 1)
                            {
                                if (!producto.insertarProdcuto())
                                {
                                    f.txtMensaje.Text = producto.mensaje;
                                    f.Show();
                                    return;
                                }
                                else
                                {
                                    f.txtMensaje.Text = "Producto creado Correctamente...!!!!";
                                    f.Show();
                                }
                            }
                            if (oldOrNew == 0)
                            {
                                if (!producto.actualizarProdcuto())
                                {
                                    f.txtMensaje.Text = producto.mensaje;
                                    f.Show();
                                    return;
                                }
                                else
                                {
                                    f.txtMensaje.Text = "Actualizado creado Correctamente...!!!!";
                                    f.Show();
                                }
                            }
                        }
                        else
                        {
                            f.txtMensaje.Text = "Introducir un porciento igual o mayor que Cero..";
                            f.Show();
                            return;
                        }
                    }
                    else
                    {
                        f.txtMensaje.Text = "Debe introducir un precio costo para el producto";
                        f.Show();
                        return;
                    }
                }
                else
                {
                    f.txtMensaje.Text = "Debe seleccionar una categoria para el prodcuto";
                    f.Show();
                    return;
                }
            }
            else
            {
                f.txtMensaje.Text = "Debe introducir una descripcion para el producto";
                f.Show();
                return;
            }
            cargarDatosGrid(dataGridViewProductos);
            limpiar();
            estadoControlesIniciar();


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            oldOrNew = 1;

            txtDescripcion.Enabled = true;
            comboBoxCategoria.Enabled = true;
            txtCosto.Enabled = true;
            txtPorciento.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;


        }

        private void dataGridViewProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDescripcion.Text = dataGridViewProductos.CurrentRow.Cells[1].Value.ToString();
            comboBoxCategoria.Text = dataGridViewProductos.CurrentRow.Cells[2].Value.ToString();
            txtCosto.Text = dataGridViewProductos.CurrentRow.Cells[3].Value.ToString();
            btnEditar.Enabled = true;
            btnNuevo.Enabled = false;
            btnCancelar.Enabled = true;
            oldOrNew = 0;
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                MessageBox.Show("Solo se aceptan numero en el campo cedula", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                //MessageBox.Show("Solo se aceptan numero en el campo cedula", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtPorciento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                MessageBox.Show("Solo se aceptan numero en el campo cedula", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                MessageBox.Show("Solo se aceptan numero en el campo cedula", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            estadoControlesIniciar();
            limpiar();
            
        }

        private void limpiar()
        {
            txtDescripcion.Text = "";
            comboBoxCategoria.Refresh();
            txtCosto.Text = "";
            txtPorciento.Text = "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtDescripcion.Enabled = true;
            comboBoxCategoria.Enabled = true;
            txtCosto.Enabled = true;
            txtPorciento.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnEditar.Enabled = false;
        }
    }
}
