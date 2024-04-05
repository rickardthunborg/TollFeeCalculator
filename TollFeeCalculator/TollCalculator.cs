using System;
using System.Globalization;
using TollFeeCalculator;

public class TollCalculator : ITollCalculator
{

    private readonly ITollFeeCalculator _feeCalculator;

    public TollCalculator(ITollFeeCalculator feeCalculator)
    {
        _feeCalculator = feeCalculator;
    }

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        int highestFee = 0; // By adding lastFee we can check for cases where the previous fee is higher or lower than the interalStart's is and approriately remove the highest fee we have added so far.
        foreach (DateTime date in dates)
        {
            int nextFee = _feeCalculator.GetTollFee(date, vehicle);
            int tempFee = _feeCalculator.GetTollFee(intervalStart, vehicle);
            
            TimeSpan timeDifference = date - intervalStart; 
            double minutes = timeDifference.TotalMinutes;

            if (dates[0].Year != date.Year || dates[0].Month != date.Month || dates[0].Day != date.Day)
            {
                throw new Exception("Cannot process multiple days");
            }

            if (minutes <= 60)
            {
                if(highestFee > nextFee)
                {
                    continue;
                }
                else 
                {
                if (totalFee > 0 && (highestFee <= tempFee || highestFee == 0)) totalFee -= tempFee;

                else if(totalFee > 0) totalFee -= highestFee;

                if (nextFee >= tempFee) tempFee = nextFee;

                if (tempFee < highestFee) tempFee = highestFee;

                totalFee += tempFee;
                if(highestFee < tempFee) highestFee = tempFee;
                }
            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
                highestFee = 0;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

}   