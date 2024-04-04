using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class TollService
    {
        public static readonly List<(int StartHour, int StartMinute, int EndHour, int EndMinute, int TollFee)> TollFeeTimeLookup = new List<(int, int, int, int, int)>
        {
            (StartHour: 6, StartMinute: 0, EndHour: 6, EndMinute: 29, TollFee: 8), //Create intervals and their prices
            (StartHour: 6, StartMinute: 30, EndHour: 6, EndMinute: 59, TollFee: 13),
            (StartHour: 7, StartMinute: 0, EndHour: 7, EndMinute: 59, TollFee: 18),
            (StartHour: 8, StartMinute: 0, EndHour: 8, EndMinute: 29, TollFee: 13),
            (StartHour: 8, StartMinute: 30, EndHour: 14, EndMinute: 59, TollFee: 8),
            (StartHour: 15, StartMinute: 0, EndHour: 15, EndMinute: 29, TollFee: 13),
            (StartHour: 15, StartMinute: 30, EndHour: 16, EndMinute: 59, TollFee: 18),
            (StartHour: 17, StartMinute: 0, EndHour: 17, EndMinute: 59, TollFee: 13),
            (StartHour: 18, StartMinute: 0, EndHour: 18, EndMinute: 29, TollFee: 8),
            (StartHour: 18, StartMinute: 30, EndHour: 05, EndMinute: 59, TollFee: 0),
        };

        public static readonly HashSet<string> TollFreeVehicleTypes = new HashSet<string>
            {
                "Motorbike", //Create vehicle type hashset containing free vehicles
                "Tractor",
                "Emergency",
                "Diplomat",
                "Foreign",
                "Military"
            };

        public bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            string vehicleType = vehicle.GetVehicleType();
            return TollFreeVehicleTypes.Contains(vehicleType);
        }


        public bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2013)
            {
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
