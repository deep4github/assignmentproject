using AssignmentProject.Entities.Enums;
using AssignmentProject.Entities.Models;
using System.Collections.Generic;

namespace AssignmentProject.Entities.Helpers
{
    public static class DataManager
    {
        private static readonly string sourceAddress = "#111, Sector 36B, Chandigarh";
        private static readonly string destinationAddress = "#333, Sector 20, Noida";
        private static readonly decimal[] packageDimensions = new decimal[] { 20, 5.5m, 5 };

        public static ApisRequestDetails GetApiRequestDetails()
        {
            return new ApisRequestDetails
            {
                ApiRequestData = new ApiRequestData
                {
                    SourceAddress = new Address
                    {
                        AddLine1 = sourceAddress
                    },
                    DestinationAddress = new Address
                    {
                        AddLine1 = destinationAddress
                    },
                    Dimensions = packageDimensions
                },
                ApiConfigs = new List<ApiConfig>
                {
                    new ApiConfig
                    {
                        ApiCredentials = new ApiCredentials
                        {
                            Username = "Test_EcomExpress_Username",
                            Password = "Test_EcomExpress_Password"
                        },
                        ApiResponseType = ApiResponseType.Json,
                        UrlMapping = new UrlMapping
                        {
                            CompanyName = Constants.Constants.EcomExpress,
                            ApiUrl = "https://localhost:44366/api/EcomExpress"
                        }
                    },
                    new ApiConfig
                    {
                        ApiCredentials = new ApiCredentials
                        {
                            Username = "Test_FedexCourier_Username",
                            Password = "Test_FedexCourier_Password"
                        },
                        ApiResponseType = ApiResponseType.Json,
                        UrlMapping = new UrlMapping
                        {
                            CompanyName = Constants.Constants.FedexCourier,
                            ApiUrl = "https://localhost:44366/api/FedexCourier"
                        }
                    },
                    new ApiConfig
                    {
                        ApiCredentials = new ApiCredentials
                        {
                            Username = "Test_SafeExpress_Username",
                            Password = "Test_SafeExpress_Password"
                        },
                        ApiResponseType = ApiResponseType.Xml,
                        UrlMapping = new UrlMapping
                        {
                            CompanyName = Constants.Constants.SafeExpress,
                            ApiUrl = "https://localhost:44366/api/SafeExpress"
                        }
                    }
                }
            };
        }
    }
}
