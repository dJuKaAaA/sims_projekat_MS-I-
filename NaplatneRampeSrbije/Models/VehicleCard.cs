using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class VehicleCard
    {
        public int ID;
        public VehicleType VehicleType { get; set; }
        public DateTime EnterDate { get; set; }
        public TollBooth TollBoth { get; set; }

        public VehicleCard()
        {

        }

        public VehicleCard(int id, VehicleType vehicleType, DateTime enterDate, TollBooth tollBoth)
        {
            ID = id;
            VehicleType = vehicleType;
            EnterDate = enterDate;
            TollBoth = tollBoth;
        }

        public VehicleCard(OleDbDataReader reader, ITollBoothRepo tollBoothRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            VehicleType = (VehicleType)reader[1];
            EnterDate = DateTime.Parse(reader[2].ToString());
            TollBoth = tollBoothRepo.GetByID(Convert.ToInt32(reader[3]));
        }
    }
}
