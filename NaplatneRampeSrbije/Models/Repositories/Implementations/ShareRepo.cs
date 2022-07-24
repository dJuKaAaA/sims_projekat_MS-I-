using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System.Data.OleDb;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class ShareRepo : IShareRepo
    {
        public Share GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

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
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

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
    }
}
