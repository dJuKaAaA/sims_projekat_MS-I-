using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class EquipmentFailureService : IEquipmentFailureService
    {
        private readonly IEquipmentFailureRepo _equipmentFailureRepo;
        private readonly ITollBoothRepo _tollBoothRepo;

        public EquipmentFailureService(IEquipmentFailureRepo equipmentFailureRepo, ITollBoothRepo tollBoothRepo)
        {
            _equipmentFailureRepo = equipmentFailureRepo;
            _tollBoothRepo = tollBoothRepo;
        }

        public void CreateEquipmentFailure(Equipment equipment, string description, FailureType failureType, int tollBoothID)
        {
            EquipmentFailure equipmentFailure = new EquipmentFailure(
                _equipmentFailureRepo.GenerateNewID(),
                equipment,
                description,
                failureType,
                _tollBoothRepo.GetByID(tollBoothID));

            _equipmentFailureRepo.Save(equipmentFailure);
        }

    }
}
