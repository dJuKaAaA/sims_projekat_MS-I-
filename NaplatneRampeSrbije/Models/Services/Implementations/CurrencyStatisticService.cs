using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class CurrencyStatisticService : ICurrencyStatisticService
    {
        private readonly IBillRepo _billrepo;

        public CurrencyStatisticService(IBillRepo racunRepo)
        {
            _billrepo = racunRepo;
        }

        public int GetPaymentCountForChosenCurrencyAndStation(int tollStationID, Currency currency)
        {
            List<Bill> bills = _billrepo.GetAll();
            int paymentCount = 0;

            foreach (Bill bill in bills)
            {
                if (bill.TollBothExited.TollStation.ID == tollStationID && bill.Currency == currency)
                {
                    paymentCount += 1;
                }
            }

            return paymentCount;
        }

        public double GetPaymentSumForChosenCurrencyAndStation(int tollStationID, Currency currency)
        {
            List<Bill> bills = _billrepo.GetAll();
            double sum = 0;

            foreach (Bill bill in bills)
            {
                if (bill.TollBothExited.TollStation.ID == tollStationID && bill.Currency == currency)
                {
                    sum += bill.Price;
                }
            }

            if (currency == Currency.Euro)
            {
                return Math.Round(sum / 117, 2);
            }
            else if (currency == Currency.Dollar)
            {
                return Math.Round(sum / 114, 2);
            }

            return sum;
        }
    }
}
