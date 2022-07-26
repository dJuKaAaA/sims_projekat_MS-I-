﻿using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class Bill
    {
        public int ID { get; set; }
        public VehicleType VehicleType { get; set; }
        public double Price { get; set; }
        public Currency Currency { get; set; }
        public DateTime ExitDate { get; set; }
        public TollBooth TollBothExited { get; set; }
        public TollBooth TollBothEntered { get; set; }
        public DateTime EntryDate { get; set; }
        public int AverageMovingSpeed { get; set; }

        public Bill()
        {
        }

        public Bill(int id, VehicleType vehicleType, double price, Currency currency, DateTime exitDate, TollBooth tollBothExited, TollBooth tollBothEntered, DateTime entryDate, int averageMovingSpeed)
        {
            ID = id;
            VehicleType = vehicleType;
            Price = price;
            Currency = currency;
            ExitDate = exitDate;
            TollBothExited = tollBothExited;
            TollBothEntered = tollBothEntered;
            EntryDate = entryDate;
            AverageMovingSpeed = averageMovingSpeed;
        }

        public Bill(OleDbDataReader reader, ITollBoothRepo tollBoothRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            VehicleType = (VehicleType)reader[1];
            Price = Convert.ToDouble(reader[2]);
            Currency = (Currency)reader[3];
            ExitDate = DateTime.ParseExact(reader[4].ToString(), Globals.dateTimeFormat, null);
            TollBothExited = tollBoothRepo.GetByID(Convert.ToInt32(reader[5]));
            TollBothEntered = tollBoothRepo.GetByID(Convert.ToInt32(reader[6]));
            EntryDate = DateTime.ParseExact(reader[7].ToString(), Globals.dateTimeFormat, null);
            AverageMovingSpeed = Convert.ToInt32(reader[8]);
        }
    }
}