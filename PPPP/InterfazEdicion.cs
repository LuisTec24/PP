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
        PictureBox Hoja;
        int NC;

        public InterfazEdicion()
        {

            InitializeComponent();
            openFileDialog1.ShowDialog(this);         
            
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
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            InterfazPrincipal interfazPrincipal=new InterfazPrincipal();

            // Mostrar el nuevo formulario
            this.Visible = false;
            interfazPrincipal.Show();


        }


        public void TPHoja(int tipoH)
        {
            Size tamañoHoja;
            switch (tipoH)
            {
                case 1: // Carta
                    tamañoHoja = new Size(2550 , 3300); // Tamaño en píxeles (ancho x alto)
                    break;

                case 2: // Oficio
                    tamañoHoja = new Size(2550 , 4200); // Tamaño en píxeles (ancho x alto)
                    break;

                case 3: // A4
                    tamañoHoja = new Size(2480 , 3508); // Tamaño en píxeles (ancho x alto)
                    break;





                default:
                    MessageBox.Show("Tipo de número no válido. Por favor, elija 1 para Carta o 2 para Oficio.");
                    return;
            }

            // Crear el PictureBox para la previsualización de la hoja
            Hoja = new PictureBox();
            
            Hoja.Left = 50;
            Hoja.BackColor = Color.White;
            Hoja.Top = 50;
            Hoja.Size = tamañoHoja; // Tamaño del PictureBox igual al tamaño de la hoja
            Hoja.SizeMode = PictureBoxSizeMode.Zoom; // Escalar la imagen para ajustarse al PictureBox

            // Cargar la imagen de la hoja en el PictureBox
            // (Asegúrate de reemplazar "HojaOriginal" con el nombre de tu imagen)
            //Hoja.Image = Properties.Resources.HojaOriginal; // Cambiar "HojaOriginal" por el nombre de tu imagen
            Hoja.Tag = tamañoHoja; // Almacenar el tamaño original de la imagen en el Tag del PictureBox
            //Hoja.SizeChanged += Hoja_SizeChanged;
            // Agregar el PictureBox al formulario
            this.PanelPre.Controls.Add(Hoja);

            // Agregar controles de zoom (por ejemplo, botones de zoom) al formulario
            // (Agrega aquí los controles que permitirán al usuario hacer zoom en la imagen)
        AbrirImagen(); // SELECCIONAR IMAGEN AL ABRIR LA VENTANA
        }

        // Método para hacer zoom en la imagen de la hoja
        private void ZoomIn(PictureBox pictureBox)
        {
            Size tamañoOriginal = (Size)pictureBox.Tag;
            Size tamañoActual = pictureBox.ClientSize;
            int nuevoAncho = (int)(tamañoActual.Width * 1.1);
            int nuevoAlto = (int)(tamañoActual.Height * 1.1);
            nuevoAncho = Math.Min(nuevoAncho, tamañoOriginal.Width);
            nuevoAlto = Math.Min(nuevoAlto, tamañoOriginal.Height);
            pictureBox.ClientSize = new Size(nuevoAncho, nuevoAlto);
        }

        // Método para alejar la imagen de la hoja
        private void ZoomOut(PictureBox pictureBox)
        {
            Size tamañoActual = pictureBox.ClientSize;
            int nuevoAncho = (int)(tamañoActual.Width / 1.1);
            int nuevoAlto = (int)(tamañoActual.Height / 1.1);
            pictureBox.ClientSize = new Size(nuevoAncho, nuevoAlto);
        }

        // Ejemplo de manejo de eventos para los botones de zoom
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn(Hoja);
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut(Hoja);
        }
//
        
        private void NCopias_ValueChanged(object sender, EventArgs e)
        {
            NC = (int)NCopias.Value;
            AgrImgHoj(NC);
                
        }

        private void AgrImgHoj(int nC)
        {
            Hoja.Controls.Clear();

            for (int i = 0; i < nC; i++)
            {
                // Crear un nuevo PictureBox para la imagen
                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.Size = new Size(100, 100); // Tamaño de la imagen dentro del panel
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Escalar la imagen para ajustarse al PictureBox
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName); // Cargar la imagen desde el archivo seleccionado

                // Calcular la posición en la hoja para cada imagen
                int posX = i * 120; // Ajusta el valor según el espacio que quieras dejar entre cada imagen
                int posY = 0; // Ajusta la posición vertical según tu diseño

                // Establecer la posición del PictureBox en la hoja
                pictureBox1.Location = new Point(posX, posY);

                // Agregar el PictureBox al PictureBox de la hoja
                Hoja.Controls.Add(pictureBox1);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
      
        }
        /*
private void Hoja_SizeChanged(object sender, EventArgs e)
{
   // Obtener el nuevo tamaño de la hoja

// Recorrer todos los PictureBox hijos de la hoja
   foreach (Control control in Hoja.Controls)
   {
       if (control is PictureBox pictureBox)
       {
           // Ajustar el tamaño del PictureBox hijo al nuevo tamaño de la hoja
           int nuevoAncho = (int)(Hoja.Width / 1.1);
           int nuevoAlto = (int)(Hoja.Height / 1.1);
           Hoja.ClientSize = new Size(nuevoAncho, nuevoAlto);
       }
   }


}*/



    }

}
