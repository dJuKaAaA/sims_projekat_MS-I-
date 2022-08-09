using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class TollStation
    {
        public int ID { get; set; }
        public Address Address { get; set; }

        public TollStation()
        {

        }

        public TollStation(int id, Address adresa)
        {
            ID = id;
            Address = adresa;
        }

        public TollStation(OleDbDataReader reader, IAddressRepo addressRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            Address = addressRepo.GetByID(Convert.ToInt32(reader[1]));
        }

        public override string ToString()
        {
            return $"{Address}";
        }

        public override bool Equals(object obj)
        {
            return obj is TollStation tollStation &&
                tollStation.ID == ID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}