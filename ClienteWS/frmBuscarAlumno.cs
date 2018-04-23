using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteWS
{
    public partial class frmBuscarAlumno : Form
    {
        public frmBuscarAlumno()
        {
            InitializeComponent();
            lblMensaje.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            var dni = Convert.ToInt32( txtIdenficacion.Text);

            using (WSPersona.WSPersonaClient cliente = new WSPersona.WSPersonaClient())
            {
                var alumno = cliente.BuscarAlumno(dni);
                txtNombre.Text = alumno.Nombre;
                txtDNI.Text = alumno.Dni.ToString();
                lblMensaje.Text = alumno.Error;
            }

        }
    }
}
