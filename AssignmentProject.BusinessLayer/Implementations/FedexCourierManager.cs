using AssignmentProject.BusinessLayer.Interfaces;
using AssignmentProject.Entities.Helpers;
using AssignmentProject.Entities.Interfaces;
using AssignmentProject.Entities.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Implementations
{
    public class FedexCourierManager : RequestManager, IShippingChargesFetcher
    {
        private readonly ApiRequestData apiRequestData;
        public override IApiConfig ApiConfig { get; set; }

        public FedexCourierManager(ApiRequestData apiRequestData, ApiConfig apiConfig)
        {
            this.apiRequestData = apiRequestData;
            this.ApiConfig = apiConfig;
        }

        public override string GetApiRequestData()
        {
            dynamic jsonObject = new
            {
                Consignee = this.apiRequestData.SourceAddress,
                Consignor = this.apiRequestData.DestinationAddress,
                Carton = this.apiRequestData.Dimensions
            };

            return JsonConvert.SerializeObject(jsonObject);
        }

        public async Task<ApiResponse> GetShippingCharges()
        {
            var result = new ApiResponse
            {
                CompanyName = ApiConfig.UrlMapping.CompanyName
            };

            await MakeRequest(response =>
            {
                if (response != string.Empty)
                {
                    dynamic resultAsJson = DataParser.ParseToJsonObject(response);
                    result.Success = true;
                    result.ShippingCharges = Convert.ToDecimal(resultAsJson["total"]);
                }
            });

            return result;
        }
    }
}
