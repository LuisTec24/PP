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
using System.Xml.Linq;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using System.Drawing.Imaging;


namespace PPPP
{
    public partial class InterfazEdicion1 : Form
    {
        public StreamReader lector;
        PictureBox Hoja;
        int NC;
        double zoomFactor = 1.0;

        public InterfazEdicion1()
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
            pictureBox.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName); // Carga la imagen
            pnPrevisualizacion.Controls.Add(pictureBox);// Agrega el PictureBox al panel
            
            }
            catch{
            //error 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            InterfazPrincipal1 interfazPrincipal=new InterfazPrincipal1();

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

                // ------------------- 01/05/24 -----------------------------------------------
                case 4:
                    tamañoHoja = new Size(3508, 4961);
                    break;

                case 5:
                    tamañoHoja = new Size(5100, 6600);
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
            int nuevoAncho = (int)(tamañoActual.Width / (1.1 * zoomFactor)); // Reducir el ancho en función del factor de zoom
            int nuevoAlto = (int)(tamañoActual.Height / (1.1 * zoomFactor)); // Reducir el alto en función del factor de zoom
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
            AgrImgHoj(NC,zoomFactor);
                
        }

        private void AgrImgHoj(int nC, double zoomFactor)
        {
            Hoja.Controls.Clear();

            int hojaAncho = Hoja.Width; // Ancho del contenedor Hoja
            int maxImagenesPorLinea = hojaAncho / (100 + 20); // Calcula cuántas imágenes pueden caber en una línea
            int posX = 0; // Posición horizontal inicial
            int posY = 0; // Posición vertical inicial

            // Calcula el tamaño proporcional de las imágenes basado en el número de imágenes por línea
            int imagenAncho = (hojaAncho - 20 * (maxImagenesPorLinea - 1)) / maxImagenesPorLinea; // Ancho de la imagen con margen
            int imagenAlto = imagenAncho; // Altura de la imagen igual al ancho

            for (int i = 0; i < nC; i++)
            {
                // Crear un nuevo PictureBox para la imagen
                PictureBox pictureBox1 = new PictureBox();

                // Establecer el tamaño de la imagen
                pictureBox1.Size = new Size(imagenAncho, imagenAlto);

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Escalar la imagen para ajustarse al PictureBox
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName); // Cargar la imagen desde el archivo seleccionado

                pictureBox1.Location = new Point(posX, posY); // Establecer la posición del PictureBox en la hoja

                // Agregar el PictureBox al PictureBox de la hoja
                Hoja.Controls.Add(pictureBox1);

                posX += imagenAncho + 20; // Mueve la posición horizontal para la próxima imagen (considerando un margen de 20 píxeles entre imágenes)

                // Si llegamos al final de la línea, salta a la siguiente línea
                if ((i + 1) % maxImagenesPorLinea == 0)
                {
                    posX = 0; // Reinicia la posición horizontal
                    posY += imagenAlto + 20; // Mueve la posición vertical para la próxima línea (considerando un margen de 20 píxeles entre líneas)
                }
            }
            }

        private void label2_Click(object sender, EventArgs e)
        {
      
        }

        private void pnPrevisualizacion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Crear un bitmap del tamaño del PictureBox
            Bitmap bmp = new Bitmap(Hoja.Width, Hoja.Height);

            // Dibujar el contenido del PictureBox en el bitmap
            Hoja.DrawToBitmap(bmp, new Rectangle(0, 0, Hoja.Width, Hoja.Height));

            // Crear un cuadro de diálogo para guardar archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de imagen (*.jpg)|*.jpg|Documentos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
            saveFileDialog.Title = "Guardar como";

            // Si el usuario selecciona una ruta y hace clic en "Guardar"
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Guardar en formato PDF
                if (Path.GetExtension(saveFileDialog.FileName).ToLower() == ".pdf")
                {
                    using (PdfWriter writer = new PdfWriter(saveFileDialog.FileName))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document document = new Document(pdf);
                            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory.Create(bmp, null));
                            document.Add(img);
                        }
                    }
                    MessageBox.Show("La hoja se ha guardado correctamente en formato PDF.");
                }
                // Guardar en formato JPG
                else
                {
                    bmp.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                    MessageBox.Show("La hoja se ha guardado correctamente en formato JPG.");
                }
            }

            // Liberar los recursos del bitmap
            bmp.Dispose();













            /*
            // Crear un bitmap del tamaño del PictureBox
            Bitmap bmp = new Bitmap(Hoja.Width, Hoja.Height);

            // Dibujar el contenido del PictureBox en el bitmap
            Hoja.DrawToBitmap(bmp, new Rectangle(0, 0, Hoja.Width, Hoja.Height));

            // Crear un cuadro de diálogo para guardar archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de imagen (*.jpg)|*.jpg|Todos los archivos (*.*)|*.*";
            saveFileDialog.Title = "Guardar como";

            // Si el usuario selecciona una ruta y hace clic en "Guardar"
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Guardar el bitmap en la ruta seleccionada por el usuario
                bmp.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("La hoja se ha guardado correctamente.");
            }

            */
        }
    }

}
