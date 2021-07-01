using AssignmentProject.BusinessLayer.Implementations;
using AssignmentProject.Entities.Models;
using NUnit.Framework;
using System.Linq;

namespace AssignmentProject.Tests
{
    public class BestDealProviderTests
    {
        private BestDealProvider bestDealProvider;

        [SetUp]
        public void Setup()
        {
            bestDealProvider = new BestDealProvider();
        }

        [Test]
        public void GetDetailsForMinimumPriceTest_ShouldReturnDetailsForMinimumPrice()
        {
            var responses = BestDealProviderTestData.GetResponses();

            var expectedResponse = responses.OrderBy(c => c.ShippingCharges).FirstOrDefault();
            var actualResponse = BestDealProvider.GetDetailsForMinimumPrice(responses);

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TearDown]
        public void TearDown()
        {
            bestDealProvider = null;
        }
    }

    public static class BestDealProviderTestData
    {
        public static ApiResponse[] GetResponses()
        {
            return new ApiResponse[]
            {
                new ApiResponse
                {
                    CompanyName = "testCompany1111",
                    ShippingCharges = 1440,
                    Success = true
                },
                new ApiResponse
                {
                    CompanyName = "testCompany222222",
                    ShippingCharges = 999,
                    Success=true
                },
                new ApiResponse
                {
                    CompanyName = "testCompany333",
                    ShippingCharges = 1440,
                    Success = true
                }
            };
        }
    }
}
