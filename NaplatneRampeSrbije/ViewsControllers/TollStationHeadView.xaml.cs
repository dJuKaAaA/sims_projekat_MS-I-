using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.Models.Services.Interfaces;
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
    /// Interaction logic for SefNaplatneStaniceView.xaml
    /// </summary>
    public partial class TollStationHeadView : Window
    {
        private readonly ITollStationHeadService _tollStationHeadService;

        public TollStationHeadView(ITollStationHeadService tollStationHeadService)
        {
            _tollStationHeadService = tollStationHeadService;
            InitializeComponent();
        }

        private void failureReportButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentFailureReportView equipmentFailureReportView = new EquipmentFailureReportView(
                new EquipmentFailureService(
                    new EquipmentFailureRepo(),
                    new TollBoothRepo()),
                new TollBoothRepo());
            Close();
            equipmentFailureReportView.Show();
        }

        private void failureDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentFailureDisplayView equipmentFailureDisplayView = new EquipmentFailureDisplayView(
                new EquipmentFailureRepo());
            Close();
            equipmentFailureDisplayView.Show();
        }

        private void createReportButton_Click(object sender, RoutedEventArgs e)
        {
            _tollStationHeadService.GenerateReport();
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

        private void tollBoothStatusButton_Click(object sender, RoutedEventArgs e)
        {
            TollStationFailuresView tollStationFailuresView = new TollStationFailuresView(
                new EquipmentFailureRepo(),
                new TollBoothRepo());
            Close();
            tollStationFailuresView.Show();
        }

        private void currentPricelistButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPricelistView currentPricelistView = new CurrentPricelistView(
                new PricelistRepo(),
                new PricelistItemRepo());
            Close();
            currentPricelistView.Show();
        }
    }
}
