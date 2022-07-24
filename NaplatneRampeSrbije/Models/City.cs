using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }

        public City()
        {

        }

        public City(int id, string name, string zipCode)
        {
            ID = id;
            Name = name;
            ZipCode = zipCode;
        }

        public City(OleDbDataReader reader)
        {
            ID = Convert.ToInt32(reader[0]);
            Name = reader[1].ToString();
            ZipCode = reader[2].ToString();
        }

        public override string ToString()
        {
            return Name + ", " + ZipCode;
        }
    }
}
