using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;

namespace CalculatorWebTests
{
    [Binding]
    public class CalculatorSteps
    {

        [AfterFeature]
        public static void CloseBrowser()
        {
            CalculatorPage.Instance.Quit();
        }

        [Given(@"I open calculator")]
        public void GivenIOpenCalculator()
        {
            CalculatorPage.Instance.Open();
        }
        
        [When(@"I enter (.*)")]
        public void WhenIEnter(double p0)
        {
           p0 = CalculatorPage.Instance.Round(p0);
            char[]  keys = p0.ToString().ToCharArray();    
            foreach(char c in keys)
            {
                CalculatorPage.Instance.Click(c.ToString());
            }
        }
        
        [When(@"I press ""(.*)""")]
        public void WhenIPress(string p0)
        {            
            CalculatorPage.Instance.Click(p0);
        }
        
        [When(@"Calculate")]
        public void WhenCalculate(Table table)
        {
            foreach (var row in table.Rows)
            {
                WhenIPress("CE");
                //operand a
                double a = double.Parse(row["a"]);
                WhenIEnter(a);
                //operation
                string op2 = row["op"];
                WhenIPress(op2);
                //operand b
                double b = double.Parse( row["b"]);
                WhenIEnter(b);
                //and I press enter
                WhenIPress("=");
                //expected
                double expected = double.Parse(row["equals"]);
                ThenTheResultShouldBe(expected);
            }
        }
        
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(double p0)
        {
            double currentValue = CalculatorPage.Instance.GetValue();
            Assert.AreEqual(p0, currentValue);
        }

        [When(@"I execute ""(.*)""")]
        public void WhenIExecute(string p0)
        {
            char[] keys = p0.ToString().ToCharArray();
            foreach (char c in keys)
            {
                CalculatorPage.Instance.Click(c.ToString());
            }

        }

    }
}
