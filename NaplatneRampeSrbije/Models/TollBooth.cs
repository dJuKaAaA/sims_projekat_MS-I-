using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class TollBooth
    {
        public int ID { get; set; }
        public TollStation TollStation { get; set; }
        public bool ElectronicPayment { get; set; }

        public TollBooth()
        {
        }

        public TollBooth(int id, TollStation naplatnaStanica, bool electronicPayment)
        {
            ID = id;
            TollStation = naplatnaStanica;
            ElectronicPayment = electronicPayment;
        }

        public TollBooth(OleDbDataReader reader, ITollStationRepo tollStationRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            TollStation = tollStationRepo.GetByID(Convert.ToInt32(reader[1]));
            ElectronicPayment = Convert.ToBoolean(reader[2]);
        }

        public override string ToString()
        {
            return $"ID naplatnog mesta: {ID}; Adresa: {TollStation.Address}";
        }

        public override bool Equals(object obj)
        {
            return obj is TollBooth tollBooth &&
                tollBooth.ID == ID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}