namespace AssignmentProject.Entities.Models
{
    public class SafeExpressInputDataModel
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public decimal[] Package { get; set; }
    }
}
