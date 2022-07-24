using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class BillRepo : IBillRepo
    {
        public List<Bill> GetAll()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM racun";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            List<Bill> bills = new List<Bill>();
            while (reader.Read())
            {
                bills.Add(new Bill(reader, new TollBoothRepo()));
            }
            reader.Close();
            return bills;
        }

        public int GenerateNewID()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT max(racun_id) FROM racun";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            int largestID = 0;
            while (reader.Read())
            {
                largestID = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return largestID + 1;
        }

        public void Save(Bill bill)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"INSERT INTO racun (racun_id, vozilo, cena, valuta, vreme_izlaska, izlazak_naplatno_mesto_id, ulazak_naplatno_mesto_id) VALUES ({bill.ID}, {Convert.ToInt32(bill.VehicleType)}, {bill.Price}, {Convert.ToInt32(bill.Currency)}, '{bill.ExitDate.ToString(Globals.formatDatumVreme)}', {bill.TollBothExited.ID}, {bill.TollBothEntered.ID})";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
