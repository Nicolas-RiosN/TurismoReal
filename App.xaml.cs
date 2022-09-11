using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Turismo.View;
namespace Turismo
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new Login();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
              {
                  if (loginView.IsVisible == false && loginView.IsLoaded)
                  {
                      var mainview = new MainWindow();
                      mainview.Show();
                      loginView.Close();
                  }
              };

        }
    }
}
