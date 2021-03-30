using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Punto_ventas
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {

            InitializeComponent();
            btnCategorias.Enabled = false;
            
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();
            frmUsuarios fUsuarios = new frmUsuarios();
            fUsuarios.MdiParent = this;
            //verifica si el formulario esta abierto 
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmUsuarios))
                {
                    f.txtMensaje.Text = "El formulario usuarios se encuentra actualmente abierto.....";
                    f.Show();
                    return;
                }
            }

            fUsuarios.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();
            frmCambiarClave cambiarClave = new frmCambiarClave();
            cambiarClave.MdiParent = this;
            //verifica si el formulario esta abierto 
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmCambiarClave))
                {
                    f.txtMensaje.Text = "El formulario cambiar clave se encuentra actualmente abierto.....";
                    f.Show();
                    return;
                }
            }

            cambiarClave.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();
            frmClientes clientes = new frmClientes();
            clientes.MdiParent = this;
            //verifica si el formulario esta abierto 
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmClientes))
                {
                    f.txtMensaje.Text = "El formulario clientes se encuentra actualmente abierto.....";
                    f.Show();
                    return;
                }
            }

            clientes.Show();
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();
            frmFacturacion facturar = new frmFacturacion();
            facturar.MdiParent = this;
            //verifica si el formulario esta abierto 
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmFacturacion))
                {
                    f.txtMensaje.Text = "El formulario facturación se encuentra actualmente abierto.....";
                    f.Show();
                    return;
                }
            }

            facturar.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();
            frmReportes fr = new frmReportes();
            fr.MdiParent = this;
            //verifica si el formulario esta abierto 
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmReportes))
                {
                    f.txtMensaje.Text = "El formulario de Reportes se encuentra actualmente abierto.....";
                    f.Show();
                    return;
                }
            }

            fr.Show();
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            {
                DialogResult dialogo = MessageBox.Show("¿ Desea Salir de la Aplicacion S/N ?",
                           "Salir de Aplicacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogo == DialogResult.OK) { }
                else { e.Cancel = true; }
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmMensaje f = new frmMensaje();
            frmProductos fp = new frmProductos();
            fp.MdiParent = this;
            //verifica si el formulario esta abierto 
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmFacturacion))
                {
                    f.txtMensaje.Text = "El formulario de Reportes se encuentra actualmente abierto.....";
                    f.Show();
                    return;
                }
            }

            fp.Show();

        }
    }
}
