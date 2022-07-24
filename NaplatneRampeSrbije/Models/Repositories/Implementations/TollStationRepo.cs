using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class TollStationRepo : ITollStationRepo
    {
        public List<TollStation> GetAll()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM naplatna_stanica";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            List<TollStation> tollStations = new List<TollStation>();
            while (reader.Read())
            {
                tollStations.Add(new TollStation(reader, new AddressRepo()));
            }
            reader.Close();
            return tollStations;
        }

        public TollStation GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM naplatna_stanica WHERE naplatna_stanica_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            TollStation tollStation = null;
            while (reader.Read())
            {
                tollStation = new TollStation(reader, new AddressRepo());
            }
            reader.Close();
            return tollStation;
        }

        public int GenerateNewID()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT max(naplatna_stanica_id) FROM naplatna_stanica";
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

        public void Save(TollStation tollStation)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"INSERT INTO naplatna_stanica (naplatna_stanica_id, adresa_id) VALUES ({tollStation.ID}, {tollStation.Address.ID})";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(TollStation tollStation)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"UPDATE naplatna_stanica SET adresa_id = {tollStation.Address.ID} WHERE naplatna_stanica_id = {tollStation.ID}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);
            string query = $"DELETE FROM naplatna_stanica WHERE naplatna_stanica_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public Dictionary<TollStation, double> GetEarnings()
        {
            Dictionary<TollStation, double> earnings = new Dictionary<TollStation, double>();
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);
            string query = $"SELECT naplatna_stanica_id, sum(cena) FROM racun, naplatno_mesto WHERE racun.izlazak_naplatno_mesto_id = naplatno_mesto.naplatno_mesto_id GROUP BY naplatna_stanica_id";
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