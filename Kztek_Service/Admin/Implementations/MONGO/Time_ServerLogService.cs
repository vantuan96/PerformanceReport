using Kztek_Core.Models;
using Kztek_Data.Repository.Mongo;
using Kztek_Library.Helpers;
using Kztek_Model.Models;
using Kztek_Service.Admin.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Service.Admin.Implementations.MONGO
{
    public class Time_ServerLogService : ITime_ServerLogService
    {
        private ITime_ServerLogRepository _Time_ServerLogRepository;

        public Time_ServerLogService(ITime_ServerLogRepository _Time_ServerLogRepository)
        {
            this._Time_ServerLogRepository = _Time_ServerLogRepository;
        }

        public async Task<MessageReport> Create(Time_ServerLog model)
        {
            return await _Time_ServerLogRepository.Add(model);
        }

        public async Task<MessageReport> Delete(string id)
        {
            var result = new MessageReport(false, "Có lỗi xảy ra");

            var obj = await GetById(id);
            if (obj != null)
            {
                var query = new StringBuilder();
                query.AppendLine("{");
                query.AppendLine("'_id': { '$eq': '" + id + "' }");
                query.AppendLine("}");

                return await _Time_ServerLogRepository.Remove(MongoHelper.ConvertQueryStringToDocument(query.ToString()));
            }
            else
            {
                result = new MessageReport(false, "Bản ghi không tồn tại");
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Time_ServerLog>> GetAll()
        {
            return await Task.FromResult(_Time_ServerLogRepository.Table);
        }

        public async Task<GridModel<Time_ServerLog>> GetAllPagingByFirst(string key, int pageNumber, int pageSize)
        {
            var query = new StringBuilder();
            query.AppendLine("{");

            if (!string.IsNullOrWhiteSpace(key))
            {
                query.AppendLine("'$or' : [ { 'OS' : { '$in' : [/" + key + "/i] } }, { 'Database' : { '$in' : [/" + key + "/i] } } ]");
            }

            query.AppendLine("}");

            var sort = new StringBuilder();
            sort.AppendLine("{");
            sort.AppendLine("'DateCreated': 1");
            sort.AppendLine("}");

            return await _Time_ServerLogRepository.GetPaging(MongoHelper.ConvertQueryStringToDocument(query.ToString()), MongoHelper.ConvertQueryStringToDocument(sort.ToString()), pageNumber, pageSize);
        }

        public async Task<Time_ServerLog> GetById(string id)
        {
            return await _Time_ServerLogRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(Time_ServerLog model)
        {
            var query = new StringBuilder();
            query.AppendLine("{");
            query.AppendLine("'_id': { '$eq': '" + model.Id + "' }");
            query.AppendLine("}");

            return await _Time_ServerLogRepository.Update(MongoHelper.ConvertQueryStringToDocument(query.ToString()), model);
        }


    }
}
