using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPPP
{
    public partial class InterfazPrincipal : Form
    {
        public InterfazPrincipal()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            InterfazEdicion interfazEdicion = new InterfazEdicion();

            // Mostrar el nuevo formulario
            this.Visible=false;
            interfazEdicion.Show();

        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backUpsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
