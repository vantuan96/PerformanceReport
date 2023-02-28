using Kztek_Data.Infrastructure;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kztek_Data.Repository.Mongo
{
    public interface ICPURAM_ServerLogRepository : IMongoRepository<CPURAM_ServerLog>
    {
    }

    public class CPURAM_ServerLogRepository : MongoRepository<CPURAM_ServerLog>, ICPURAM_ServerLogRepository
    {

    }
}
