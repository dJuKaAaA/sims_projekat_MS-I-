using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for PregledKvarovaView.xaml
    /// </summary>
    public partial class EquipmentFailureDisplayView : Window
    {
        private readonly IEquipmentFailureRepo _equipmentFailureRepo;

        public EquipmentFailureDisplayView(IEquipmentFailureRepo equipmentFailureRepo)
        {
            InitializeComponent();
            _equipmentFailureRepo = equipmentFailureRepo;
            FillEquipmentFailureDataGrid();
        }

        public void FillEquipmentFailureDataGrid()
        {
            equipmentFailureDataGrid.Items.Clear();
            foreach (EquipmentFailure failure in _equipmentFailureRepo.GetAllNotFixed())
            {
                equipmentFailureDataGrid.Items.Add(failure);
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
    }
}
