using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class LoginService : ILoginService
    {
        public bool TryLogin(string username, string password)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(Globals.connectionPath))
                {
                    string query = $"SELECT * FROM radnik WHERE korisnicko_ime = '{username}' AND lozinka = '{password}'";
                    OleDbCommand command = new OleDbCommand(query, connection);

                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Globals.signedEmployee = new Employee(
                        reader, 
                        new TollBoothRepo(),
                        new TollStationRepo(),
                        new AddressRepo());
                    reader.Close();
                }
            }
            catch
            {
                Globals.signedEmployee = null;
            }

            return Globals.signedEmployee != null;
        }
    }
}
