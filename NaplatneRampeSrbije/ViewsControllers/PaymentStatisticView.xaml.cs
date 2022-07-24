using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StatistikaBrojaNaplataZaOdabraniDan.xaml
    /// </summary>
    public partial class PaymentCountStatisticView : Window
    {
        private readonly IPaymentNumberStatisticService _paymentNumberStatisticService;

        public PaymentCountStatisticView(IPaymentNumberStatisticService paymentNumberStatisticService)
        {
            InitializeComponent();
            _paymentNumberStatisticService = paymentNumberStatisticService;
            FillCurrencyComboBox();
        }
        
        private void FillCurrencyComboBox()
        {
            currencyComboBox.Items.Add(Currency.Dinar);
            currencyComboBox.Items.Add(Currency.Euro);
            currencyComboBox.Items.Add(Currency.Dollar);
            currencyComboBox.SelectedItem = Currency.Dinar;
        }

        private void generateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string chosenDateParsed = chosenDatePicker.Text;
                Currency currency = (Currency)currencyComboBox.SelectedItem;
                int paymentCount = _paymentNumberStatisticService.GetPaymentNumber(chosenDateParsed);
                double earnings = _paymentNumberStatisticService.GetEarnings(chosenDateParsed, currency);
                paymentSumLabel.Content = paymentCount.ToString();
                earningsSumLabel.Content = earnings.ToString();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerMainView managerMainView = new ManagerMainView();
            Close();
            managerMainView.Show();
        }
    }
}