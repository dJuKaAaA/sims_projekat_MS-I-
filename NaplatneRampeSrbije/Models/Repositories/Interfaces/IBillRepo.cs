using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IBillRepo
    {
        List<Bill> GetAll();
        void Save(Bill bill);
        int GenerateNewID();
    }
}
