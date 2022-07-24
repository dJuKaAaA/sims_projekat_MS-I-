using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class ReadingLicencePlate
    {
        public int ID { get; set; }
        public string LicencePlate { get; set; }
        public VehicleCard VehicleCard { get; set; }

        public ReadingLicencePlate()
        {

        }

        public ReadingLicencePlate(int id, string licencePlate, VehicleCard vehicleCard)
        {
            ID = id;
            LicencePlate = licencePlate;
            VehicleCard = vehicleCard;
        }

        public ReadingLicencePlate(OleDbDataReader reader, IVehicleCardRepo vehicleCardRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            LicencePlate = reader[1].ToString();
            VehicleCard = vehicleCardRepo.GetByID(Convert.ToInt32(reader[2]));
        }
    }
}
