using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class AddressRepo : IAddressRepo
    {
        public List<Address> GetAll()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM adresa";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            List<Address> addresses = new List<Address>();
            Address address;
            while (reader.Read())
            {
                address = new Address(reader, new CityRepo());
                addresses.Add(address);
            }
            reader.Close();
            return addresses;
        }

        public int GenerateNewID()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT max(adresa_id) FROM adresa";
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

        public Address GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM adresa WHERE adresa_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Address address = null;
            while (reader.Read())
            {
                address = new Address(reader, new CityRepo());
            }
            reader.Close();

            return address;
        }

        public void Save(Address adress)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"INSERT INTO adresa (adresa_id, ulica, broj, mesto_id) VALUES ({adress.ID}, '{adress.Street}','{adress.Number}',{adress.City.ID})";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(Address address)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"UPDATE adresa SET ulica = '{address.Street}', broj = '{address.Number}', mesto_id = {address.City.ID} WHERE adresa_id = {address.ID}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"DELETE FROM adresa WHERE adresa_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}