using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class TollBoothCRUDService : ITollBoothCRUDService
    {
        private readonly ITollBoothRepo _tollBoothRepo;
        private readonly ITollStationRepo _tollStationRepo;

        public TollBoothCRUDService(ITollBoothRepo tollBoothRepo, ITollStationRepo tollStationRepo)
        {
            _tollBoothRepo = tollBoothRepo;
            _tollStationRepo = tollStationRepo;
        }

        public void SaveTollBooth(int tollStationID, bool electronicPayment)
        {
            TollBooth naplatnoMesto = new TollBooth(_tollBoothRepo.GenerateNewID(), _tollStationRepo.GetByID(tollStationID), electronicPayment);
            _tollBoothRepo.Save(naplatnoMesto);
        }

        public void EditTollBooth(int id, int tollStationID, bool electronicPayment)
        {
            TollBooth naplatnoMesto = new TollBooth(id, _tollStationRepo.GetByID(tollStationID), electronicPayment);
            _tollBoothRepo.Edit(naplatnoMesto);
        }

        public void DeleteTollBooth(int id)
        {
            _tollBoothRepo.Delete(id);
        }
    }
}
