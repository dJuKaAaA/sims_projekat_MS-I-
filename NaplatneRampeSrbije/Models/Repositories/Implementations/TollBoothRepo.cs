using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class TollBoothRepo : ITollBoothRepo
    {
        public TollBooth GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM naplatno_mesto WHERE naplatno_mesto_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            TollBooth tollBooth = null;
            while (reader.Read())
            {
                tollBooth = new TollBooth(reader, new TollStationRepo());
            }
            reader.Close();
            return tollBooth;
        }

        public List<TollBooth> GetAll()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM naplatno_mesto";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            List<TollBooth> tollBooths = new List<TollBooth>();
            while (reader.Read())
            {
                tollBooths.Add(new TollBooth(reader, new TollStationRepo()));
            }
            reader.Close();
            return tollBooths;
        }

        public void Save(TollBooth tollBooth)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"INSERT INTO naplatno_mesto (naplatno_mesto_id, naplatna_stanica_id, el_naplata) VALUES ({tollBooth.ID}, {tollBooth.TollStation.ID}, {tollBooth.ElectronicPayment})";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Edit(TollBooth tollBooth)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"UPDATE naplatno_mesto SET naplatna_stanica_id = {tollBooth.TollStation.ID}, el_naplata = {tollBooth.ElectronicPayment} WHERE naplatno_mesto_id = {tollBooth.ID}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"DELETE FROM naplatno_mesto WHERE naplatno_mesto_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public int GenerateNewID()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT max(naplatno_mesto_id) FROM naplatno_mesto";
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

        public Dictionary<TollBooth, double> GetEarnings()
        {
            Dictionary<TollBooth, double> earnings = new Dictionary<TollBooth, double>();
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);
            string query = $"SELECT izlazak_naplatno_mesto_id, sum(cena) FROM racun GROUP BY izlazak_naplatno_mesto_id";

            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                earnings[GetByID(Convert.ToInt32(reader[0]))] = Convert.ToDouble(reader[1]);
            }
            reader.Close();
            return earnings;
        }
    }
}
