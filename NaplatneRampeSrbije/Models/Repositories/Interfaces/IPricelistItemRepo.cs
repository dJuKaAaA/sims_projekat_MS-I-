using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IPricelistItemRepo
    {
        List<PricelistItem> GetByPricelist(Pricelist pricelist);
    }
}
