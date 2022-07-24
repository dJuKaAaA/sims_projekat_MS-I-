using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.ViewsControllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NaplatneRampeSrbije
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LoginView loginView = new LoginView(
                new LoginService());
            loginView.Show();

            base.OnStartup(e);
        }
    }
}
