using Kztek_Core.Models;
using Kztek_Data.Repository.Mongo;
using Kztek_Library.Helpers;
using Kztek_Model.Models;
using Kztek_Service.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Service.Admin.Implementations.MONGO
{
    public class CPURAM_ServerLogService : ICPURAM_ServerLogService
    {
        private ICPURAM_ServerLogRepository _CPURAM_ServerLogRepository;

        public CPURAM_ServerLogService(ICPURAM_ServerLogRepository _CPURAM_ServerLogRepository)
        {
            this._CPURAM_ServerLogRepository = _CPURAM_ServerLogRepository;
        }

        public async Task<MessageReport> Create(CPURAM_ServerLog model)
        {
            return await _CPURAM_ServerLogRepository.Add(model);
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

                return await _CPURAM_ServerLogRepository.Remove(MongoHelper.ConvertQueryStringToDocument(query.ToString()));
            }
            else
            {
                result = new MessageReport(false, "Bản ghi không tồn tại");
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<CPURAM_ServerLog>> GetAll()
        {
            return await Task.FromResult(_CPURAM_ServerLogRepository.Table);
        }

        public async Task<GridModel<CPURAM_ServerLog>> GetAllPagingByFirst(string key, int pageNumber, int pageSize)
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

            return await _CPURAM_ServerLogRepository.GetPaging(MongoHelper.ConvertQueryStringToDocument(query.ToString()), MongoHelper.ConvertQueryStringToDocument(sort.ToString()), pageNumber, pageSize);
        }

        public async Task<CPURAM_ServerLog> GetById(string id)
        {
            return await _CPURAM_ServerLogRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(CPURAM_ServerLog model)
        {
            var query = new StringBuilder();
            query.AppendLine("{");
            query.AppendLine("'_id': { '$eq': '" + model.Id + "' }");
            query.AppendLine("}");

            return await _CPURAM_ServerLogRepository.Update(MongoHelper.ConvertQueryStringToDocument(query.ToString()), model);
        }
        public async Task<List<CPURAM_ServerLog_Custom>> GetList()
        {
            var list = new List<CPURAM_ServerLog_Custom>();

            var query = from n in _CPURAM_ServerLogRepository.Table       
                        select new CPURAM_ServerLog_Custom
                        {                 
                            Id = n.Id.ToString(),
                            RAM_Availability = n.RAM_Availability,
                            CPU_Usage = n.CPU_Usage,
                            Database = n.Database,
                            DateCreated = n.DateCreated,
                            OS = n.OS,
                            Type = SetTypeByDate(n.DateCreated)
                        };

            if(query != null && query.Count() > 0)
            {
                //foreach(var item in query)
                //{
                //    item.Type = SetTypeByDate(item.DateCreated);
                //}
                var a = query.ToList();
                list = query.GroupBy(n => new { n.Database, n.OS, n.Type })
                    .Select(n => new CPURAM_ServerLog_Custom{ 
                    Database = n.Key.Database,
                    OS = n.Key.OS,
                    Type = n.Key.Type,
                    AVGCPU = n.Average(c => c.CPU_Usage),
                    AVGRAM = n.Average(c => c.RAM_Availability)
                    }).ToList();
            }

            return await Task.FromResult(list);
        }

        string  SetTypeByDate(DateTime DateCreated)
        {
            string type = "";
            if (DateCreated >= strDate(DateCreated,"00") && DateCreated < strDate(DateCreated, "05"))
            {
                type = "0p - 5p";
            }
            else if (DateCreated >= strDate(DateCreated, "05") && DateCreated < strDate(DateCreated, "10"))
            {
                type = "5p - 10p";
            }
            else if (DateCreated >= strDate(DateCreated, "10") && DateCreated < strDate(DateCreated, "15"))
            {
                type = "10p - 15p";
            }
            else if (DateCreated >= strDate(DateCreated, "15") && DateCreated < strDate(DateCreated, "20"))
            {
                type = "15p - 20p";
            }
            else if (DateCreated >= strDate(DateCreated, "20") && DateCreated < strDate(DateCreated, "25"))
            {
                type = "20p - 25p";
            }
            else if (DateCreated >= strDate(DateCreated, "25") && DateCreated < strDate(DateCreated, "30"))
            {
                type = "25p - 30p";
            }
            else if (DateCreated >= strDate(DateCreated, "30") && DateCreated < strDate(DateCreated, "35"))
            {
                type = "30p - 35p";
            }
            else if (DateCreated >= strDate(DateCreated, "35") && DateCreated < strDate(DateCreated, "40"))
            {
                type = "35p - 40p";
            }
            else if (DateCreated >= strDate(DateCreated, "40") && DateCreated < strDate(DateCreated, "45"))
            {
                type = "40p - 45p";
            }
            else if (DateCreated >= strDate(DateCreated, "45") && DateCreated < strDate(DateCreated, "50"))
            {
                type = "45p - 50p";
            }
            else if (DateCreated >= strDate(DateCreated, "50") && DateCreated < strDate(DateCreated, "55"))
            {
                type = "50p - 55p";
            }
            else if (DateCreated >= strDate(DateCreated, "55") && DateCreated <= strDate(DateCreated, "60"))
            {
                type = "55p - 60p";

            }

            return type;
        }

        DateTime strDate(DateTime DateCreated,string minute)
        {
            var str = Convert.ToDateTime(DateCreated.ToString(string.Format("yyyy/MM/dd HH:{0}:00", minute)));

            return str;
        }
    }
}
