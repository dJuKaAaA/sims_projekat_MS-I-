using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class EquipmentFailureRepo : IEquipmentFailureRepo
    {
        public EquipmentFailure GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM kvar WHERE kvar_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EquipmentFailure equipmentFailure = null;
            while (reader.Read())
            {
                equipmentFailure = new EquipmentFailure(reader, new TollBoothRepo());
            }
            reader.Close();
            return equipmentFailure;
        }

        public int GenerateNewID()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT max(kvar_id) FROM kvar";
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

        public void Save(EquipmentFailure failure)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"INSERT INTO kvar (kvar_id, oprema, opis, naplatno_mesto_id, vrsta_kvara, popravljeno) VALUES ({failure.ID}, {Convert.ToInt32(failure.Equipment)}, '{failure.Description}', {failure.TollBooth.ID}, {Convert.ToInt32(failure.FailureType)}, {failure.IsFixed})";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<EquipmentFailure> GetAllNotFixed()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM kvar WHERE popravljeno = FALSE";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            List<EquipmentFailure> equipmentFailures = new List<EquipmentFailure>();
            while (reader.Read())
            {
                equipmentFailures.Add(new EquipmentFailure(reader, new TollBoothRepo()));
            }
            reader.Close();
            return equipmentFailures;
        }

        public void FixByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"UPDATE kvar SET popravljeno = TRUE WHERE kvar_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
