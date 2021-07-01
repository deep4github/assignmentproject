using AssignmentProject.BusinessLayer.Interfaces;
using AssignmentProject.Entities.Helpers;
using AssignmentProject.Entities.Interfaces;
using AssignmentProject.Entities.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Implementations
{
    public class EcomExpressManager : RequestManager, IShippingChargesFetcher
    {
        private readonly ApiRequestData apiRequestData;
        public override IApiConfig ApiConfig { get; set; }

        public EcomExpressManager(ApiRequestData apiRequestData, ApiConfig apiConfig)
        {
            this.apiRequestData = apiRequestData;
            this.ApiConfig = apiConfig;
        }

        public override string GetApiRequestData()
        {
            dynamic jsonObject = new JObject();
            jsonObject.ContactAddress = JObject.FromObject(this.apiRequestData.SourceAddress);
            jsonObject.WarehouseAddress = JObject.FromObject(this.apiRequestData.DestinationAddress);
            jsonObject.PackageDimensions = new JArray(this.apiRequestData.Dimensions);
            return jsonObject.ToString();
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
                    result.ShippingCharges = Convert.ToDecimal(resultAsJson.amount);
                }
            });

            return result;
        }
    }
}
