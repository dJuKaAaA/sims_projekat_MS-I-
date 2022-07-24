using NaplatneRampeSrbije.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.DTOs
{
    public class TollStationDTO
    {
        public string ID { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public TollStationDTO(string id, string street, string number, string city, string zipCode)
        {
            ID = id;
            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
        }

        public TollStationDTO(TollStation naplatnaStanica)
        {
            ID = naplatnaStanica.ID.ToString();
            Street = naplatnaStanica.Address.Street;
            Number = naplatnaStanica.Address.Number;
            City = naplatnaStanica.Address.City.Name;
            ZipCode = naplatnaStanica.Address.City.ZipCode;
        }

    }
}