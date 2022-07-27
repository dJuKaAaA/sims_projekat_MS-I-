using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
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
    /// <summary>
    /// Interaction logic for MenadzerMainView.xaml
    /// </summary>
    public partial class ManagerMainView : Window
    {
        public ManagerMainView()
        {
            InitializeComponent();
        }

        private void currencyStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            CurrencyStatisticView currencyStatisticView = new CurrencyStatisticView(
                new CurrencyStatisticService(
                    new BillRepo()),
                new TollStationRepo());
            Close();
            currencyStatisticView.Show();
        }

        private void vehicleDepartingStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            VehiclesDepartingStatisticView vehicleDepartingStatisticView = new VehiclesDepartingStatisticView(
                new TollBoothService(
                    new TollBoothRepo(),
                    new BillRepo()),
                new TollBoothRepo());
            Close();
            vehicleDepartingStatisticView.Show();
        }

        private void paymentStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            PaymentCountStatisticView paymentStatisticView = new PaymentCountStatisticView(
                new PaymentNumberStatisticService(
                    new BillRepo()));
            Close();
            paymentStatisticView.Show();
        }

        private void Logout()
        {
            Globals.signedEmployee = null;
            LoginView loginWindow = new LoginView(
                new LoginService());
            Close();
            loginWindow.Show();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }
    }
}