using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class TollFeeCalculator : ITollFeeCalculator
    {
        public TollService TollService { get; set; }


        public TollFeeCalculator(TollService tollService)
        {
            TollService = tollService;
        }
        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (TollService.IsTollFreeDate(date) || TollService.IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            var tollFeeTimeLookup = TollService.TollFeeTimeLookup;

            foreach (var interval in tollFeeTimeLookup)
            {
                if (hour >= interval.StartHour && hour <= interval.EndHour &&
                    minute >= interval.StartMinute && minute <= interval.EndMinute) return interval.TollFee;
            }

            return 0;
        }
    }
}

