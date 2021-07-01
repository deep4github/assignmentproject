using AssignmentProject.Entities.Enums;
using AssignmentProject.Entities.Interfaces;

namespace AssignmentProject.Entities.Models
{
    public class ApiConfig : IApiConfig
    {
        public UrlMapping UrlMapping { get; set; }
        public ApiResponseType ApiResponseType { get; set; }
        public ApiCredentials ApiCredentials { get; set; }
    }
}
