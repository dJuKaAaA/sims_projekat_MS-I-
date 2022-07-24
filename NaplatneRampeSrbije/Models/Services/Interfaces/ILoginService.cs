using System;
using System.Collections.Generic;
using System.Text;

namespace NaplatneRampeSrbije.Models.Services.Interfaces
{
    public interface ILoginService
    {
        bool TryLogin(string username, string password);
    }
}
