using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Implementations;
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
    public partial class TollStationFailuresView : Window
    {
        private readonly IEquipmentFailureRepo _equipmentFailureRepo;
        private readonly ITollBoothRepo _tollBoothRepo;

        public TollStationFailuresView(IEquipmentFailureRepo equipmentFailureRepo, ITollBoothRepo tollBoothRepo)
        {
            InitializeComponent();
            _equipmentFailureRepo = equipmentFailureRepo;
            _tollBoothRepo = tollBoothRepo;
            tollStationTextBlock.Text = "Naplatna stanica: " + Globals.signedEmployee.TollStation.ToString();
            FillTollBoothComboBox();
        }

        private void FillTollBoothComboBox()
        {
            List<TollBooth> tollBooths = _tollBoothRepo.GetAllByTollStationID(Globals.signedEmployee.TollStation.ID);
            tollBoothComboBox.ItemsSource = tollBooths;
            tollBoothComboBox.SelectedItem = tollBooths[0];
            SetFailureIndicators();
        }

        private void rampFailureIndicatorCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (rampFailureIndicatorCheckBox.IsChecked == false)
            {
                _ = MessageBox.Show("Rampa je u funckiji", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                rampFailureIndicatorCheckBox.IsChecked = true;
                return;
            }

            TollBooth tollBooth = (TollBooth)tollBoothComboBox.SelectedItem;
            if (MessageBox.Show("Rampa popravljena?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (EquipmentFailure equipmentFailure in _equipmentFailureRepo.GetAllNotFixed())
                {
                    if (equipmentFailure.TollBooth.ID == tollBooth.ID)
                    {
                        if (equipmentFailure.Equipment == Equipment.Ramp)
                        {
                            _equipmentFailureRepo.FixByID(equipmentFailure.ID);
                            rampFailureIndicatorCheckBox.IsChecked = true;
                            _ = MessageBox.Show("Rampa je popravljena uspesno", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    }
                }
            }
            else
            {
                rampFailureIndicatorCheckBox.IsChecked = false;
            }
        }

        private void lightsFailureIndicatorCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (lightsFailureIndicatorCheckBox.IsChecked == false)
            {
                _ = MessageBox.Show("Svetla su u funckiji", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                lightsFailureIndicatorCheckBox.IsChecked = true;
                return;
            }

            TollBooth tollBooth = (TollBooth)tollBoothComboBox.SelectedItem;
            if (MessageBox.Show("Svetla popravljena?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (EquipmentFailure equipmentFailure in _equipmentFailureRepo.GetAllNotFixed())
                {
                    if (equipmentFailure.TollBooth.ID == tollBooth.ID)
                    {
                        if (equipmentFailure.Equipment == Equipment.Headlights)
                        {
                            _equipmentFailureRepo.FixByID(equipmentFailure.ID);
                            lightsFailureIndicatorCheckBox.IsChecked = true;
                            _ = MessageBox.Show("Svetla su popravljena uspesno", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    }
                }
            }
            else
            {
                lightsFailureIndicatorCheckBox.IsChecked = false;
            }
        }

        private void cameraFailureIndicatorCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (cameraFailureIndicatorCheckBox.IsChecked == false)
            {
                _ = MessageBox.Show("Kamera je u funckiji", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                cameraFailureIndicatorCheckBox.IsChecked = true;
                return;
            }

            TollBooth tollBooth = (TollBooth)tollBoothComboBox.SelectedItem;
            if (MessageBox.Show("Kamera popravljena?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (EquipmentFailure equipmentFailure in _equipmentFailureRepo.GetAllNotFixed())
                {
                    if (equipmentFailure.TollBooth.ID == tollBooth.ID)
                    {
                        if (equipmentFailure.Equipment == Equipment.Camera)
                        {
                            _equipmentFailureRepo.FixByID(equipmentFailure.ID);
                            cameraFailureIndicatorCheckBox.IsChecked = true;
                            _ = MessageBox.Show("Kamera je popravljena uspesno", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    }
                }
            }
            else
            {
                cameraFailureIndicatorCheckBox.IsChecked = false;
            }
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

        private void tollBoothComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFailureIndicators();
        }

        private void SetFailureIndicators()
        {
            rampFailureIndicatorCheckBox.IsChecked = true;
            lightsFailureIndicatorCheckBox.IsChecked = true;
            cameraFailureIndicatorCheckBox.IsChecked = true;

            TollBooth tollBooth = (TollBooth)tollBoothComboBox.SelectedItem;
            foreach (EquipmentFailure equipmentFailure in _equipmentFailureRepo.GetAllNotFixed())
            {
                if (equipmentFailure.TollBooth.ID == tollBooth.ID)
                {
                    switch (equipmentFailure.Equipment)
                    {
                        case Equipment.Camera:
                            cameraFailureIndicatorCheckBox.IsChecked = false;
                            break;
                        case Equipment.Ramp:
                            rampFailureIndicatorCheckBox.IsChecked = false;
                            break;
                        case Equipment.Headlights:
                            lightsFailureIndicatorCheckBox.IsChecked = false;
                            break;
                    }
                }
            }
        }
    }
}
