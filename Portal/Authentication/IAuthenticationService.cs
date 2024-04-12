using Portal.Models;
using System.Threading.Tasks;

namespace Portal.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task LogOut();
    }
}