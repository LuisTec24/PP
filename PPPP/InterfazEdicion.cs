using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPPP
{
    public partial class InterfazEdicion : Form
    {
        public StreamReader lector;
        public InterfazEdicion()
        {

            InitializeComponent();
            openFileDialog1.ShowDialog(this);
            AbrirImagen(); // SELECCIONAR IMAGEN AL ABRIR LA VENTANA

        }

        private void AbrirImagen()
        {
            
            lector = new StreamReader(openFileDialog1.FileName);
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = pnPrevisualizacion.Size; // Tamaño de la imagen dentro del panel
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Escala la imagen para ajustarse al PictureBox
            pictureBox.Image = Image.FromFile(openFileDialog1.FileName); // Carga la imagen
            pnPrevisualizacion.Controls.Add(pictureBox);// Agrega el PictureBox al panel
        }



        /*private void BitmapRecortar()
        {
            Rectangle rectOrig = new Rectangle(posXmin, posYmin, anchura, altura);
            Bitmap source = new Bitmap(openFileDialog1.FileName);


            Rectangle rectOrig = new Rectangle(posXmin, posYmin, anchura, altukra);

            Bitmap CroppedImage = CropImage(source, rectOrig);hh

        }
        */
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            InterfazPrincipal interfazPrincipal=new InterfazPrincipal();

            // Mostrar el nuevo formulario
            this.Visible = false;
            //interfazPrincipal.Show();


        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
