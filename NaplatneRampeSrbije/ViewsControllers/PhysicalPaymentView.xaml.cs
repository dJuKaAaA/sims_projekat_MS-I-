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
            entryDatePicker.SelectedDate = DateTime.Now;
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

        private void ValidateTime()
        {
            string hoursText = hoursTextBox.Text;
            string minutesText = minutesTextBox.Text;

            if (string.IsNullOrEmpty(hoursText)) { throw new Exception("Sat ulaska nije upisan"); }
            if (string.IsNullOrEmpty(minutesText)) { throw new Exception("Minut ulaska nije upisan"); }

            int hours;
            try { hours = Convert.ToInt32(hoursText); }
            catch { throw new Exception("Sat ulaska mora biti broj"); }

            int minutes;
            try { minutes = Convert.ToInt32(minutesText); }
            catch { throw new Exception("minut ulaska mora biti broj"); }

            if (hours >= 24 || hours < 0) { throw new Exception("Sat mora biti izmedju 0 i 23"); }
            if (minutes >= 60 || minutes < 0) { throw new Exception("Sat mora biti izmedju 0 i 59"); }

        }

        private void generateBillButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateTime();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (entryDatePicker.SelectedDate == null)
            {
                _ = MessageBox.Show("Datum ulaska nije izabran", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string entryDateParsed = entryDatePicker.Text + " " + hoursTextBox.Text + ":" + minutesTextBox.Text;
            DateTime entryDate = DateTime.Parse(entryDateParsed);

            if (entryDate.CompareTo(DateTime.Now) > 0)
            {
                _ = MessageBox.Show("Datum ulaska nije validan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TollBooth enteredTollBooth = (TollBooth)enteredTollBoothComboBox.SelectedItem;
            VehicleType vehicleType = (VehicleType)vehicleTypeComboBox.SelectedItem;
            Currency currency = (Currency)currencyComboBox.SelectedItem;

            DateTime exitDate = DateTime.Now;

            if (_tollBoothService.SameTollStation(enteredTollBooth.ID, Globals.signedEmployee.TollBooth.ID))
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
                new ShareRepo().GetByEnterExitTollStation(enteredTollBooth.TollStation.ID, Globals.signedEmployee.TollBooth.TollStation.ID),
                new BillRepo(),
                vehicleType,
                currency,
                enteredTollBooth,
                entryDate,
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
