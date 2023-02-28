using Kztek_Data.Infrastructure;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kztek_Data.Repository.Mongo
{
    public interface ITime_ServerLogRepository : IMongoRepository<Time_ServerLog>
    {
    }

    public class Time_ServerLogRepository : MongoRepository<Time_ServerLog>, ITime_ServerLogRepository
    {

    }
}
