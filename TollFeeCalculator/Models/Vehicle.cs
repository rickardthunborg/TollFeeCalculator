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
        public string RegistrationNumber { get; set; }
        public string VehicleType { get; set; }
        public bool IsTollFree { get; set; }

        public Vehicle(int id, string registrationNumber, string vehicleType, bool isTollFree)
        {
            Id = id;
            RegistrationNumber = registrationNumber;
            VehicleType = vehicleType;
            IsTollFree = isTollFree;
        }
    }
}
