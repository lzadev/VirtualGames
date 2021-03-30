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
    public partial class frmListadoProductos : Form
    {
        mostrarDatosEnGrid mt = new mostrarDatosEnGrid();
        public frmListadoProductos()
        {
            InitializeComponent();
            cargarDatosGrid(dataGridViewProductos);
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

        private void dataGridViewProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           // Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (dataGridViewProductos.Rows.Count == 0)
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
            mt.Sentencia = "exec sp_buscar_producto'" + txtBuscar.Text + "' ;";

            if (!mt.cargarGrid(dataGridViewProductos))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
    }
}
