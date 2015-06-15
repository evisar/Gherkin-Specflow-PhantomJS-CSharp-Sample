using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seavus.Framework.SeleniumPageAutomation;

namespace CalculatorWebTests
{
    /// <summary>
    /// Calculator Page Object
    /// </summary>
    public class CalculatorPage : PageObject<CalculatorPage>
    {
        public int Decimals { get; set; }

        public double Round(double value)
        {
            return Math.Round(value, Decimals);
        }

        public double GetValue()
        {
            string resultText = GetText("Result");
            double result;
            double.TryParse(resultText, out result);
            return Round(result);
        }


    }

}
