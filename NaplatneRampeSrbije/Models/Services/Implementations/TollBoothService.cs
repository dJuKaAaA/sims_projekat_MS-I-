using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class TollBoothService : ITollBoothService
    {
        private readonly ITollBoothRepo _tollBoothRepo;
        private readonly IBillRepo _billRepo;

        public TollBoothService(ITollBoothRepo tollBoothRepo, IBillRepo billRepo)
        {
            _tollBoothRepo = tollBoothRepo;
            _billRepo = billRepo;
        }

        public bool SameTollStation(TollBooth tollBooth1, TollBooth tollBooth2)
        {
            return tollBooth1.TollStation.ID == tollBooth2.TollStation.ID;
        }
        public VehicleType GetMostFrequentlyDepartingVehicle(int tollBoothID)
        {
            VehicleType mostFrequentlyDepartingVehicle = VehicleType.None;
            Dictionary<VehicleType, int> vehicleDepartings = new Dictionary<VehicleType, int>
            {
                [mostFrequentlyDepartingVehicle] = 0
            };
            foreach (Bill bill in _billRepo.GetAll())
            {
                if (!vehicleDepartings.ContainsKey(bill.VehicleType))
                {
                    vehicleDepartings[bill.VehicleType] = 0;
                }

                if (tollBoothID == bill.TollBothExited.ID || tollBoothID == bill.TollBothEntered.ID)
                {
                    ++vehicleDepartings[bill.VehicleType];
                }
            }

            foreach (KeyValuePair<VehicleType, int> kvp in vehicleDepartings)
            {
                if (kvp.Value > vehicleDepartings[mostFrequentlyDepartingVehicle])
                {
                    mostFrequentlyDepartingVehicle = kvp.Key;
                }
            }

            return mostFrequentlyDepartingVehicle;
        }
    }
}
