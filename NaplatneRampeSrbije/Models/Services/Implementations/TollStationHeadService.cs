using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class TollStationHeadService : ITollStationHeadService
    {
        private readonly ITollBoothRepo _tollBoothRepo;
        private readonly ITollStationRepo _tollStationRepo;

        public TollStationHeadService(ITollBoothRepo tollBoothRepo, ITollStationRepo tollStationRepo)
        {
            _tollBoothRepo = tollBoothRepo;
            _tollStationRepo = tollStationRepo;
        }

        public void GenerateReport()
        {
            string fileName = "IzvestajOPrihodu.txt";
            using (StreamWriter writer = new StreamWriter(@"../../../../Izvestaji/" + fileName))
            {
                GenerateTollBoothData(writer);
                GenerateTollStationData(writer);
            }
        }

        private void GenerateTollStationData(StreamWriter writer)
        {
            writer.WriteLine("Izvestaj o zaradi na naplatnim stanicama:");
            writer.WriteLine("---------------------------------------");
            foreach (KeyValuePair<TollStation, double> kvp in _tollStationRepo.GetEarnings())
            {
                writer.WriteLine(kvp.Key.ToString() + ": " + kvp.Value);
            }
        }

        private void GenerateTollBoothData(StreamWriter writer)
        {
            writer.WriteLine("Izvestaj o zaradi na naplatnim mestima:");
            writer.WriteLine("---------------------------------------");
            foreach (KeyValuePair<TollBooth, double> kvp in _tollBoothRepo.GetEarnings())
            {

                writer.WriteLine(kvp.Key.ToString() + ": " + kvp.Value);
            }
        }
    }
}
