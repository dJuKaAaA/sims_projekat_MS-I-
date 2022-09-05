using NaplatneRampeSrbije.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NaplatneRampeSrbije
{
    public class Globals
    {
        public static Employee signedEmployee = null;
        public static string connectionPath = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\baza.accdb");
        public static string dateFormat = "d.M.yyyy.";
        public static string dateTimeFormat = "d.M.yyyy. HH:mm";
    }
}
