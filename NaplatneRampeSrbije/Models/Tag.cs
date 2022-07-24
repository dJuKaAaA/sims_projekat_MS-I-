using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class Tag
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public VehicleType VehicleType { get; set; }
        public double Balance { get; set; }

        public Tag()
        {

        }

        public Tag(string iD, string firstName, string lastName, VehicleType vehicleType, double balance)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            VehicleType = vehicleType;
            Balance = balance;
        }

        public Tag(OleDbDataReader reader)
        {
            ID = reader[0].ToString();
            FirstName = reader[1].ToString();
            LastName = reader[2].ToString();
            VehicleType = (VehicleType)reader[3];
            Balance = Convert.ToDouble(reader[4]);
        }
    }
}
