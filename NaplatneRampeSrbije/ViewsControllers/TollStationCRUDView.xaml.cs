using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.DTOs;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Windows;

namespace NaplatneRampeSrbije.ViewsControllers
{
    /// <summary>
    /// Interaction logic for CRUDNaplatnaStanicaView.xaml
    /// </summary>
    public partial class TollStationCRUDView : Window
    {
        private readonly ITollStationCRUDService _tollStationCRUDService;

        public TollStationCRUDView(ITollStationCRUDService tollStationCRUDService)
        {
            InitializeComponent();
            _tollStationCRUDService = tollStationCRUDService;
            FillTollStationDataGrid();
        }

        private void FillTollStationDataGrid()
        {
            tollStationDataGrid.Items.Clear();
            foreach (TollStationDTO tollStationDTO in _tollStationCRUDService.GetTollStationsForDisplay())
            {
                tollStationDataGrid.Items.Add(tollStationDTO);
            }
        }

        private void createTollStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string street = streetTextBox.Text;
                string number = numberTextBox.Text;
                string zipCode = zipCodeTextBox.Text;

                if (MessageBox.Show("Kreiraj?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _tollStationCRUDService.CreateTollStation(street, number, zipCode);
                    _ = MessageBox.Show("Uspešno ste kreirali naplatnu stanicu!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillTollStationDataGrid();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateTollStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TollStationDTO tollStationDTO = tollStationDataGrid.SelectedItem as TollStationDTO;
                if (tollStationDTO == null)
                {
                    _ = MessageBox.Show("Niste izabrali naplatnu stanicu", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int id = Convert.ToInt32(tollStationDTO.ID);
                string street = streetTextBox.Text;
                string number = numberTextBox.Text;
                string zipCode = zipCodeTextBox.Text;

                if (MessageBox.Show("Izmeni?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _tollStationCRUDService.UpdateTollStation(id, street, number, zipCode);
                    _ = MessageBox.Show("Uspešno ste izmenili naplatnu stanicu!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillTollStationDataGrid();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteTollStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TollStationDTO tollStationDTO = tollStationDataGrid.SelectedItem as TollStationDTO;
                if (tollStationDTO == null)
                {
                    _ = MessageBox.Show("Niste izabrali naplatnu stanicu", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show("Izbriši?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _tollStationCRUDService.DeleteTollStation(Convert.ToInt32(tollStationDTO.ID));
                    _ = MessageBox.Show("Uspešno ste obrisali naplatnu stanicu!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillTollStationDataGrid();
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