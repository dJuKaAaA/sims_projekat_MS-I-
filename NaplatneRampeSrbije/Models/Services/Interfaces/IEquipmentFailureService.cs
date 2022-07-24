using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface IEquipmentFailureService
    {
        void CreateEquipmentFailure(Equipment equipment, string description, FailureType failureType, int tollBoothID);
    }
}
