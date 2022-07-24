using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface ICityRepo
    {
        City GetByID(int id);
        List<City> GetAll();
        City GetByZipCode(string zipCode);
    }
}
