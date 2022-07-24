using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class PricelistItem
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public VehicleType VehicleType { get; set; }
        public Share Share { get; set; }
        public Pricelist Pricelist { get; set; }

        public PricelistItem()
        {

        }

        public PricelistItem(int id, double price, VehicleType vehicleType, Share share, Pricelist pricelist)
        {
            ID = id;
            Price = price;
            VehicleType = vehicleType;
            Share = share;
            Pricelist = pricelist;
        }

        public PricelistItem(OleDbDataReader reader, IShareRepo shareRepo, Pricelist pricelist)
        {
            ID = Convert.ToInt32(reader[0]);
            Price = Convert.ToDouble(reader[1]);
            VehicleType = (VehicleType)reader[2];
            Share = shareRepo.GetByID(Convert.ToInt32(reader[3]));
            Pricelist = pricelist;
        }
    }
}
