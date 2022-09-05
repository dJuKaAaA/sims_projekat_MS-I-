using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface ITollBoothService
    {
        bool SameTollStation(TollBooth tollBooth1, TollBooth tollBooth2);
        VehicleType GetMostFrequentlyDepartingVehicle(int tollBoothID);
    }
}
