using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
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
    public partial class CurrencyStatisticView : Window
    {
        private readonly ICurrencyStatisticService _currencyStatisticService;
        private readonly ITollStationRepo _tollStationRepo;

        public CurrencyStatisticView(ICurrencyStatisticService currencyStatisticService, ITollStationRepo tollStationRepo)
        {
            InitializeComponent();
            _currencyStatisticService = currencyStatisticService;
            _tollStationRepo = tollStationRepo;
            FillTollStationComboBox();
            FillCurrencyComboBox();
        }

        private void FillTollStationComboBox()
        {
            List<TollStation> allTollStations = _tollStationRepo.GetAll();
            tollStationComboBox.ItemsSource = allTollStations;
            tollStationComboBox.SelectedItem = allTollStations[0];
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
            TollStation tollStation = (TollStation)tollStationComboBox.SelectedItem;
            Currency currency = (Currency)currencyComboBox.SelectedItem;

            int paymentCount = _currencyStatisticService.GetPaymentCountForChosenCurrencyAndStation(tollStation.ID, currency);
            double sum = _currencyStatisticService.GetPaymentSumForChosenCurrencyAndStation(tollStation.ID, currency);

            paymentCountLabel.Content = "Broj plaćanja: " + paymentCount.ToString();
            totalSumLabel.Content = "Ukupna suma: " + sum.ToString();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerMainView managerMainView = new ManagerMainView();
            Close();
            managerMainView.Show();
        }
    }
}
