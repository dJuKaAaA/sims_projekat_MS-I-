using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface ITollBoothRepo
    {
        TollBooth GetByID(int id);
        List<TollBooth> GetAll();
        void Save(TollBooth tollBooth);
        void Edit(TollBooth tollBooth);
        void Delete(int id);
        int GenerateNewID();
        Dictionary<TollBooth, double> GetEarnings();
        List<TollBooth> GetAllByTollStationID(int tollStationID);
    }
}
