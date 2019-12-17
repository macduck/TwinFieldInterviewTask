using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using TwinfieldTask.ApiVariant2;
using TwinfieldTask.Data;

namespace TwinfieldTaskTests.ApiVariant2
{
    public class TestCases
    {
        private PointOfSaleTerminal _terminal;
        [SetUp]
        public void Setup()
        {

            _terminal = new PointOfSaleTerminal(orderingSessionFactory: () => 
                new OrderingSession(
                    new ProductsRepository(),
                    new VolumeDiscountCalculationStrategy(
                        new ProductVolumeDiscountRulesRepository()
                    ))
            );
        }

        public static IEnumerable<(IEnumerable<string> ProductCodes, decimal ExpectedTotal)> Cases()
        {
            yield return (ProductCodes: "ABCDABA".ToCharArray().Select(a => a.ToString()), ExpectedTotal: 13.25M);
            yield return (ProductCodes: "CCCCCCC".ToCharArray().Select(a => a.ToString()), ExpectedTotal: 6.00M);
            yield return (ProductCodes: "ABCD".ToCharArray().Select(a => a.ToString()), ExpectedTotal: 7.25M);
        }

        [Test]
        [TestCaseSource(nameof(Cases))]
        public void VolumePriceCalculation((IEnumerable<string> ProductCodes, decimal ExpectedTotal) testCase)
        {
            //arrange
            var session = _terminal.OpenOrderingSession();
            testCase.ProductCodes.ToList()
                .ForEach(productCode => 
                    _terminal.Scan(productCode));

            //act
            var actualTotal = _terminal.CalculateTotal();

            //assert
            Assert.AreEqual(testCase.ExpectedTotal, actualTotal);
        }
    }
}
