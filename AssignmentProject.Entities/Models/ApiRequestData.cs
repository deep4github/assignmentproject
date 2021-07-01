using AssignmentProject.Entities.Interfaces;

namespace AssignmentProject.Entities.Models
{
    public class ApiRequestData : IApiRequest
    {
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public decimal[] Dimensions { get; set; }
    }
}
