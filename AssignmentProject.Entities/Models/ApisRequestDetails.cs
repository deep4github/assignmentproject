using System.Collections.Generic;

namespace AssignmentProject.Entities.Models
{
    public class ApisRequestDetails
    {
        public ApiRequestData ApiRequestData { get; set; }
        public List<ApiConfig> ApiConfigs { get; set; }
    }
}
