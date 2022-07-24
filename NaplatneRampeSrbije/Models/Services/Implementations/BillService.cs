using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class BillService : IBillService
    {
        private readonly IBillRepo _billRepo;
        private readonly IPricelistRepo _pricelistRepo;
        private readonly IShareRepo _shareRepo;
        private readonly ITollBoothRepo _tollBoothRepo;

        public BillService(IBillRepo billRepo, IPricelistRepo pricelistRepo, IShareRepo shareRepo, ITollBoothRepo tollBoothRepo)
        {
            _billRepo = billRepo;
            _pricelistRepo = pricelistRepo;
            _shareRepo = shareRepo;
            _tollBoothRepo = tollBoothRepo;
        }

        public void SaveBill(int billID, VehicleType vehicleType, double price, Currency currency, DateTime ExitDate, int tollBoothExitedID, int tollBoothEnteredID)
        {
            TollBooth tollBoothEntered = _tollBoothRepo.GetByID(tollBoothEnteredID);
            TollBooth tollBoothExited = _tollBoothRepo.GetByID(tollBoothExitedID);

            Bill bill = new Bill(billID, vehicleType, price, currency, ExitDate, tollBoothExited, tollBoothEntered);

            _billRepo.Save(bill);
        }

        public double GetPriceForShareAndVehicle(TollBooth tollBoothEntered, VehicleType vehicleType, Currency currency)
        {
            Pricelist pricelist = _pricelistRepo.GetCurrent();

            TollBooth tollBoothExited = Globals.ulogovaniRadnik.TollBooth;

            Share share = _shareRepo.GetByEnterExitTollStation(tollBoothEntered.TollStation.ID, tollBoothExited.TollStation.ID);

            double price = 0;

            foreach (PricelistItem item in pricelist.Items)
            {
                if (item.Share.ID == share.ID && item.VehicleType == vehicleType)
                {
                    price = item.Price;
                    break;
                }
            }

            if (currency == Currency.Euro)
            {
                return Math.Round(price / 117, 2);
            }
            else if (currency == Currency.Dollar)
            {
                return Math.Round(price / 114, 2);
            }

            return price;
        }
    }
}
