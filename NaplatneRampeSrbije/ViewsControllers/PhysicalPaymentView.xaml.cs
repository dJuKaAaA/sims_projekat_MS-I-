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
using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.Models.Services.Interfaces;

namespace NaplatneRampeSrbije.ViewsControllers
{
    /// <summary>
    /// Interaction logic for RucnaNaplataPutarineView.xaml
    /// </summary>
    public partial class PhysicalPaymentView : Window
    {
        private readonly ITollBoothService _tollBoothService;
        private readonly ITollBoothRepo _tollBoothRepo;

        public PhysicalPaymentView(ITollBoothService tollBoothService, ITollBoothRepo tollBoothRepo)
        {
            InitializeComponent();
            _tollBoothService = tollBoothService;
            _tollBoothRepo = tollBoothRepo;
            FillTollBoothsComboBox();
            FillVehicleTypeComboBox();
            FillCurrencyComboBox();
        }

        private void FillTollBoothsComboBox()
        {
            List<TollBooth> allTollBooths = _tollBoothRepo.GetAll();
            enteredTollBoothComboBox.ItemsSource = allTollBooths;
            enteredTollBoothComboBox.SelectedItem = allTollBooths[0];
        }

        private void FillVehicleTypeComboBox()
        {
            vehicleTypeComboBox.Items.Add(VehicleType.Car);
            vehicleTypeComboBox.Items.Add(VehicleType.Truck);
            vehicleTypeComboBox.Items.Add(VehicleType.Bus);
            vehicleTypeComboBox.SelectedItem = VehicleType.Car;
        }

        private void FillCurrencyComboBox()
        {
            currencyComboBox.Items.Add(Currency.Dinar);
            currencyComboBox.Items.Add(Currency.Euro);
            currencyComboBox.Items.Add(Currency.Dollar);
            currencyComboBox.SelectedItem = Currency.Dinar;
        }

        private void generateBillButton_Click(object sender, RoutedEventArgs e)
        {
            TollBooth tollBooth = (TollBooth)enteredTollBoothComboBox.SelectedItem;
            VehicleType vehicleType = (VehicleType)vehicleTypeComboBox.SelectedItem;
            Currency currency = (Currency)currencyComboBox.SelectedItem;
            DateTime exitDate = DateTime.Now;

            if (_tollBoothService.SameTollStation(tollBooth.ID, Globals.signedEmployee.TollBooth.ID))
            {
                _ = MessageBox.Show("Nije validna kombinacija mesta ulaska i izlaska", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            BillGenerationView billGenerationView = new BillGenerationView(
                new BillService(
                    new BillRepo(),
                    new PricelistRepo(),
                    new ShareRepo(),
                    new TollBoothRepo()),
                new BillRepo(),
                vehicleType,
                currency,
                tollBooth,
                exitDate);
            Close();
            billGenerationView.Show();
        }

        private void rampControlButton_Click(object sender, RoutedEventArgs e)
        {
            enteredTollBoothComboBox.IsEnabled = !enteredTollBoothComboBox.IsEnabled;
            vehicleTypeComboBox.IsEnabled = !vehicleTypeComboBox.IsEnabled;
            currencyComboBox.IsEnabled = !currencyComboBox.IsEnabled;
            rampBlockedCheckBox.IsChecked = !rampBlockedCheckBox.IsChecked;
            generateBillButton.IsEnabled = !generateBillButton.IsEnabled;
            blockRampButton.IsEnabled = !blockRampButton.IsEnabled;
            unblockRampButton.IsEnabled = !unblockRampButton.IsEnabled;
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
