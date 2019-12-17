using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using TwinfieldTask.ApiVariant1;
using TwinfieldTask;

namespace TwinfieldTaskTests.ApiVariant1
{
    public class TestCases
    {
        static public IEnumerable<(IEnumerable<string> ProductCodes, decimal ExpectedTotal)> Cases()
        {
            yield return (ProductCodes: "ABCDABA".ToCharArray().Select(a=>a.ToString()), ExpectedTotal: 13.25M);
            yield return (ProductCodes: "CCCCCCC".ToCharArray().Select(a => a.ToString()), ExpectedTotal: 6.00M);
            yield return (ProductCodes: "ABCD".ToCharArray().Select(a => a.ToString()), ExpectedTotal: 7.25M);
        }

        [Test]
        [TestCaseSource(nameof(Cases))]
        public void VolumePriceCalculation((IEnumerable<string> ProductCodes, decimal ExpectedTotal) testCase)
        {
            //arrange
            var terminal = new PointOfSaleTerminal();
            terminal.SetPricing(new ProductsPricingService());
            testCase.ProductCodes.ToList().ForEach(
                productCode => terminal.Scan(productCode)
            );

            //act
            var actualTotal = terminal.CalculateTotal();

            //assert
            Assert.AreEqual(testCase.ExpectedTotal, actualTotal);
        }
    }
}
