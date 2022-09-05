using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;

namespace NaplatneRampeSrbije.Models
{
    public class Pricelist
    {
        public int ID;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PricelistItem> Items { get; set; }

        public Pricelist()
        {

        }

        public Pricelist(int id, DateTime startDate, DateTime datumKraja)
        {
            ID = id;
            StartDate = startDate;
            EndDate = datumKraja;
            Items = new List<PricelistItem>();
        }

        public Pricelist(OleDbDataReader reader, IPricelistItemRepo pricelistItemRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            StartDate = DateTime.ParseExact(reader[1].ToString(), "d.M.yyyy.", CultureInfo.InvariantCulture);
            EndDate = reader[2].ToString() == "null" ? new DateTime() : DateTime.Parse(reader[2].ToString());
            Items = pricelistItemRepo.GetByPricelist(this);
        }
    }
}
