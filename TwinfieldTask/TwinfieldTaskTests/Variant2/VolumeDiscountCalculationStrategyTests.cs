using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TwinfieldTask;
using TwinfieldTask.ApiVariant2;
using TwinfieldTask.Data;

namespace TwinfieldTaskTests.ApiVariant2
{
    public class VolumeDiscountCalculationStrategyTests
    {
        private static ProductVolumeDiscountRule _defualtProductVolumeDiscount;
        static VolumeDiscountCalculationStrategyTests()
        {
            _defualtProductVolumeDiscount = new ProductVolumeDiscountRule
            {
                ProductCode = "A",
                VolumeQuantity = 3,
                VolumePrice = 1.29M
            };
        }

        private Mock<IProductVolumeDiscountRulesRepository> _rulesRepoMock;
        private VolumeDiscountCalculationStrategy _strategy;

        [SetUp]
        public void Setup()
        {
            _rulesRepoMock = new Mock<IProductVolumeDiscountRulesRepository>();
            

            _rulesRepoMock.Setup(c => 
                c.FindByProductCode(_defualtProductVolumeDiscount.ProductCode))
                .Returns(_defualtProductVolumeDiscount);

            _strategy = new VolumeDiscountCalculationStrategy(_rulesRepoMock.Object);
        }

        public static IEnumerable<(string ProductCode, decimal UnitPrice, int Quantity, decimal ExpectedDiscount)> OrderItemCases()
        {
            yield return (ProductCode: _defualtProductVolumeDiscount.ProductCode, UnitPrice: 0.75M, Quantity: 5, ExpectedDiscount: 0.96M);
            yield return (ProductCode: _defualtProductVolumeDiscount.ProductCode, UnitPrice: 0.75M, Quantity: 2, ExpectedDiscount: 0M);
            yield return (ProductCode: "FAKE", UnitPrice: 0.75M, Quantity: 2, ExpectedDiscount: 0M);

        }

        [Test]
        [TestCaseSource(nameof(OrderItemCases))]
        public void CalculateDiscountTests((string ProductCode, decimal UnitPrice, int Quantity, decimal ExpectedDiscount) testCase)
        {
            //arrange
            //act
            var actualDiscount = _strategy.CalculateDiscount(testCase.ProductCode, testCase.UnitPrice, testCase.Quantity);

            //assert
            Assert.AreEqual(testCase.ExpectedDiscount, actualDiscount);
        }
    }
}
