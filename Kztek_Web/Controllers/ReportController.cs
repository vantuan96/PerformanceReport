using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kztek_Service.Admin.Interfaces;
using Kztek_Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Kztek_Web.Controllers
{
    public class ReportController : Controller
    {
        private ICPURAM_ServerLogService _CPURAM_ServerLogService;
        private ITime_ServerLogService _Time_ServerLogService;

        public ReportController(ICPURAM_ServerLogService _CPURAM_ServerLogService, ITime_ServerLogService _Time_ServerLogService)
        {
            this._CPURAM_ServerLogService = _CPURAM_ServerLogService;
            this._Time_ServerLogService = _Time_ServerLogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [CheckSessionCookie]
        public async Task<IActionResult> ChartCPURAM()
        {
            var list = await _CPURAM_ServerLogService.GetList();


            return View(list);
        }

        [CheckSessionCookie]
        public async Task<IActionResult> ChartTime()
        {
            var list = await _Time_ServerLogService.GetAll();


            return View(list);
        }
    }
}
