using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Windows;

namespace NaplatneRampeSrbije.ViewsControllers
{
    public partial class BillGenerationView : Window
    {
        private readonly IBillService _billService;
        private readonly VehicleType _vehicleType;
        private readonly Currency _currency;
        private readonly TollBooth _tollBooth;
        private readonly DateTime _exitDate;
        private readonly double _forPayment;

        public BillGenerationView(
            IBillService billService,
            IBillRepo billRepo,
            VehicleType vehicleType,
            Currency currency,
            TollBooth tollBooth,
            DateTime exitDate)
        {
            InitializeComponent();
            _billService = billService;
            _vehicleType = vehicleType;
            _currency = currency;
            _tollBooth = tollBooth;
            _exitDate = exitDate;

            tollBoothEnteredTextBox.Items.Add(_tollBooth);
            tollBoothEnteredTextBox.SelectedItem = _tollBooth;
            vehicleTypeComboBox.Items.Add(_vehicleType);
            vehicleTypeComboBox.SelectedItem = _vehicleType;
            currencyComboBox.Items.Add(_currency);
            currencyComboBox.SelectedItem = _currency;
            exitDateTextBox.Text = _exitDate.ToString(Globals.dateTimeFormat);
            billIDTextBox.Text = billRepo.GenerateNewID().ToString();

            _forPayment = _billService.GetPriceForShareAndVehicle(_tollBooth, _vehicleType, Currency.Dinar);
            double forPaymentDisplay = _billService.GetPriceForShareAndVehicle(_tollBooth, _vehicleType, _currency);

            paymentSumTextBox.Text = forPaymentDisplay.ToString();
        }

        private void payButton_Click(object sender, RoutedEventArgs e)
        {
            int billID = Convert.ToInt32(billIDTextBox.Text);
            VehicleType vehicleType = (VehicleType)vehicleTypeComboBox.SelectedItem;
            double paymentPrice = Convert.ToDouble(paymentSumTextBox.Text);
            Currency currency = (Currency)currencyComboBox.SelectedItem;
            DateTime exitDate = DateTime.Parse(exitDateTextBox.Text);
            int exitedTollBoothID = Globals.signedEmployee.TollBooth.ID;
            int enteredTollBoothID = _tollBooth.ID;

            double paidSum;
            double billChange;
            try
            {
                string paidSumText = paidSumTextBox.Text.Replace('.', ',');
                paidSum = Convert.ToDouble(paidSumText);
                if (paidSum < paymentPrice)
                {
                    _ = MessageBox.Show("Uneta manja nego tražena suma", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                billChange = Math.Round(paidSum - paymentPrice, 2);
            }
            catch
            {
                _ = MessageBox.Show("Morate uneti ispravnu sumu", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                _billService.SaveBill(billID, vehicleType, _forPayment, currency, exitDate, exitedTollBoothID, enteredTollBoothID);
                _ = MessageBox.Show($"Uspešno plaćanje, Kusur ({currency}): {billChange}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                PhysicalPaymentView physicalPaymentView = new PhysicalPaymentView(
                    new TollBoothService(
                        new TollBoothRepo(),
                        new BillRepo()),
                    new TollBoothRepo());
                Close();
                physicalPaymentView.Show();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            PhysicalPaymentView physicalPaymentView = new PhysicalPaymentView(
                    new TollBoothService(
                        new TollBoothRepo(),
                        new BillRepo()),
                    new TollBoothRepo());
            Close();
            physicalPaymentView.Show();
        }
    }
}
