using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class EmployeeCRUDService : IEmployeeCRUDService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IAddressRepo _addressRepo;
        private readonly ITollBoothRepo _tollBoothRepo;
        private readonly ITollStationRepo _tollStationRepo;

        public EmployeeCRUDService(
            IEmployeeRepo employeeRepo,
            IAddressRepo addressRepo,
            ITollBoothRepo tollBoothRepo,
            ITollStationRepo tollStationRepo)
        {
            _employeeRepo = employeeRepo;
            _addressRepo = addressRepo;
            _tollBoothRepo = tollBoothRepo;
            _tollStationRepo = tollStationRepo;
        }

        public void CreateEmployee(string firstName, string lastName, Gender gender, string phoneNumber, WorkPlace workPlace, string username, string password, int addressID, int workSpaceID)
        {
            Address adresa = _addressRepo.GetByID(addressID);
            Employee employee;
            int id = _employeeRepo.GenerateNewID();
            switch (workPlace)
            {
                case WorkPlace.BillingOfficer:
                    employee = new Employee(id, firstName, lastName, gender, phoneNumber, workPlace, username, password, adresa, _tollBoothRepo.GetByID(workSpaceID), null);
                    break;
                case WorkPlace.TollStationHead:
                    employee = new Employee(id, firstName, lastName, gender, phoneNumber, workPlace, username, password, adresa, null, _tollStationRepo.GetByID(workSpaceID));
                    break;
                default:
                    employee = new Employee(id, firstName, lastName, gender, phoneNumber, workPlace, username, password, adresa, null, null);
                    break;
            }
            Employee employeeByWorkSpaceID = _employeeRepo.GetByWorkSpaceID(workSpaceID, workPlace);
            if (employeeByWorkSpaceID != null)
            {
                throw new Exception("Radno mesto zauzeto");
            }

            _employeeRepo.Save(employee);
        }

        public void UpdateEmployee(int oldID, string firstName, string lastName, Gender gender, string phoneNumber, WorkPlace workPlace, string username, string password, int addressID, int workSpaceID)
        {
            Address adresa = _addressRepo.GetByID(addressID);
            Employee employee;
            switch (workPlace)
            {
                case WorkPlace.BillingOfficer:
                    employee = new Employee(oldID, firstName, lastName, gender, phoneNumber, workPlace, username, password, adresa, _tollBoothRepo.GetByID(workSpaceID), null);
                    break;
                case WorkPlace.TollStationHead:
                    employee = new Employee(oldID, firstName, lastName, gender, phoneNumber, workPlace, username, password, adresa, null, _tollStationRepo.GetByID(workSpaceID));
                    break;
                case WorkPlace.Manager:
                    employee = new Employee(oldID, firstName, lastName, gender, phoneNumber, workPlace, username, password, adresa, null, null);
                    break;
                case WorkPlace.Admininstrator:
                    employee = new Employee(oldID, firstName, lastName, gender, phoneNumber, workPlace, username, password, adresa, null, null);
                    break;
                default:
                    employee = new Employee();
                    break;
            }
            Employee employeeByWorkSpaceID = _employeeRepo.GetByWorkSpaceID(workSpaceID, workPlace);
            if (employeeByWorkSpaceID != null && employeeByWorkSpaceID.ID != employee.ID)
            {
                throw new Exception("Radno mesto zauzeto");
            }
            _employeeRepo.Update(employee);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepo.Delete(id);
        }
    }
}
