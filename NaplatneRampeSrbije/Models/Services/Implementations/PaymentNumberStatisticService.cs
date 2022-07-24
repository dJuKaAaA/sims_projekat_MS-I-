using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class PaymentNumberStatisticService : IPaymentNumberStatisticService
    {
        private readonly IBillRepo _billRepo;

        public PaymentNumberStatisticService(IBillRepo billRepo)
        {
            _billRepo = billRepo;
        }

        public int GetPaymentNumber(string chosenDateParsed)
        {
            DateValidationCheck(chosenDateParsed);
            DateTime chosenDate = Convert.ToDateTime(chosenDateParsed).Date;

            List<Bill> bills = _billRepo.GetAll();

            int paymentCount = 0;
            foreach (Bill bill in bills)
            {
                DateTime billGenerationDate = bill.ExitDate.Date;
                if (billGenerationDate == chosenDate)
                {
                    paymentCount++;
                }
            }

            return paymentCount;
        }

        public double GetEarnings(string chosenDateParsed, Currency currency)
        {
            DateValidationCheck(chosenDateParsed);
            DateTime chosenDate = Convert.ToDateTime(chosenDateParsed).Date;

            List<Bill> bills = _billRepo.GetAll();

            double sum = 0;
            foreach (Bill bill in bills)
            {
                DateTime billGenerationDate = bill.ExitDate.Date;
                if (billGenerationDate == chosenDate)
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

        private void DateValidationCheck(string datum)
        {
            if (!DateTime.TryParse(datum, out DateTime _))
            {
                throw new Exception("Los unos datuma!");
            }
        }
    }
}