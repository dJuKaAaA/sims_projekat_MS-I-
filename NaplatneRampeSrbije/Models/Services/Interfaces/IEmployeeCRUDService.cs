using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface IEmployeeCRUDService
    {
        void CreateEmployee(string firstName, string lastName, Gender gender, string phoneNumber, WorkPlace workPlace, string username, string password, int addressID, int workSpaceID);
        void UpdateEmployee(int oldID, string firstName, string lastName, Gender gender, string phoneNumber, WorkPlace workPlace, string username, string password, int addressID, int workSpaceID);
        void DeleteEmployee(int id);
    }
}
