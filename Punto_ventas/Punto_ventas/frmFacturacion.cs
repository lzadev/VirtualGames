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
    
    public partial class frmFacturacion : Form
    {
        mostrarDatosEnGrid mt = new mostrarDatosEnGrid();
        public frmFacturacion()
        {
            InitializeComponent();
            textTotal.Text = "RD$00.0";

        }

        
        private void btnBuscarProductos_Click(object sender, EventArgs e)
        {
            frmListadoProductos p = new frmListadoProductos();
            p.ShowDialog();
            if (p.DialogResult == DialogResult.OK)
            {
                txtCodigo.Text = p.dataGridViewProductos.Rows[p.dataGridViewProductos.CurrentRow.Index].Cells[0].Value.ToString();
                txtDescripcion.Text = p.dataGridViewProductos.Rows[p.dataGridViewProductos.CurrentRow.Index].Cells[1].Value.ToString();
                txtPrecio.Text = p.dataGridViewProductos.Rows[p.dataGridViewProductos.CurrentRow.Index].Cells[4].Value.ToString();

                txtCantidad.Focus();
            }
        }


        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmListadoClientes c = new frmListadoClientes();
            c.ShowDialog();
            if (c.DialogResult == DialogResult.OK)
            {
                txtIdCliente.Text = c.dataGridViewClientes.Rows[c.dataGridViewClientes.CurrentRow.Index].Cells[0].Value.ToString();
                txtNombre.Text = c.dataGridViewClientes.Rows[c.dataGridViewClientes.CurrentRow.Index].Cells[1].Value.ToString() + " " + c.dataGridViewClientes.Rows[c.dataGridViewClientes.CurrentRow.Index].Cells[2].Value.ToString();
                txtCedula.Text = c.dataGridViewClientes.Rows[c.dataGridViewClientes.CurrentRow.Index].Cells[3].Value.ToString();
                txtTelefono.Text = c.dataGridViewClientes.Rows[c.dataGridViewClientes.CurrentRow.Index].Cells[4].Value.ToString();
                txtDIreccion.Text = c.dataGridViewClientes.Rows[c.dataGridViewClientes.CurrentRow.Index].Cells[5].Value.ToString();
            }
        }


        public static double total;
        public static int fila = 0;
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            frmMensaje fm = new frmMensaje();
            if (txtCodigo.Text != string.Empty.Trim() && txtDescripcion.Text.Trim() != string.Empty &&
              txtPrecio.Text.Trim() != string.Empty)
            {
                if (txtCantidad.Text.Trim() != string.Empty)
                {
                    bool ex = false;
                    int num = 0;

                    if (fila == 0)
                    {
                        dataGridViewProductos.Rows.Add(txtCodigo.Text, txtDescripcion.Text, txtCantidad.Text,txtPrecio.Text);

                        double importe = Convert.ToDouble(dataGridViewProductos.Rows[fila].Cells[2].Value) * Convert.ToDouble(dataGridViewProductos.Rows[fila].Cells[3].Value);

                        dataGridViewProductos.Rows[fila].Cells[4].Value = importe;

                        dataGridViewProductos.Columns[4].DefaultCellStyle.Format = "N2";

                        fila++;
                    }
                    else
                    {
                        foreach (DataGridViewRow i in dataGridViewProductos.Rows)
                        {
                            if (i.Cells[0].Value.ToString() == txtCodigo.Text)
                            {
                                ex = true;

                                num = i.Index;


                            }
                        }

                        if (ex == true)
                        {
                            dataGridViewProductos.Rows[num].Cells[3].Value = (Convert.ToDouble(txtCantidad.Text) + Convert.ToDouble(dataGridViewProductos.Rows[num].Cells[3].Value)).ToString();
                            double importe = Convert.ToDouble(dataGridViewProductos.Rows[num].Cells[2].Value) * Convert.ToDouble(dataGridViewProductos.Rows[num].Cells[3].Value);

                            dataGridViewProductos.Rows[num].Cells[4].Value = importe;

                        }
                        else
                        {
                            dataGridViewProductos.Rows.Add(txtCodigo.Text, txtDescripcion.Text,txtCantidad.Text ,txtPrecio.Text);

                            double importe = Convert.ToDouble(dataGridViewProductos.Rows[fila].Cells[2].Value) * Convert.ToDouble(dataGridViewProductos.Rows[fila].Cells[3].Value);

                            dataGridViewProductos.Rows[fila].Cells[4].Value = importe;

                            fila++;
                        }
                    }

                    total = 0;

                    foreach (DataGridViewRow i in dataGridViewProductos.Rows)
                    {
                        total += Convert.ToDouble(i.Cells[4].Value);


                    }

                    textTotal.Text = "RD$" + total.ToString("N2");

                    txtCodigo.Text = "";
                    txtDescripcion.Text = "";
                    txtPrecio.Text = "";
                    txtCantidad.Text = "";
                }
                else
                {
                    fm.txtMensaje.Text = "Debe de introducir una cantidad";
                    fm.Show();
                    return;
                }

            }
            else
            {
                fm.txtMensaje.Text = "Debe seleccionar un producto";
                fm.Show();
                return;
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmMensaje fm = new frmMensaje();
            if(txtIdCliente.Text != string.Empty.Trim() && txtNombre.Text.Trim()!=string.Empty && txtCedula.Text.Trim() != string.Empty.Trim() &&
                txtTelefono.Text.Trim() != string.Empty && txtDIreccion.Text.Trim() != string.Empty)
            {
                int idCliente = int.Parse(txtIdCliente.Text);
                string usuario = Login.currentCuenta;

                //instanciando la clase facturacion
                Facturacion factura = new Facturacion();

                factura.idCliente = idCliente;
                factura.usuario = usuario;

                if (dataGridViewProductos.Rows.Count != 0)
                {
                    if (!factura.InsertarFactura())
                    {
                        MessageBox.Show(factura.mensaje);
                        return;
                    }

                    foreach (DataGridViewRow f in dataGridViewProductos.Rows)
                    {
                        if (!factura.facturar(int.Parse(f.Cells[0].Value.ToString()), int.Parse(f.Cells[2].Value.ToString()), Convert.ToDouble(f.Cells[3].Value.ToString())))
                        {
                            MessageBox.Show(factura.mensaje);
                            return;
                        }


                    }
                }
                else
                {
                    fm.txtMensaje.Text = "No hay productos agragados para facturar.";
                    fm.Show();
                    return;
                }
            }
            else
            {
                fm.txtMensaje.Text = "Debe seleccionar un cliente.";
                fm.Show();
                return;
            }
            resetControls();
            fm.txtMensaje.Text = "Producto(s) facturado(s) con exíto!!!..";
            fm.Show();

        }

        private void resetControls()
        {
            dataGridViewProductos.DataSource = "";
            txtIdCliente.Text = "";
            txtNombre.Text = "";
            txtCedula.Text = "";
            txtTelefono.Text = "";
            txtDIreccion.Text = "";
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            textTotal.Text = "RD$00.0";
        }

        private void frnFacturacion_Load(object sender, EventArgs e)
        {
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (fila > 0)
            {
                total = total - (Convert.ToDouble(dataGridViewProductos.Rows[dataGridViewProductos.CurrentRow.Index].Cells[4].Value));


                textTotal.Text = "RD$"+total.ToString();

                dataGridViewProductos.Rows.RemoveAt(dataGridViewProductos.CurrentRow.Index);
                fila -= 1;

            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Debe introducir un valor valído", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           resetControls();
        }


    }
}
