using AssignmentProject.Entities.Constants;
using AssignmentProject.Entities.Enums;
using AssignmentProject.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Implementations
{
    public abstract class RequestManager
    {
        private readonly HttpClient httpClient;
        public abstract IApiConfig ApiConfig { get; set; }
        public abstract string GetApiRequestData();

        protected RequestManager()
        {
            this.httpClient = new HttpClient();
        }

        protected async Task MakeRequest(Action<string> onSuccess)
        {
            var response = await InitiateRequest();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject(responseContent);
                onSuccess?.Invoke(responseContent);
            }
        }

        private async Task<HttpResponseMessage> InitiateRequest()
        {
            var mediaType = GetMediaType();
            var apiRequestData = GetApiRequestData();

            httpClient.BaseAddress = new Uri(ApiConfig.UrlMapping.ApiUrl);
            httpClient.DefaultRequestHeaders.Add("username", ApiConfig.ApiCredentials.Username);
            httpClient.DefaultRequestHeaders.Add("password", ApiConfig.ApiCredentials.Password);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (mediaType == Constants.MediaTypeJson)
            {
                var stringContent = new StringContent(apiRequestData, Encoding.UTF8, mediaType);
                return await this.httpClient.PostAsync(new Uri(ApiConfig.UrlMapping.ApiUrl), stringContent);
            }

            if (mediaType == Constants.MediaTypeXml)
            {
                var xmlAsString = GetXmlNodeAsString(apiRequestData);
                var content = new StringContent(xmlAsString, Encoding.UTF8, Constants.MediaTypeTextXml);
                return await this.httpClient.PostAsync(ApiConfig.UrlMapping.ApiUrl, content);
            }

            throw new Exception(string.Format("{0} media type not supported", mediaType));
        }

        private string GetMediaType()
        {
            return ApiConfig.ApiResponseType == ApiResponseType.Json
                ? Constants.MediaTypeJson
                : Constants.MediaTypeXml;
        }

        private string GetXmlNodeAsString(string apiRequestData)
        {
            // This will be dynamically generated from input data.
            return @"<SafeExpressInputDataModel><Source><AddLine1>#111, Sector 36B, Chandigarh</AddLine1></Source><Destination><AddLine1>#333, Sector 20, Noida</AddLine1></Destination><Package><decimal>20</decimal><decimal>5.5</decimal><decimal>5</decimal></Package></SafeExpressInputDataModel>";
        }
    }
}
