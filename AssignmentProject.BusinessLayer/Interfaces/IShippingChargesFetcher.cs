using AssignmentProject.Entities.Models;
using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Interfaces
{
    public interface IShippingChargesFetcher
    {
        Task<ApiResponse> GetShippingCharges();
    }
}
