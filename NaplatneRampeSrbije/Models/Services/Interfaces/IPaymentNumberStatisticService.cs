using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface IPaymentNumberStatisticService
    {
        int GetPaymentNumber(string chosenDateParsed);
        double GetEarnings(string chosenDateParsed, Currency currency);
    }
}
