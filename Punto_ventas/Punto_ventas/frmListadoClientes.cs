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
    public partial class frmListadoClientes : Form
    {

        mostrarDatosEnGrid mt = new mostrarDatosEnGrid();
        public frmListadoClientes()
        {
            InitializeComponent();
            cargarDatosGrid(dataGridViewClientes);

        }

        private void frmListadoClientes_Load(object sender, EventArgs e)
        {

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

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.Rows.Count == 0)
            {
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buscar()
        {
            mt.Sentencia = "exec sp_buscar_cliente'" + txtBuscar.Text + "' ;";

            if (!mt.cargarGrid(dataGridViewClientes))
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
