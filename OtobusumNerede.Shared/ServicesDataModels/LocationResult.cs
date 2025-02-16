using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;

namespace OtobusumNerede.Shared.ServicesDataModels
{
    public class LocationResult
    {
        public bool IsSuccess { get; set; }
        public Location Location { get; set; }
        public string ErrorMessage { get; set; }

        public static LocationResult Success(Location location)
        {
            return new() { IsSuccess = true, Location = location };
        }

        public static LocationResult Fail(string error)
        {
            return new() { IsSuccess = false, ErrorMessage = error, Location=null };
        }
    }
}
