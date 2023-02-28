using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kztek_Core.Models;
using Kztek_Library.Configs;
using Kztek_Library.Models;
using Kztek_Model.Models;
using Kztek_Service.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kztek_Web.Apis.Mobile
{
    [Authorize(Policy = ApiConfig.Auth_Bearer_Mobile)]
    [Route("api/mobile/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _AuthService;

        public AuthController(IAuthService _AuthService)
        {
            this._AuthService = _AuthService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<MessageReport> login(AuthModel model)
        {
            return await _AuthService.Login(model);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<SY_User> getbyid(string id)
        {
            return await _AuthService.GetById(id);
        }

        [HttpPost("checkvalid")]
        public async Task<MessageReport> checkvalid(AuthModel model)
        {
            return await _AuthService.CheckValid(model);
        }

        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<MessageReport> reset(ResetPassModel model)
        {
            return await _AuthService.ResetPass(model);
        }

        [HttpPost("updateinfo")]
        public async Task<MessageReport> updateinfo([FromForm] UserUpdateModel model)
        {
            return await _AuthService.UpdateInfo(model);
        }
    }
}
