using AssignmentProject.BusinessLayer.Implementations;
using AssignmentProject.Entities.Helpers;
using System;
using System.Threading.Tasks;

namespace AssignmentProject.ApiConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bestDealProvider = new BestDealProvider();
            var apiRequestDetails = DataManager.GetApiRequestDetails();
            var bestDeal = await bestDealProvider.GetBestDeal(apiRequestDetails);
            Console.WriteLine(string.Format("{0} provides best deal for the price of INR {1}",
                bestDeal.CompanyName, bestDeal.ShippingCharges));
            Console.ReadKey();
        }
    }
}
