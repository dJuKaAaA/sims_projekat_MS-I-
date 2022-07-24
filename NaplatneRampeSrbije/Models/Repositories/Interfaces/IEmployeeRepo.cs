using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IEmployeeRepo
    {
        Employee GetByID(int id);
        int GenerateNewID();
        List<Employee> GetAll();
        void Save(Employee newEmployee);
        void Update(Employee employee);
        void Delete(int id);
        Employee GetByWorkSpaceID(int workSpaceID, WorkPlace workPlace);
    }
}
