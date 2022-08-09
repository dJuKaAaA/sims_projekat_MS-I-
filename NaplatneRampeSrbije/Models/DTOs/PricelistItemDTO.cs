using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.DTOs
{
    public class PricelistItemDTO
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public VehicleType VehicleType { get; set; }
        public TollStation EnteredTollStation { get; set; }
        public TollStation ExitedTollStation { get; set; }

        public PricelistItemDTO()
        {

        }

        public PricelistItemDTO(int id, double price, VehicleType vehicleType, TollStation enteredTollStation, TollStation exitedTollStation)
        {
            ID = id;
            Price = price;
            VehicleType = vehicleType;
            EnteredTollStation = enteredTollStation;
            ExitedTollStation = exitedTollStation;
        }

        public PricelistItemDTO(PricelistItem pricelistItem)
        {
            ID = pricelistItem.ID;
            Price = pricelistItem.Price;
            VehicleType = pricelistItem.VehicleType;
            EnteredTollStation = pricelistItem.Share.TollStationEntered;
            ExitedTollStation = pricelistItem.Share.TollStatioExited;
        }
    }
}
