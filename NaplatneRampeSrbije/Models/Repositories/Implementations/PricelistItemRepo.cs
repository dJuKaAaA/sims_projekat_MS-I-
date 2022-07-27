using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class PricelistItemRepo : IPricelistItemRepo
    {
        public List<PricelistItem> GetByPricelist(Pricelist pricelist)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            List<PricelistItem> items = new List<PricelistItem>();
            string query = $"SELECT * FROM stavka_cenovnika WHERE cenovnik_id = {pricelist.ID}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new PricelistItem(reader, new ShareRepo(), pricelist));
            }
            reader.Close();
            return items;
        }
    }
}
