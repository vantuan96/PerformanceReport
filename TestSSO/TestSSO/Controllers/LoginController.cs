using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestSSO.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult Login(bool isError = false)
        //{

        //    #region Set Cookie Current App
        //    //var cookie = Request.Cookies[ConstantKey.CurrentApp] ?? new HttpCookie(ConstantKey.CurrentApp);
        //    //var routeValueDic = new RouteValueDictionary(System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values);
        //    //string appid = AppUtils.AppIdDefault;
        //    //if (System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["appid"] != null)
        //    //{
        //    //    int.TryParse(System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["appid"].ToString(), out appId);
        //    //}
        //    //else if (cookie != null && cookie.Value != null)
        //    //{
        //    //    int.TryParse(cookie.Value, out appId);
        //    //}
        //    //cookie.Value = appId.ToString();
        //    //cookie.Expires = DateTime.Now.AddMonths(AppUtils.ExpireDays);
        //    //System.Web.HttpContext.Current.Response.SetCookie(cookie);
        //    //ViewBag.CurrentAPP = systemSettingCaching.CurrentApp();
        //    #endregion .Set Cookie Current App

        //    if (VinAuthentication.IsLogon(HttpContext))
        //    {
        //        var encryptedTicket = HttpContext.Request.Cookies[ConstantKey.SignInToken].Value;
        //        try
        //        {
        //            var decryptedTicket = FormsAuthentication.Decrypt(encryptedTicket).UserData;
        //            var loginInfo = JsonConvert.DeserializeObject<UserContract>(decryptedTicket);
        //            if (loginInfo.UserId > 0)
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                return View();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.LoginFailed = Session["LoginFailed"] == null ? 0 : int.Parse(Session["LoginFailed"].ToString());

        //        if (TempData["LoginError"] != null && TempData["ModelState"] != null)
        //        {
        //            var loginModel = TempData["LoginError"] as LoginModel;
        //            ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);
        //            return View(loginModel);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //}
    }
}