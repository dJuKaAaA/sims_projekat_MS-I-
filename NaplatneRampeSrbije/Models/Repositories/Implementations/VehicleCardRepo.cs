using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using System.Data.OleDb;

namespace NaplatneRampeSrbije.Models.Repositories.Implementations
{
    public class VehicleCardRepo : IVehicleCardRepo
    {
        public VehicleCard GetByID(int id)
        {
            using OleDbConnection connection = new OleDbConnection(Globals.putanjaKonekcije);

            string query = $"SELECT * FROM kartica WHERE kartica_id = {id}";
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            VehicleCard vehicleCard = null;
            while (reader.Read())
            {
                vehicleCard = new VehicleCard(reader, new TollBoothRepo());
            }
            reader.Close();
            return vehicleCard;
        }
    }
}
