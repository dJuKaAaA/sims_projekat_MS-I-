using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Data.OleDb;

namespace NaplatneRampeSrbije.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public City City { get; set; }

        public Address()
        {
        }

        public Address(int id, string street, string number, City city)
        {
            ID = id;
            Street = street;
            Number = number;
            City = city;
        }

        public Address(OleDbDataReader reader, ICityRepo cityRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            Street = reader[1].ToString();
            Number = reader[2].ToString();
            City = cityRepo.GetByID(Convert.ToInt32(reader[3]));
        }

        public override string ToString()
        {
            return City.ToString() + ", " + Street + ", " + Number;
        }
    }
}