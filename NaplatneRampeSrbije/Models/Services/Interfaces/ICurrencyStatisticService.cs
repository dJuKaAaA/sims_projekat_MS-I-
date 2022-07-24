using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface ICurrencyStatisticService
    {
        int GetPaymentCountForChosenCurrencyAndStation(int tollStationID, Currency currency);
        double GetPaymentSumForChosenCurrencyAndStation(int tollStationID, Currency currency);
    }
}
