
using Kztek_Core.Models;
using Kztek_Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kztek_Service.Admin.Interfaces
{
    public interface ITime_ServerLogService
    {
        Task<MessageReport> Create(Time_ServerLog model);
        Task<MessageReport> Delete(string id);
        Task<IEnumerable<Time_ServerLog>> GetAll();
        Task<GridModel<Time_ServerLog>> GetAllPagingByFirst(string key, int pageNumber, int pageSize);
        Task<Time_ServerLog> GetById(string id);
        Task<MessageReport> Update(Time_ServerLog model);
    }
}
