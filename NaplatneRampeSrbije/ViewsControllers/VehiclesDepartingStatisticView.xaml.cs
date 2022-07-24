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
    /// <summary>
    /// Interaction logic for StatistikaProlaskaVozilaView.xaml
    /// </summary>
    public partial class VehiclesDepartingStatisticView : Window
    {
        private readonly ITollBoothService _tollBoothService;
        private readonly ITollBoothRepo _tollBoothRepo;

        public VehiclesDepartingStatisticView(ITollBoothService tollBoothService, ITollBoothRepo tollBoothRepo)
        {
            InitializeComponent();
            _tollBoothService = tollBoothService;
            _tollBoothRepo = tollBoothRepo;
            FillTollBoothsComboBox();
        }

        private void FillTollBoothsComboBox()
        {
            List<TollBooth> allTollBooths = _tollBoothRepo.GetAll();
            tollBoothComboBox.ItemsSource = allTollBooths;
            tollBoothComboBox.SelectedItem = allTollBooths[0];
        }

        private void generateReportButton_Click(object sender, RoutedEventArgs e)
        {
            TollBooth tollBooth = (TollBooth)tollBoothComboBox.SelectedItem;
            VehicleType frequentVehicle = _tollBoothService.GetMostFrequentlyDepartingVehicle(tollBooth.ID);
            mostFrequentVehiclesTextBox.Text = frequentVehicle.ToString();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerMainView managerMainView = new ManagerMainView();
            Close();
            managerMainView.Show();
        }
    }
}
