using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class EmployeeRepo : IEmployeeRepo
    {
        public Employee GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM radnik WHERE radnik_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Employee employee = null;
            while (reader.Read())
            {
                employee = new Employee(
                    reader,
                    new TollBoothRepo(),
                    new TollStationRepo(),
                    new AddressRepo());
            }
            reader.Close();
            return employee;
        }

        public int GenerateNewID()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT max(radnik_id) FROM radnik";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            int largestID = 0;
            while (reader.Read())
            {
                largestID = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return largestID + 1;
        }

        public List<Employee> GetAll()
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            List<Employee> allEmployees = new List<Employee>();
            string query = "SELECT * FROM radnik";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Employee employee = null;
            while (reader.Read())
            {
                employee = new Employee(
                    reader,
                    new TollBoothRepo(),
                    new TollStationRepo(),
                    new AddressRepo());
                allEmployees.Add(employee);
            }
            reader.Close();
            return allEmployees;
        }

        public void Save(Employee newEmployee)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM radnik WHERE radnik_id = {newEmployee.ID} OR korisnicko_ime = '{newEmployee.Username}' OR lozinka = '{newEmployee.Password}'";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Employee employee;
            while (reader.Read())
            {
                employee = new Employee(
                    reader,
                    new TollBoothRepo(),
                    new TollStationRepo(),
                    new AddressRepo());
                if (employee != null)
                {
                    reader.Close();
                    throw new Exception("Vec postojece korisnicko ime ili sifra");
                }
            }
            reader.Close();

            int workSpaceID = -1;
            if (newEmployee.WorkPlace == WorkPlace.BillingOfficer)
            {
                workSpaceID = newEmployee.TollBooth.ID;
            }
            else if (newEmployee.WorkPlace == WorkPlace.TollStationHead)
            {
                workSpaceID = newEmployee.TollStation.ID;
            }

            if (workSpaceID == -1)
            {
                query = $"INSERT INTO radnik (radnik_id, ime, prezime, pol, telefon, korisnicko_ime, lozinka, radno_mesto, adresa_id, mesto_rada_id) VALUES ({newEmployee.ID}, '{newEmployee.FirstName}', '{newEmployee.LastName}', {Convert.ToInt32(newEmployee.Gender)}, '{newEmployee.PhoneNumber}', '{newEmployee.Username}', '{newEmployee.Password}', {Convert.ToInt32(newEmployee.WorkPlace)}, {newEmployee.Address.ID}, NULL)";
            }
            else
            {
                query = $"INSERT INTO radnik (radnik_id, ime, prezime, pol, telefon, korisnicko_ime, lozinka, radno_mesto, adresa_id, mesto_rada_id) VALUES ({newEmployee.ID}, '{newEmployee.FirstName}', '{newEmployee.LastName}', {Convert.ToInt32(newEmployee.Gender)}, '{newEmployee.PhoneNumber}', '{newEmployee.Username}', '{newEmployee.Password}', {Convert.ToInt32(newEmployee.WorkPlace)}, {newEmployee.Address.ID}, {workSpaceID})";
            }
            command = new OleDbCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void Update(Employee employee)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            int workSpaceID = -1;
            if (employee.WorkPlace == WorkPlace.BillingOfficer)
            {
                workSpaceID = employee.TollBooth.ID;
            }
            else if (employee.WorkPlace == WorkPlace.TollStationHead)
            {
                workSpaceID = employee.TollStation.ID;
            }

            string query;
            if (workSpaceID == -1)
            {
                query = $"UPDATE radnik SET ime = '{employee.FirstName}', prezime = '{employee.LastName}', pol = {Convert.ToInt32(employee.Gender)}, telefon = '{employee.PhoneNumber}', korisnicko_ime = '{employee.Username}', lozinka = '{employee.Password}', radno_mesto = {Convert.ToInt32(employee.WorkPlace)}, adresa_id = {employee.Address.ID}, mesto_rada_id = NULL WHERE radnik_id = {employee.ID}";
            }
            else
            {
                query = $"UPDATE radnik SET ime = '{employee.FirstName}', prezime = '{employee.LastName}', pol = {Convert.ToInt32(employee.Gender)}, telefon = '{employee.PhoneNumber}', korisnicko_ime = '{employee.Username}', lozinka = '{employee.Password}', radno_mesto = {Convert.ToInt32(employee.WorkPlace)}, adresa_id = {employee.Address.ID}, mesto_rada_id = {workSpaceID} WHERE radnik_id = {employee.ID}";
            }
            connection.Open();
            OleDbCommand command = new OleDbCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"DELETE FROM radnik WHERE radnik_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public Employee GetByWorkSpaceID(int workSpaceID, WorkPlace workPlace)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM radnik WHERE mesto_rada_id = {workSpaceID} AND radno_mesto = {Convert.ToInt32(workPlace)}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            Employee employee = null;
            while (reader.Read())
            {
                employee = new Employee(
                    reader,
                    new TollBoothRepo(),
                    new TollStationRepo(),
                    new AddressRepo());
            }
            reader.Close();
            return employee;
        }
    }
}
