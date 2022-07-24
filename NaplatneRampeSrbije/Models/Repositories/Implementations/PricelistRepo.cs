using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class PricelistRepo : IPricelistRepo
    {
        public Pricelist GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM cenovnik WHERE cenovnik_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Pricelist pricelist = null;
            while (reader.Read())
            {
                pricelist = new Pricelist(reader, new PricelistItemRepo());
            }
            reader.Close();
            return pricelist;
        }

        public Pricelist GetCurrent()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM cenovnik WHERE datum_kraja = 'null'";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Pricelist pricelist = null;
            while (reader.Read())
            {
                pricelist = new Pricelist(reader, new PricelistItemRepo());
            }
            reader.Close();
            return pricelist;
        }
    }
}
