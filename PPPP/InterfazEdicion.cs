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
            try {    
            lector = new StreamReader(openFileDialog1.FileName);
            
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = pnPrevisualizacion.Size; // Tamaño de la imagen dentro del panel
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Escala la imagen para ajustarse al PictureBox
            pictureBox.Image = Image.FromFile(openFileDialog1.FileName); // Carga la imagen
            pnPrevisualizacion.Controls.Add(pictureBox);// Agrega el PictureBox al panel
            }
            catch{
            //error 
            }
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
            interfazPrincipal.Show();


        }

        private void button7_Click(object sender, EventArgs e)
        {

        }


        public void TPHoja(int tipoH)
        {
            Size tamañoHoja;
            switch (tipoH)
            {
                case 1: // Carta
                    tamañoHoja = new Size(450, 550); // Tamaño en píxeles (ancho x alto)
                    break;


                case 2: // Oficio
                    tamañoHoja = new Size(850, 1400); // Tamaño en píxeles (ancho x alto)
                    break;
                default:
                    MessageBox.Show("Tipo de número no válido. Por favor, elija 1 para Carta o 2 para Oficio.");
                    return;
            }

            // Crear el PictureBox para la previsualización de la hoja
            PictureBox Hoja = new PictureBox();
            Hoja.Size = tamañoHoja; // Tamaño del PictureBox igual al tamaño de la hoja
            Hoja.SizeMode = PictureBoxSizeMode.Zoom; // Escalar la imagen para ajustarse al PictureBox

            // Cargar una imagen transparente del tamaño de la hoja
            Bitmap imagenHoja = new Bitmap(tamañoHoja.Width, tamañoHoja.Height);
            using (Graphics g = Graphics.FromImage(imagenHoja))
            {
                g.Clear(Color.Transparent);
            }
            Hoja.Image = imagenHoja;
            Hoja.BackColor = Color.Black;

            // Agregar el PictureBox al formulario
            this.Controls.Add(Hoja);
        }




    }

}
