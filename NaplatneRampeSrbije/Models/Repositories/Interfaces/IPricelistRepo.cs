using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IPricelistRepo
    {
        Pricelist GetByID(int id);
        Pricelist GetCurrent();
    }
}
