using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    //By making a TollHelper or TollService we can move methods that support our main ones to a place where they won't directly sit and interfere, instead letting us call them remotely. 
    //This provides ease of maintenance and a cleaner codebase. It could even be argued that we should separate vehicle-related methods and date-related methods in the future for even more convenience.
    public class TollService
    {
        public static readonly List<(int StartHour, int StartMinute, int EndHour, int EndMinute, int TollFee)> TollFeeTimeLookup = new List<(int, int, int, int, int)>
        {
            (StartHour: 6, StartMinute: 0, EndHour: 6, EndMinute: 29, TollFee: 8), //Creating the intervals here allows us to keep this data in a single place instead of risking 
            (StartHour: 6, StartMinute: 30, EndHour: 6, EndMinute: 59, TollFee: 13), // having multiple places where we store them, forcing us to work on more code than necessary in the future.
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
                "Motorbike", //Creating a vehicle type hashset containing free vehicles like this will similarly to the intervals let us keep it in a single secure place
                "Tractor", //freeing us from risk of adding new classes where we have to write the exact same things.
                "Emergency",
                "Diplomat",
                "Foreign",
                "Military"
            };

        public bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) throw new Exception("Vehicle is null."); //Now we can call our Hashset instead of hardcoding it into the method checking for each vehicle. This 
            // makes the method more scalable for future vehicle types.

            string vehicleType = vehicle.GetVehicleType();
            return TollFreeVehicleTypes.Contains(vehicleType);
        }


        public bool IsTollFreeDate(DateTime date) //By taking advantage of nugets/libraries in .NET we can remove the need for hardcoded days to keep track of and also check for every 
            //year ever instead of just 2013. This makes the method infinitely more versatile and robust.
        {
            if (date == null) throw new Exception("Date is null.");

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.Month == 7) return true;

            bool isHoliday = new SwedenPublicHoliday().IsPublicHoliday(date); 
            if(isHoliday) return true;

            bool isDayBeforeHoliday = new SwedenPublicHoliday().IsPublicHoliday(date.AddDays(1));
            return isDayBeforeHoliday;
        }
    }
}
