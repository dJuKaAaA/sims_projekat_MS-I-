using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Data.OleDb;

namespace NaplatneRampeSrbije.Models
{
    public class EquipmentFailure
    {
        public int ID { get; set; }
        public Equipment Equipment { get; set; }
        public string Description { get; set; }
        public FailureType FailureType { get; set; }
        public TollBooth TollBooth { get; set; }

        public EquipmentFailure()
        {

        }

        public EquipmentFailure(int id, Equipment equipment, string description, FailureType failureType, TollBooth tollBoth)
        {
            ID = id;
            Equipment = equipment;
            Description = description;
            FailureType = failureType;
            TollBooth = tollBoth;
        }

        public EquipmentFailure(OleDbDataReader reader, ITollBoothRepo tollBoothRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            Equipment = (Equipment)reader[1];
            Description = reader[2].ToString();
            TollBooth = tollBoothRepo.GetByID(Convert.ToInt32(reader[3]));
            FailureType = (FailureType)reader[4];
        }
    }
}
