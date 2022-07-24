using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace NaplatneRampeSrbije.Models
{
    public class Share
    {
        public int ID { get; set; }
        public int Length { get; set; }
        public TollStation TollStationEntered { get; set; }
        public TollStation TollStatioExited { get; set; }

        public Share()
        {

        }

        public Share(int id, int length, TollStation tollStationEntered, TollStation tollStationExited)
        {
            ID = id;
            Length = length;
            TollStationEntered = tollStationEntered;
            TollStatioExited = tollStationExited;
        }

        public Share(OleDbDataReader reader, ITollStationRepo tollStationRepo)
        {
            ID = Convert.ToInt32(reader[0]);
            Length = Convert.ToInt32(reader[1]);
            TollStationEntered = tollStationRepo.GetByID(Convert.ToInt32(reader[2]));
            TollStatioExited = tollStationRepo.GetByID(Convert.ToInt32(reader[3]));
        }
    }
}
