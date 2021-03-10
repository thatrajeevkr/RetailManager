using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.API
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
        Task RemoveUserFromRole(string userId, string roleName);
        Task AdduserToRole(string userId, string roleName);
        Task<Dictionary<string, string>> GetAllRoles();
    }
}