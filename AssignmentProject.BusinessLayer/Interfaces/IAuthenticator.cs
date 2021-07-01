using System.Threading.Tasks;

namespace AssignmentProject.BusinessLayer.Interfaces
{
    public interface IAuthenticator
    {
        Task<bool> CheckAuthentication(string username, string passoword, string authenticationApiUrl);
    }
}
