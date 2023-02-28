
using Kztek_Core.Models;
using Kztek_Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kztek_Service.Admin.Interfaces
{
    public interface ICPURAM_ServerLogService
    {
        Task<MessageReport> Create(CPURAM_ServerLog model);
        Task<MessageReport> Delete(string id);
        Task<IEnumerable<CPURAM_ServerLog>> GetAll();
        Task<GridModel<CPURAM_ServerLog>> GetAllPagingByFirst(string key, int pageNumber, int pageSize);
        Task<CPURAM_ServerLog> GetById(string id);
        Task<MessageReport> Update(CPURAM_ServerLog model);
        Task<List<CPURAM_ServerLog_Custom>> GetList();
    }
}
