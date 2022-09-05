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
    /// Interaction logic for ShareCreationView.xaml
    /// </summary>
    public partial class ShareCreationView : Window
    {
        private readonly ITollStationRepo _tollStationRepo;
        private readonly IShareRepo _shareRepo;
        public ShareCreationView(ITollStationRepo tollStationRepo, IShareRepo shareRepo)
        {
            _shareRepo = shareRepo;
            _tollStationRepo = tollStationRepo;
            InitializeComponent();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            List<TollStation>tollStations = _tollStationRepo.GetAll();
            foreach(TollStation tollStation in tollStations)
            {
                TollBooth1ComboBox.Items.Add(tollStation);
                TollBooth2ComboBox.Items.Add(tollStation);
            }
            TollBooth1ComboBox.SelectedIndex = 0;
            TollBooth2ComboBox.SelectedIndex = 1;
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            List<TollStation> tollStations = _tollStationRepo.GetAll();
            int tollStation1Index, tollStation2Index, maxSpeed, lenght;
            tollStation1Index = TollBooth1ComboBox.SelectedIndex;
            tollStation2Index = TollBooth2ComboBox.SelectedIndex;
            TollStation tollStation1 = tollStations[tollStation1Index];
            TollStation tollStation2 = tollStations[tollStation2Index];
            if(tollStation1.ID == tollStation2.ID)
            {
                MessageBox.Show("Pocetna i krajnja stanica ne smeju biti iste");
                return;
            }
            Share share = _shareRepo.GetByEnterExitTollStation(tollStation1.ID, tollStation2.ID);
            if(share != null)
            {
                MessageBox.Show("Ova deonica vec postoji");
                return;
            }
            share = _shareRepo.GetByEnterExitTollStation(tollStation2.ID, tollStation1.ID);
            if(share != null)
            {
                MessageBox.Show("Ova deonica vec postoji");
                return;
            }
            try
            {
                maxSpeed = int.Parse(MaxSpeedTextBox.Text);
                lenght = int.Parse(LenghtTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Slova nisu dozvoljena");
                return;
            }
            int id = _shareRepo.GenerateNewID();
            Share newShare = new Share(id, lenght, tollStation1, tollStation2, maxSpeed);
            _shareRepo.Save(newShare);
            MessageBox.Show("Deonica uspesno dodata");
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
