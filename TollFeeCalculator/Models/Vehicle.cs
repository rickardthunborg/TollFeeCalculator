using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string VehicleType { get; private set; }

        public Vehicle(string vehicleType)
        {
            VehicleType = vehicleType;
        }
    }
}
