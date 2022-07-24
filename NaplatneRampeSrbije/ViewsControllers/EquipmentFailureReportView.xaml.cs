using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
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
    /// Interaction logic for PrijavaKvaraView.xaml
    /// </summary>
    public partial class EquipmentFailureReportView : Window
    {
        private readonly IEquipmentFailureService _equipmentFailureService;
        private readonly ITollBoothRepo _tollBoothRepo;

        public EquipmentFailureReportView(IEquipmentFailureService equipmentFailureService, ITollBoothRepo tollBoothRepo)
        {
            InitializeComponent();
            _equipmentFailureService = equipmentFailureService;
            _tollBoothRepo = tollBoothRepo;
            FillTollBoothComboBox();
            FillEquipmentComboBox();
            FillFailureTypeComboBox();
        }

        private void FillTollBoothComboBox()
        {
            tollBoothComboBox.ItemsSource = _tollBoothRepo.GetAll();
        }

        private void FillEquipmentComboBox()
        {
            equipmentComboBox.Items.Add(Equipment.Camera);
            equipmentComboBox.Items.Add(Equipment.Headlights);
            equipmentComboBox.Items.Add(Equipment.Ramp);
            equipmentComboBox.SelectedItem = Equipment.Camera;
        }

        private void FillFailureTypeComboBox()
        {
            failureTypeComboBox.Items.Add(FailureType.Electronic);
            failureTypeComboBox.Items.Add(FailureType.Physical);
            failureTypeComboBox.SelectedItem = FailureType.Electronic;
        }

        private void sendEquipmentFailureButton_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)equipmentComboBox.SelectedItem;
            string description = equipmentFailureDescriptionTextBox.Text;
            FailureType failureType = (FailureType)failureTypeComboBox.SelectedItem;
            TollBooth tollBooth = (TollBooth)tollBoothComboBox.SelectedItem;
            int tollBoothID = tollBooth.ID;

            _equipmentFailureService.CreateEquipmentFailure(equipment, description, failureType, tollBoothID);
            _ = MessageBox.Show("Kvar uspešno prijavljen", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            TollStationHeadView tollStationHeadView = new TollStationHeadView(
                new TollStationHeadService(
                    new TollBoothRepo(),
                    new TollStationRepo()));
            Close();
            tollStationHeadView.Show();
        }
    }
}
