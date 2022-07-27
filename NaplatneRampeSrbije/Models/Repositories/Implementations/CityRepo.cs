using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class CityRepo : ICityRepo
    {
        public City GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM mesto WHERE mesto_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            City city = null;
            while (reader.Read())
            {
                city = new City(reader);
            }
            reader.Close();
            return city;
        }

        public List<City> GetAll()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM mesto";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            List<City> tollBooths = new List<City>();
            City city = null;
            while (reader.Read())
            {
                city = new City(reader);
                tollBooths.Add(city);
            }
            reader.Close();
            return tollBooths;
        }

        public City GetByZipCode(string zipCode)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.connectionPath);

            string query = $"SELECT * FROM mesto WHERE postanski_broj = '{zipCode}'";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            City city = null;
            while (reader.Read())
            {
                city = new City(reader);
            }
            reader.Close();
            return city;
        }
    }
}