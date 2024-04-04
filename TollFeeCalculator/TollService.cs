using PublicHoliday;
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

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.Month == 7) return true;

            bool isHoliday = new SwedenPublicHoliday().IsPublicHoliday(date);

            bool isDayBeforeHoliday = new SwedenPublicHoliday().IsPublicHoliday(date.AddDays(1));

            return isDayBeforeHoliday;
        }
    }
}
