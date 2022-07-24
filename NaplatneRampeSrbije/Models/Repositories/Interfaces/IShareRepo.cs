using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Repositories.Interfaces
{
    public interface IShareRepo
    {
        Share GetByID(int id);
        Share GetByEnterExitTollStation(int enteredTollStationID, int exitedTollStationID);
    }
}
