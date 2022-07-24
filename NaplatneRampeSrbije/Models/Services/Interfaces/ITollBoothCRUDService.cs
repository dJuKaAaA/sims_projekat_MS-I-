using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface ITollBoothCRUDService
    {
        void SaveTollBooth(int tollStationID, bool electronicPayment);
        void EditTollBooth(int id, int tollStationID, bool electronicPayment);
        void DeleteTollBooth(int id);
    }
}
