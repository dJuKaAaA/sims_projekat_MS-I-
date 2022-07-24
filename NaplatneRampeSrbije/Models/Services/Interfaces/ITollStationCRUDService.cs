using NaplatneRampeSrbije.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface ITollStationCRUDService
    {
        List<TollStationDTO> GetTollStationsForDisplay();
        void CreateTollStation(string street, string number, string zipCode);
        void UpdateTollStation(int id, string street, string number, string zipCode);
        void DeleteTollStation(int id);
    }
}
