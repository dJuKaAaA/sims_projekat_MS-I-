using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.DTOs;
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
    public partial class CurrentPricelistView : Window
    {
        private readonly IPricelistRepo _pricelistRepo;
        private readonly IPricelistItemRepo _pricelistItemRepo;

        public CurrentPricelistView(IPricelistRepo pricelistRepo, IPricelistItemRepo pricelistItemRepo)
        {
            InitializeComponent();
            _pricelistRepo = pricelistRepo;
            _pricelistItemRepo = pricelistItemRepo;
            FillCurrentPricelistDataGrid();
        }

        public void FillCurrentPricelistDataGrid()
        {
            Pricelist currentPricelist = _pricelistRepo.GetCurrent();
            List<PricelistItem> currentPricelistItems = _pricelistItemRepo.GetByPricelist(currentPricelist);
            foreach (PricelistItem pricelistItem in currentPricelistItems)
            {
                currentPricelistDataGrid.Items.Add(new PricelistItemDTO(pricelistItem));
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Globals.signedEmployee.WorkPlace)
            {
                case WorkPlace.BillingOfficer:
                    PhysicalPaymentView physicalPaymentView = new PhysicalPaymentView(
                        new TollBoothService(
                            new TollBoothRepo(),
                            new BillRepo()),
                        new TollBoothRepo(),
                        new EquipmentFailureRepo(),
                        false);
                    Close();
                    physicalPaymentView.Show();
                    break;
                case WorkPlace.TollStationHead:
                    TollStationHeadView tollStationHeadView = new TollStationHeadView(
                            new TollStationHeadService(
                                new TollBoothRepo(),
                                new TollStationRepo()));
                    Close();
                    tollStationHeadView.Show();
                    break;
                case WorkPlace.Manager:
                    ManagerMainView managerMainView = new ManagerMainView();
                    Close();
                    managerMainView.Show();
                    break;
                case WorkPlace.Admininstrator:
                    AdministratorMainView administratorMainView = new AdministratorMainView();
                    Close();
                    administratorMainView.Show();
                    break;
            }
        }
    }
}
