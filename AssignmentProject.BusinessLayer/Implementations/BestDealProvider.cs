using AssignmentProject.BusinessLayer.Interfaces;
using AssignmentProject.Entities.Constants;
using AssignmentProject.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Implementations
{
    public class BestDealProvider: IBestDealProvider
    {
        public async Task<ApiResponse> GetBestDeal(ApisRequestDetails apisRequestDetails)
        {
            if (apisRequestDetails == null
                || apisRequestDetails.ApiRequestData == null
                || apisRequestDetails.ApiConfigs == null
                || apisRequestDetails.ApiConfigs.Count == 0)
            {
                throw new Exception("APIs request details are missing.");
            }

            var shippingServiceProviders = GetShippingChargesFetchers(apisRequestDetails);
            var requests = shippingServiceProviders.Select(x => x.GetShippingCharges());
            var responses = await Task.WhenAll(requests);

            return GetDetailsForMinimumPrice(responses);
        }

        public static ApiResponse GetDetailsForMinimumPrice(ApiResponse[] responses)
        {
            return responses.OrderBy(c => c.ShippingCharges).FirstOrDefault(c => c.Success);
        }

        private static IShippingChargesFetcher[] GetShippingChargesFetchers(ApisRequestDetails apisRequestDetails)
        {
            var shippingChargesFetchers = new IShippingChargesFetcher[apisRequestDetails.ApiConfigs.Count];
            var priceFetchers = GetPriceFetchers(apisRequestDetails);
            var counter = 0;

            foreach(var apiConfig in apisRequestDetails.ApiConfigs)
            {
                var priceFetcher = priceFetchers.Where(x => x.Key == apiConfig.UrlMapping.CompanyName).FirstOrDefault();
                shippingChargesFetchers[counter] = (IShippingChargesFetcher)priceFetcher.Value;
                counter++;
            }

            return shippingChargesFetchers;
        }

        private static Dictionary<string, RequestManager> GetPriceFetchers(ApisRequestDetails apisRequestDetails)
        {
            return new Dictionary<string, RequestManager>
            {
                {
                    Constants.EcomExpress,
                    new EcomExpressManager(apisRequestDetails.ApiRequestData,
                        apisRequestDetails.ApiConfigs.Where(x=>x.UrlMapping.CompanyName==Constants.EcomExpress).FirstOrDefault())
                },
                {
                    Constants.FedexCourier,
                    new FedexCourierManager(apisRequestDetails.ApiRequestData,
                        apisRequestDetails.ApiConfigs.Where(x=>x.UrlMapping.CompanyName==Constants.FedexCourier).FirstOrDefault())
                },
                {
                    Constants.SafeExpress,
                    new SafeExpressManager(apisRequestDetails.ApiRequestData,
                        apisRequestDetails.ApiConfigs.Where(x=>x.UrlMapping.CompanyName==Constants.SafeExpress).FirstOrDefault())
                }
            };
        }
    }
}
