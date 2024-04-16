using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator.Models;


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
            if (TollService.IsTollFreeDate(date) || vehicle.IsTollFree) return 0;

            var tollFeeTimeLookup = TollService.TollFeeTimeLookup;

            foreach (var interval in tollFeeTimeLookup)
            {
                if ((date.Hour > interval.StartHour || (date.Hour == interval.StartHour && date.Minute >= interval.StartMinute)) &&
                    (date.Hour < interval.EndHour || (date.Hour == interval.EndHour && date.Minute <= interval.EndMinute))) return interval.TollFee;
            }

            return 0;
        }
    } //By separating the toll calculator into more than one class we allow the solution to adhere to single responsibility principles, letting us keep classes focused on a single task
      //instead of grouping them together, which could lead to confusion when more code is developed and the solution gets more complicated.
}

