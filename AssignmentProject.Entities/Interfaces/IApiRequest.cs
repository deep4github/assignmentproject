using AssignmentProject.Entities.Models;

namespace AssignmentProject.Entities.Interfaces
{
    public interface IApiRequest
    {
        Address SourceAddress { get; set; }
        Address DestinationAddress { get; set; }
        decimal[] Dimensions { get; set; }
    }
}
