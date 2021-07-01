using AssignmentProject.BusinessLayer.Interfaces;
using AssignmentProject.Entities.Helpers;
using AssignmentProject.Entities.Interfaces;
using AssignmentProject.Entities.Models;
using System;
using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Implementations
{
    public class SafeExpressManager : RequestManager, IShippingChargesFetcher
    {
        private readonly ApiRequestData apiRequestData;
        public override IApiConfig ApiConfig { get; set; }

        public SafeExpressManager(ApiRequestData apiRequestData, ApiConfig apiConfig)
        {
            this.apiRequestData = apiRequestData;
            this.ApiConfig = apiConfig;
        }

        public override string GetApiRequestData()
        {
            var dataModel = new SafeExpressInputDataModel
            {
                Source = this.apiRequestData.SourceAddress,
                Destination = this.apiRequestData.DestinationAddress,
                Package = this.apiRequestData.Dimensions
            };

            var xmlObject = DataParser.ParseToXml(dataModel);
            return xmlObject;
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
                    result.ShippingCharges = Convert.ToDecimal(resultAsJson["quote"]);
                }
            });

            return result;
        }
    }
}
