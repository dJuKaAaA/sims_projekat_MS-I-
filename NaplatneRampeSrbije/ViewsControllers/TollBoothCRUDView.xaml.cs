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

namespace NaplatneRampeSrbije.ViewsControllers
{
    public partial class TollBoothCRUDView : Window
    {
        private readonly ITollBoothCRUDService _tollBoothCRUDService;
        private readonly ITollStationRepo _tollStationRepo;
        private readonly ITollBoothRepo _tollBoothRepo;

        public TollBoothCRUDView(
            ITollBoothCRUDService tollBoothCRUDService,
            ITollStationRepo tollStationRepo,
            ITollBoothRepo tollBoothRepo)
        {
            InitializeComponent();
            _tollBoothCRUDService = tollBoothCRUDService;
            _tollStationRepo = tollStationRepo;
            _tollBoothRepo = tollBoothRepo;
            FillTollBoothDataGrid();
            FillTollStationComboBox();
        }

        private void FillTollStationComboBox()
        {
            List<TollStation> allTollStations = _tollStationRepo.GetAll();
            tollStationComboBox.ItemsSource = allTollStations;
            tollStationComboBox.SelectedItem = allTollStations[0];
        }

        private void FillTollBoothDataGrid()
        {
            tollBoothDataGrid.Items.Clear();
            foreach (TollBooth tollBooth in _tollBoothRepo.GetAll())
            {
                tollBoothDataGrid.Items.Add(tollBooth);
            }
        }

        private void createTollBoothButton_Click(object sender, RoutedEventArgs e)
        {
            TollStation tollStation = (TollStation)tollStationComboBox.SelectedItem;
            bool electronicPayment = (bool)electronicPaymentCheckBox.IsChecked;

            try
            {
                if (MessageBox.Show("Kreiraj?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _tollBoothCRUDService.SaveTollBooth(tollStation.ID, electronicPayment);
                    _ = MessageBox.Show("Uspešno dodato naplatno mesto", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillTollBoothDataGrid();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateTollBoothButton_Click(object sender, RoutedEventArgs e)
        {
            TollBooth tollBooth = (TollBooth)tollBoothDataGrid.SelectedItem;
            if (tollBooth == null)
            {
                _ = MessageBox.Show("Naplatno mesto nije izabrano", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TollStation tollStation = (TollStation)tollStationComboBox.SelectedItem;
            bool electronicPayment = (bool)electronicPaymentCheckBox.IsChecked;

            try
            {
                if (MessageBox.Show("Izmeni?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _tollBoothCRUDService.EditTollBooth(tollBooth.ID, tollStation.ID, electronicPayment);
                    _ = MessageBox.Show("Uspešno izmenjeno naplatno mesto.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillTollBoothDataGrid();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteTollBoothButton_Click(object sender, RoutedEventArgs e)
        {
            TollBooth tollBooth = (TollBooth)tollBoothDataGrid.SelectedItem;
            if (tollBooth == null)
            {
                _ = MessageBox.Show("Naplatno mesto nije izabrano", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (MessageBox.Show("Izbriši?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _tollBoothCRUDService.DeleteTollBooth(tollBooth.ID);
                    _ = MessageBox.Show("Uspešno izbrisano naplatno mesto.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillTollBoothDataGrid();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            AdministratorMainView administratorMainView = new AdministratorMainView();
            Close();
            administratorMainView.Show();
        }
    }
}
