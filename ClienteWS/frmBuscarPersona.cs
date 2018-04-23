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
    public partial class frmBuscarPersona : Form
    {
        public frmBuscarPersona()
        {
            InitializeComponent();
            lblMensaje.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            var dni = txtIdenficacion.Text;

            using (WSPersona.WSPersonaClient client = new WSPersona.WSPersonaClient())
            {
                var persona = client.ObtenerPersona(dni);
                txtNombre.Text = persona.Nombre;
                txtEdad.Text = persona.Edad.ToString();
                lblMensaje.Text = persona.Error;
            }

        }
    }
}
