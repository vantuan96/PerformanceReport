using System;
using System.Threading.Tasks;
using Kztek_Core.Models;
using Kztek_Library.Models;
using Kztek_Model.Models;

namespace Kztek_Service.Api.Interfaces
{
    public interface IAuthService
    {
        Task<MessageReport> Login(AuthModel model);

        Task<SY_User> GetById(string id);

        Task<MessageReport> CheckValid(AuthModel model);

        Task<MessageReport> ResetPass(ResetPassModel model);

        Task<MessageReport> UpdateInfo(UserUpdateModel model);
    }
}
