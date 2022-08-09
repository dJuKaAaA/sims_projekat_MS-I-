using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.ViewsControllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NaplatneRampeSrbije.ViewsControllers
{
    public partial class AdministratorMainView : Window
    {
        public AdministratorMainView()
        {
            InitializeComponent();
        }

        private void tollBoothCRUDButton_Click(object sender, RoutedEventArgs e)
        {
            TollBoothCRUDView tollBoothCRUDView = new TollBoothCRUDView(
                new TollBoothCRUDService(
                    new TollBoothRepo(),
                    new TollStationRepo()),
                new TollStationRepo(),
                new TollBoothRepo());
            Close();
            tollBoothCRUDView.Show();
        }

        private void employeeCRUDButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeCRUDView employeeCRUDView = new EmployeeCRUDView(
                new EmployeeCRUDService(
                    new EmployeeRepo(),
                    new AddressRepo(),
                    new TollBoothRepo(),
                    new TollStationRepo()),
                new AddressRepo(),
                new TollBoothRepo(),
                new TollStationRepo());
            Close();
            employeeCRUDView.Show();
        }

        private void tollStationCRUDButton_Click(object sender, RoutedEventArgs e)
        {
            TollStationCRUDView tollStationCRUDView = new TollStationCRUDView(
                new TollStationCRUDService(
                    new TollStationRepo(),
                    new AddressRepo(),
                    new CityRepo()));
            Close();
            tollStationCRUDView.Show();
        }

        private void Logout()
        {
            Globals.signedEmployee = null;
            LoginView loginWindow = new LoginView(
                new LoginService());
            Close();
            loginWindow.Show();
        }

        private void currentPricelistButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPricelistView currentPricelistView = new CurrentPricelistView(
                new PricelistRepo(),
                new PricelistItemRepo());
            Close();
            currentPricelistView.Show();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }
    }
}