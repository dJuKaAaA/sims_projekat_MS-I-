using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.DTOs;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Implementations
{
    public class TollStationCRUDService : ITollStationCRUDService
    {
        private readonly ITollStationRepo _tollStationRepo;
        private readonly IAddressRepo _addressRepo;
        private readonly ICityRepo _cityRepo;

        public TollStationCRUDService(
            ITollStationRepo tollStationRepo,
            IAddressRepo addressRepo,
            ICityRepo cityRepo)
        {
            _tollStationRepo = tollStationRepo;
            _addressRepo = addressRepo;
            _cityRepo = cityRepo;
        }

        public List<TollStationDTO> GetTollStationsForDisplay()
        {
            List<TollStationDTO> tollStationDTOs = new List<TollStationDTO>();
            List<TollStation> tollStations = _tollStationRepo.GetAll();
            foreach (TollStation tollStation in tollStations)
            {
                TollStationDTO tollStationDTO = new TollStationDTO(tollStation);
                tollStationDTOs.Add(tollStationDTO);
            }

            return tollStationDTOs;
        }

        public void CreateTollStation(string street, string number, string zipCode)
        {
            StreetCheck(street);
            NumberCheck(number);
            CityCheck(zipCode);
            City city = _cityRepo.GetByZipCode(zipCode);
            Address address = new Address(_addressRepo.GenerateNewID(), street, number, city);
            _addressRepo.Save(address);
            TollStation tollStation = new TollStation(_tollStationRepo.GenerateNewID(), address);
            _tollStationRepo.Save(tollStation);
        }

        public void UpdateTollStation(int id, string street, string number, string zipCode)
        {
            StreetCheck(street);
            TollStationIDCheck(id);
            NumberCheck(number);
            CityCheck(zipCode);

            TollStation tollStation = _tollStationRepo.GetByID(id);
            City city = _cityRepo.GetByZipCode(zipCode);

            Address address = _addressRepo.GetByID(tollStation.Address.ID);
            address.Street = street;
            address.Number = number;
            address.City = city;
            _addressRepo.Update(address);

            tollStation.Address = address;
            _tollStationRepo.Update(tollStation);
        }

        public void DeleteTollStation(int id)
        {
            TollStationIDCheck(id);
            TollStation tollStation = _tollStationRepo.GetByID(id);
            _tollStationRepo.Delete(id);
            _addressRepo.Delete(tollStation.Address.ID);
        }

        private void TollStationIDCheck(int id)
        {
            List<TollStation> tollStations = _tollStationRepo.GetAll();
            foreach (TollStation tollStation in tollStations)
            {
                if (tollStation.ID == id)
                {
                    return;
                }
            }
            throw new Exception("Nije pronadjen uneti id naplatne stanice");
        }

        private void NumberCheck(string number)
        {
            if (!int.TryParse(number, out _))
            {
                throw new Exception("Pogresan unos broja!");
            }
        }

        private void CityCheck(string zipCode)
        {
            List<City> cities = _cityRepo.GetAll();
            foreach (City city in cities)
            {
                if (city.ZipCode == zipCode)
                {
                    return;
                }
            }
            throw new Exception("Nije pronadjeno mesto!");
        }

        private void StreetCheck(string street)
        {
            if (street.Trim() == "")
            {
                throw new Exception("Morate uneti ulicu!");
            }
        }
    }
}