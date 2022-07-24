using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface IBillService
    {
        void SaveBill(int billID, VehicleType vehicleType, double price, Currency currency, DateTime ExitDate, int tollBoothExitedID, int tollBoothEnteredID);
        double GetPriceForShareAndVehicle(TollBooth tollBoothEntered, VehicleType vehicleType, Currency currency);
    }
}
