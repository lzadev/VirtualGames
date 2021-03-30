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
    public partial class frmReportes : Form
    {
        mostrarDatosEnGrid mt = new mostrarDatosEnGrid();
        public frmReportes()
        {
            InitializeComponent();
        }

        private void cargarDatosGrid(DataGridView dataGrid)
        {
            mt.Sentencia = "exec sp_reporte_venta";

            if (!mt.cargarGrid(dataGrid))
            {
                MessageBox.Show("El Grid no pudo llenarse: " + mt.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            double total = 0;
            frmMensaje fm = new frmMensaje();
            if(comboBoxReporte.SelectedIndex == 0)
            {
                dataGridViewReporte.Enabled = true;

                cargarDatosGrid(dataGridViewReporte);
                dataGridViewReporte.Columns[4].DefaultCellStyle.Format = "N2";
         

                foreach (DataGridViewRow i in dataGridViewReporte.Rows)
                {
                    total += Convert.ToDouble(i.Cells[4].Value);

                }

                textTotal.Text = "RD$"+total.ToString("N2");
            }
            else
            {
                fm.txtMensaje.Text = "Debe seleccionar el reporte a generar";
                fm.Show();
                return;
            }

        }
    }
}
