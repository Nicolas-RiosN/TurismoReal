using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.OracleClient;


namespace Turismo.View
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        OracleConnection conexion = new OracleConnection("DATA SOURCE = orcl1 ; PASSWORD = 123 ; USER ID = TURISMO");

        public Login()
        {
            InitializeComponent();
        }

        //Suscribo el evento para que se arrastre de cualquier parte del formulario y hacerlo mas comodo
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        //Minimizar Venatana
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; 
        }

        //Cerrar Ventana
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {




            conexion.Open();
            OracleCommand comando = new OracleCommand("SELECT * FROM LOGIN_ADMIN WHERE USUARIO_LOGIN_ADMIN = :usuario AND PASS_LOGIN_ADMIN = :contra", conexion);
            comando.Parameters.AddWithValue(":usuario", txtUser.Text);
            comando.Parameters.AddWithValue(":contra", txtPassword.Password);

            OracleDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                MainWindow formulario = new MainWindow();
                conexion.Close();
                formulario.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se ha podido ingresar, revise el nombre de usuario o contraseña");
                conexion.Close();
            }
        }

    }
}
