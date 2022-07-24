using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IVehicleCardRepo
    {
        VehicleCard GetByID(int id);
    }
}
