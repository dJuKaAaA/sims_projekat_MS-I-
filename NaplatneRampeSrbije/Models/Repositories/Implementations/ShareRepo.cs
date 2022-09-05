using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Data.OleDb;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class ShareRepo : IShareRepo
    {
        public Share GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM deonica WHERE deonica_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Share share = null;
            while (reader.Read())
            {
                share = new Share(reader, new TollStationRepo());
            }
            reader.Close();
            return share;
        }

        public Share GetByEnterExitTollStation(int enteredTollStationID, int exitedTollStationID)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM deonica WHERE pocetak_naplatna_stanica_id = {enteredTollStationID} AND kraj_naplatna_stanica_id = {exitedTollStationID}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Share share = null;
            while (reader.Read())
            {
                share = new Share(reader, new TollStationRepo());
            }
            reader.Close();
            return share;
        }

        public int GenerateNewID()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT max(deonica_id) FROM deonica";
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

        public void Save(Share share)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"INSERT INTO deonica (deonica_id, duzina, pocetak_naplatna_stanica_id, kraj_naplatna_stanica_id, doz_brz_kretanja) VALUES ({share.ID}, {share.Length}, {share.TollStationEntered.ID}, {share.TollStatioExited.ID}, {share.SpeedLimit})";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
