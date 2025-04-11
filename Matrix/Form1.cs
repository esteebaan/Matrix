using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;



namespace Matrix


{

    public partial class Form1 : Form

    {

        // Vector para almacenar los puntos que formarán la figura  

        private Point[] puntos;



        // Matriz de ejemplo (podría representar una transformación)  

        private int[,] matriz;



        // Tipo de figura seleccionada 

        private string tipoFigura;



        // Tamaño para las figuras 

        private int tamanoFigura = 100;



        public Form1()

        {

            InitializeComponent();

            InicializarComponentesPersonalizados();

        }



        private void InicializarComponentesPersonalizados()

        {

            // Configuración básica del formulario  

            this.Text = "Aplicación Gráfica en .NET";

            this.Size = new Size(800, 600);



            // TextBox para ingresar el tamaño de la figura 

            TextBox txtTamanoFigura = new TextBox();

            txtTamanoFigura.Location = new Point(20, 20);

            txtTamanoFigura.Width = 100;

            txtTamanoFigura.Name = "txtTamanoFigura";

            txtTamanoFigura.Text = "100";

            // Validación: solo se permiten dígitos  

            txtTamanoFigura.KeyPress += TxtNumeroPuntos_KeyPress;

            this.Controls.Add(txtTamanoFigura);



            Label lblTamano = new Label();

            lblTamano.Text = "Tamaño:";

            lblTamano.Location = new Point(20, 3);

            lblTamano.AutoSize = true;

            this.Controls.Add(lblTamano);



            // ComboBox para seleccionar el tipo de figura  

            ComboBox cmbFigura = new ComboBox();

            cmbFigura.Location = new Point(140, 20);

            cmbFigura.Width = 150;

            cmbFigura.Name = "cmbFigura";

            cmbFigura.Items.Add("Triángulo");

            cmbFigura.Items.Add("Cuadrado");

            cmbFigura.Items.Add("Rectángulo");

            cmbFigura.Items.Add("Círculo");

            // Seleccionar el primer elemento por defecto 

            cmbFigura.SelectedIndex = 0;

            // Aquí se podría agregar validación adicional o acciones al cambiar la selección  

            cmbFigura.SelectedIndexChanged += CmbFigura_SelectedIndexChanged;

            this.Controls.Add(cmbFigura);



            Label lblFigura = new Label();

            lblFigura.Text = "Figura:";

            lblFigura.Location = new Point(140, 3);

            lblFigura.AutoSize = true;

            this.Controls.Add(lblFigura);



            // Botón para generar y dibujar la figura  

            Button btnDibujar = new Button();

            btnDibujar.Location = new Point(310, 20);

            btnDibujar.Text = "Dibujar";

            btnDibujar.Click += (sender, e) =>

            {

                // Validación: se debe ingresar un valor y seleccionar una figura  

                if (string.IsNullOrEmpty(txtTamanoFigura.Text))

                {

                    MessageBox.Show("Ingrese el tamaño de la figura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }



                if (cmbFigura.SelectedIndex == -1)

                {

                    MessageBox.Show("Seleccione una figura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }



                // Conversión y validación del número ingresado  

                if (!int.TryParse(txtTamanoFigura.Text, out tamanoFigura))

                {

                    MessageBox.Show("El tamaño de la figura debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }



                if (tamanoFigura < 10)

                {

                    MessageBox.Show("El tamaño mínimo debe ser 10.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;

                }



                // Guardar el tipo de figura seleccionada 

                tipoFigura = cmbFigura.SelectedItem.ToString();



                // Creación de puntos según la figura seleccionada 

                GenerarPuntosFigura();



                // Creación de una matriz de 3x3 (por ejemplo, una matriz identidad)  

                matriz = new int[3, 3]

                {

                    {1, 0, 0},

                    {0, 1, 0},

                    {0, 0, 1}

                };



                // Forzar el repintado del formulario para mostrar la figura  

                this.Invalidate();

            };

            this.Controls.Add(btnDibujar);

        }



        // Método para generar los puntos según el tipo de figura 

        private void GenerarPuntosFigura()

        {

            // Centro de la pantalla para ubicar la figura 

            int centroX = this.ClientSize.Width / 2;

            int centroY = this.ClientSize.Height / 2;



            switch (tipoFigura)

            {

                case "Triángulo":

                    puntos = new Point[3];

                    // Triángulo equilátero 

                    puntos[0] = new Point(centroX, centroY - tamanoFigura);

                    puntos[1] = new Point(centroX - (int)(tamanoFigura * 0.866), centroY + (tamanoFigura / 2));

                    puntos[2] = new Point(centroX + (int)(tamanoFigura * 0.866), centroY + (tamanoFigura / 2));

                    break;



                case "Cuadrado":

                    puntos = new Point[4];

                    // Cuadrado centrado 

                    int mitad = tamanoFigura / 2;

                    puntos[0] = new Point(centroX - mitad, centroY - mitad);

                    puntos[1] = new Point(centroX + mitad, centroY - mitad);

                    puntos[2] = new Point(centroX + mitad, centroY + mitad);

                    puntos[3] = new Point(centroX - mitad, centroY + mitad);

                    break;



                case "Rectángulo":

                    puntos = new Point[4];

                    // Rectángulo centrado (ancho = 2*alto) 

                    int mitadAlto = tamanoFigura / 2;

                    int mitadAncho = tamanoFigura;

                    puntos[0] = new Point(centroX - mitadAncho, centroY - mitadAlto);

                    puntos[1] = new Point(centroX + mitadAncho, centroY - mitadAlto);

                    puntos[2] = new Point(centroX + mitadAncho, centroY + mitadAlto);

                    puntos[3] = new Point(centroX - mitadAncho, centroY + mitadAlto);

                    break;



                case "Círculo":

                    // El círculo se dibuja de manera diferente, no necesitamos puntos 

                    // pero mantenemos el array para compatibilidad 

                    puntos = new Point[1];

                    puntos[0] = new Point(centroX, centroY);

                    break;

            }

        }



        // Validación en el TextBox: permite solo dígitos y teclas de control  

        private void TxtNumeroPuntos_KeyPress(object sender, KeyPressEventArgs e)

        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))

                e.Handled = true;

        }



        private void CmbFigura_SelectedIndexChanged(object sender, EventArgs e)

        {

            // Aquí se puede implementar lógica adicional si se requiere cambiar  

            // el comportamiento según la figura seleccionada.  

        }



        // Método para dibujar la figura en el formulario  

        protected override void OnPaint(PaintEventArgs e)

        {

            base.OnPaint(e);

            if (puntos != null && puntos.Length > 0)

            {

                // Centro de la pantalla 

                int centroX = this.ClientSize.Width / 2;

                int centroY = this.ClientSize.Height / 2;



                // Usar diferentes colores para el borde y el relleno 

                Pen bordeAzul = new Pen(Color.Blue, 2);

                SolidBrush rellenoAzulClaro = new SolidBrush(Color.FromArgb(100, 0, 0, 255));



                // Dibujar según el tipo de figura 

                switch (tipoFigura)

                {

                    case "Triángulo":

                    case "Cuadrado":

                    case "Rectángulo":

                        // Dibujar el polígono con relleno 

                        e.Graphics.FillPolygon(rellenoAzulClaro, puntos);

                        // Dibujar el borde del polígono 

                        e.Graphics.DrawPolygon(bordeAzul, puntos);

                        break;



                    case "Círculo":

                        // Dibujar círculo con relleno 

                        e.Graphics.FillEllipse(rellenoAzulClaro,

                            centroX - tamanoFigura / 2,

                            centroY - tamanoFigura / 2,

                            tamanoFigura,

                            tamanoFigura);

                        // Dibujar el borde del círculo 

                        e.Graphics.DrawEllipse(bordeAzul,

                            centroX - tamanoFigura / 2,

                            centroY - tamanoFigura / 2,

                            tamanoFigura,

                            tamanoFigura);

                        break;

                }



                // Dibujar los puntos de referencia para polígonos 

                if (tipoFigura != "Círculo")

                {

                    foreach (var punto in puntos)

                    {

                        e.Graphics.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);

                    }

                }

            }

        }

    }

}