using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IEquipmentFailureRepo
    {
        EquipmentFailure GetByID(int id);
        int GenerateNewID();
        void Save(EquipmentFailure failure);
        List<EquipmentFailure> GetAll();
    }
}
