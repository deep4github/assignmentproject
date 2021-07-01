using AssignmentProject.Entities.Enums;
using AssignmentProject.Entities.Models;

namespace AssignmentProject.Entities.Interfaces
{
    public interface IApiConfig
    {
        public UrlMapping UrlMapping { get; set; }
        public ApiResponseType ApiResponseType { get; set; }
        public ApiCredentials ApiCredentials { get; set; }
    }
}
