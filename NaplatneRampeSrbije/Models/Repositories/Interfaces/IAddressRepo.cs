using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IAddressRepo
    {
        List<Address> GetAll();
        int GenerateNewID();
        Address GetByID(int id);
        void Save(Address address);
        void Update(Address address);
        void Delete(int id);
    }
}
