using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kztek_Core.Models;
using Kztek_Data.Repository.Mongo;
using Kztek_Library.Helpers;
using Kztek_Library.Models;
using Kztek_Library.Security;
using Kztek_Model.Models;
using Kztek_Service.Admin.Interfaces;
using X.PagedList;

namespace Kztek_Service.Admin.Implementations.MONGO
{
    public class SY_UserService : ISY_UserService
    {
        private ISY_UserRepository _SY_UserRepository;
        private ISY_Map_User_RoleRepository _SY_Map_User_RoleRepository;

        public SY_UserService(ISY_UserRepository _SY_UserRepository, ISY_Map_User_RoleRepository _SY_Map_User_RoleRepository)
        {
            this._SY_UserRepository = _SY_UserRepository;
            this._SY_Map_User_RoleRepository = _SY_Map_User_RoleRepository;
        }

        public async Task<List<SY_User>> GetAll()
        {
            var data = _SY_UserRepository.Table;
            return await Task.FromResult(data.ToList());
        }

        public async Task<GridModel<SY_User>> GetPaging(string key, int pageNumber, int pageSize)
        {
            var query = new StringBuilder();
            query.AppendLine("{");

            if (!string.IsNullOrWhiteSpace(key))
            {
                query.AppendLine("'$or': [");

                query.AppendLine("{ 'Name': { '$in': [/" + key + "/i] } }");
                query.AppendLine(", { 'Username': { '$in': [/" + key + "/i] } }");

                query.AppendLine("]");
            }

            query.AppendLine("}");

            var sort = new StringBuilder();
            sort.AppendLine("{");
            sort.AppendLine("'Username': 1");
            sort.AppendLine("}");

            return await _SY_UserRepository.GetPaging(MongoHelper.ConvertQueryStringToDocument(query.ToString()), MongoHelper.ConvertQueryStringToDocument(sort.ToString()), pageNumber, pageSize);
        }

        public async Task<SY_User> GetById(string id)
        {
            return await _SY_UserRepository.GetOneById(id);
        }

        public async Task<SY_User> GetByUsername(string username)
        {
            var query = from n in _SY_UserRepository.Table
                        where n.Username == username
                        select n;

            return await Task.FromResult(query.FirstOrDefault());
        }

        public async Task<SY_User> GetByUsername_notId(string username, string id)
        {
            var query = from n in _SY_UserRepository.Table
                        where n.Username == username && n.Id != id
                        select n;

            return await Task.FromResult(query.FirstOrDefault());
        }

        public async Task<SY_User_Submit> GetCustomById(string id)
        {
            var model = new SY_User_Submit();

            var obj = await _SY_UserRepository.GetOneById(id);
            if (obj != null)
            {
                model = await GetCustomByModel(obj);
            }

            return model;
        }

        public async Task<SY_User_Submit> GetCustomByModel(SY_User model)
        {
            var obj = new SY_User_Submit()
            {
                Id = model.Id,
                Active = model.Active,
                Name = model.Name,
                Roles = new List<string>(),
                Username = model.Username,
                isAdmin = model.isAdmin,
                Avatar = model.Avatar
            };

            obj.Roles = (from n in _SY_Map_User_RoleRepository.Table
                         where n.UserId == model.Id
                         select n.RoleId).ToList();

            return await Task.FromResult(obj);
        }

        public async Task<MessageReport> Create(SY_User model)
        {
            return await _SY_UserRepository.Add(model);
        }

        public async Task<MessageReport> Update(SY_User model)
        {
            var query = new StringBuilder();
            query.AppendLine("{");
            query.AppendLine("'_id': { '$eq': '" + model.Id + "' }");
            query.AppendLine("}");

            return await _SY_UserRepository.Update(MongoHelper.ConvertQueryStringToDocument(query.ToString()), model);
        }

        public async Task<MessageReport> Delete(string id)
        {
            var result = new MessageReport(false, "C?? l???i x???y ra");

            var obj = await GetById(id);
            if (obj != null)
            {
                var query = new StringBuilder();
                query.AppendLine("{");
                query.AppendLine("'_id': { '$eq': '" + id + "' }");
                query.AppendLine("}");

                return await _SY_UserRepository.Remove(MongoHelper.ConvertQueryStringToDocument(query.ToString()));
            }
            else
            {
                result = new MessageReport(false, "B???n ghi kh??ng t???n t???i");
            }

            return await Task.FromResult(result);
        }

        public Task<MessageReport> Login(AuthModel model, out SY_User user)
        {
            var result = new MessageReport(false, "C?? l???i x???y ra");

            try
            {
                //Ki???m tra username
                var objUser = GetByUsername(model.Username).Result;
                if (objUser == null)
                {
                    user = null;
                    result = new MessageReport(false, "T??i kho???n kh??ng t???n t???i");
                    return Task.FromResult(result);
                }

                if (objUser.Active == false)
                {
                    user = null;
                    result = new MessageReport(false, "T??i kho???n b??? kh??a");
                    return Task.FromResult(result);
                }

                //Gi???i m??
                var pass = CryptoHelper.DecryptPass_User(objUser.Password, objUser.PasswordSalat);

                //Check m???t kh???u
                if (pass != model.Password)
                {
                    user = null;
                    result = new MessageReport(false, "M???t kh???u kh??ng kh???p");
                    return Task.FromResult(result);
                }

                //G??n l???i user
                user = objUser;
                result = new MessageReport(true, "????ng nh???p th??nh c??ng");
            }
            catch (System.Exception ex)
            {
                user = null;
                result = new MessageReport(false, ex.Message);
            }

            return Task.FromResult(result);
        }


    }
}