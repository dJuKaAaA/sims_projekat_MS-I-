using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface ITollBoothService
    {
        bool SameTollStation(int tollBoothID1, int tollBoothID2);
        VehicleType GetMostFrequentlyDepartingVehicle(int tollBoothID);
    }
}
