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
        private readonly Share _share;
        private readonly VehicleType _vehicleType;
        private readonly Currency _currency;
        private readonly TollBooth _enteredTollBooth;
        private readonly DateTime _entryDate;
        private readonly DateTime _exitDate;
        private readonly double _forPayment;

        public BillGenerationView(
            IBillService billService,
            Share share,
            IBillRepo billRepo,
            VehicleType vehicleType,
            Currency currency,
            TollBooth tollBooth,
            DateTime entryDate,
            DateTime exitDate)
        {
            InitializeComponent();
            _billService = billService;
            _share = share;
            _vehicleType = vehicleType;
            _currency = currency;
            _enteredTollBooth = tollBooth;
            _entryDate = entryDate;
            _exitDate = exitDate;

            tollBoothEnteredTextBox.Items.Add(_enteredTollBooth);
            tollBoothEnteredTextBox.SelectedItem = _enteredTollBooth;
            vehicleTypeComboBox.Items.Add(_vehicleType);
            vehicleTypeComboBox.SelectedItem = _vehicleType;
            currencyComboBox.Items.Add(_currency);
            currencyComboBox.SelectedItem = _currency;
            exitDateTextBox.Text = _exitDate.ToString(Globals.dateTimeFormat);
            billIDTextBox.Text = billRepo.GenerateNewID().ToString();

            _forPayment = _billService.GetPriceForShareAndVehicle(_enteredTollBooth, _vehicleType, Currency.Dinar);
            double forPaymentDisplay = _billService.GetPriceForShareAndVehicle(_enteredTollBooth, _vehicleType, _currency);

            paymentSumTextBox.Text = forPaymentDisplay.ToString();
            averageSpeedTextBox.Text = GetAverageSpeed().ToString();
            speedLimitTextBox.Text = _share.SpeedLimit.ToString();
        }

        private int GetAverageSpeed()
        {
            double roadLength = _share.Length;
            TimeSpan travelTimePassed = _exitDate.Subtract(_entryDate);
            double travelTime = travelTimePassed.TotalHours;
            double averageMovingSpeed = roadLength / travelTime;
            return (int)averageMovingSpeed;
        }

        private void payButton_Click(object sender, RoutedEventArgs e)
        {
            int billID = Convert.ToInt32(billIDTextBox.Text);
            VehicleType vehicleType = (VehicleType)vehicleTypeComboBox.SelectedItem;
            double paymentPrice = Convert.ToDouble(paymentSumTextBox.Text);
            Currency currency = (Currency)currencyComboBox.SelectedItem;
            DateTime exitDate = DateTime.Parse(exitDateTextBox.Text);
            int exitedTollBoothID = Globals.signedEmployee.TollBooth.ID;
            int enteredTollBoothID = _enteredTollBooth.ID;

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

                _billService.SaveBill(billID, vehicleType, _forPayment, currency, exitDate, exitedTollBoothID, enteredTollBoothID, _entryDate, GetAverageSpeed());
                _ = MessageBox.Show($"Uspešno plaćanje, Kusur ({currency}): {billChange}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                if (GetAverageSpeed() > _share.SpeedLimit)
                {
                    _ = MessageBox.Show("Ograničenje brzine je prekoračeno! Slediće novčana kazna ili sudski postupak!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                PhysicalPaymentView physicalPaymentView = new PhysicalPaymentView(
                    new TollBoothService(
                        new TollBoothRepo(),
                        new BillRepo()),
                    new TollBoothRepo(),
                    new EquipmentFailureRepo());
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
                    new TollBoothRepo(),
                    new EquipmentFailureRepo());
            Close();
            physicalPaymentView.Show();
        }
    }
}
