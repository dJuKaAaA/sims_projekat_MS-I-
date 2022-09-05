using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using NaplatneRampeSrbije.ViewsControllers;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
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
using System.Windows.Navigation;

namespace NaplatneRampeSrbije.ViewsControllers
{
    public partial class LoginView : Window
    {
        private readonly ILoginService _loginService;

        public LoginView(ILoginService loginService)
        {
            InitializeComponent();
            _loginService = loginService;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_loginService.TryLogin(usernameTextBox.Text, passwordBox.Password))
            {
                if (Globals.signedEmployee.WorkPlace == WorkPlace.Admininstrator)
                {
                    AdministratorMainView administratorMainView = new AdministratorMainView();
                    Close();
                    administratorMainView.Show();
                }
                else if (Globals.signedEmployee.WorkPlace == WorkPlace.Manager)
                {
                    ManagerMainView managerMainView = new ManagerMainView();
                    Close();
                    managerMainView.Show();
                }
                else if(Globals.signedEmployee.WorkPlace == WorkPlace.TollStationHead)
                {
                    TollStationHeadView tollStationHeadView = new TollStationHeadView(
                        new TollStationHeadService(
                            new TollBoothRepo(),
                            new TollStationRepo()));
                    Close();
                    tollStationHeadView.Show();
                }
                else if (Globals.signedEmployee.WorkPlace == WorkPlace.BillingOfficer)
                {
                    PhysicalPaymentView physicalPaymentView = new PhysicalPaymentView(
                        new TollBoothService(
                            new TollBoothRepo(),
                            new BillRepo()),
                        new TollBoothRepo(),
                        new EquipmentFailureRepo(),
                        false);
                    Close();
                    physicalPaymentView.Show();
                }
            }
            else
            {
                _ = MessageBox.Show("Radnik nije nađen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}