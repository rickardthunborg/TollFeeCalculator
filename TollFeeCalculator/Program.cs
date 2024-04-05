﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TollService tollService = new TollService();


            ITollFeeCalculator _feeCalculator = new TollFeeCalculator(tollService);

            TollCalculator calculator = new TollCalculator(_feeCalculator);

            DateTime[] dates = new DateTime[]
            {
                 new DateTime(2024, 10, 28, 6, 28, 0),
                 new DateTime(2024, 10, 28, 6, 59, 0),
                 new DateTime(2024, 10, 28, 7, 12, 0),
                 new DateTime(2024, 10, 28, 8, 39, 0),

            };

            Car car = new Car();

            int tollFee = calculator.GetTollFee(car, dates);
            Console.WriteLine(tollFee);
            Console.ReadLine();
        }
    }
}
