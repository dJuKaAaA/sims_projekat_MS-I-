using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface ITollStationRepo
    {
        List<TollStation> GetAll();
        TollStation GetByID(int id);
        int GenerateNewID();
        void Save(TollStation tollStation);
        void Update(TollStation tollStation);
        void Delete(int id);
        Dictionary<TollStation, double> GetEarnings();
        public List<int> GetTollStationsByShare(TollStation tollStation);
    }
}
