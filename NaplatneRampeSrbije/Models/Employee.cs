using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public WorkPlace WorkPlace { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public TollBooth TollBooth { get; set; }
        public TollStation TollStation { get; set; }
        public Address Address { get; set; }

        public Employee()
        {

        }

        public Employee(int id, string firstName, string lastName, Gender gender, string phoneNumber, WorkPlace workPlace, string username, string password, Address address, TollBooth tollBooth, TollStation tollStation)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PhoneNumber = phoneNumber;
            WorkPlace = workPlace;
            Username = username;
            Password = password;
            TollBooth = tollBooth;
            TollStation = tollStation;
            Address = address;
        }

        public Employee(OleDbDataReader reader, ITollBoothRepo tollBoothRepo, ITollStationRepo tollStationRepo, IAddressRepo addressRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            FirstName = reader[1].ToString();
            LastName = reader[2].ToString();
            Gender = (Gender)reader[3];
            PhoneNumber = reader[4].ToString();
            Username = reader[5].ToString();
            Password = reader[6].ToString();
            Address = addressRepo.GetByID(Convert.ToInt32(reader[8]));

            WorkPlace = (WorkPlace)reader[7];
            if (WorkPlace == WorkPlace.BillingOfficer)
            {
                TollBooth = tollBoothRepo.GetByID(Convert.ToInt32(reader[9]));
                TollStation = null;
            }
            else if (WorkPlace == WorkPlace.TollStationHead)
            {
                TollBooth = null;
                TollStation = tollStationRepo.GetByID(Convert.ToInt32(reader[9]));
            }
            else
            {
                TollBooth = null;
                TollStation = null;
            }
        }
    }
}
