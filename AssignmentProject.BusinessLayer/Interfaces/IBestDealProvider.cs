using AssignmentProject.Entities.Models;
using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Interfaces
{
    public interface IBestDealProvider
    {
        Task<ApiResponse> GetBestDeal(ApisRequestDetails apisRequestDetails);
    }
}
