﻿using System;
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
        int lastFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = _feeCalculator.GetTollFee(date, vehicle);
            int tempFee = _feeCalculator.GetTollFee(intervalStart, vehicle);
            

            TimeSpan timeDifference = date - intervalStart;
            double minutes = timeDifference.TotalMinutes;

            if (minutes <= 60)
            {
                if (totalFee > 0 && (lastFee == tempFee || lastFee == 0)) {
                    totalFee -= tempFee;
                }
                else if(lastFee != tempFee && totalFee > 0) 
                {
                    totalFee -= lastFee;
                }

                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
                lastFee = nextFee;
            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
                lastFee = 0;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

}   