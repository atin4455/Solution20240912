﻿using BookStore.FrontEnd.Site.Models;
using BookStore.FrontEnd.Site.Models.Dtos;
using BookStore.FrontEnd.Site.Models.Repository;
using BookStore.FrontEnd.Site.Models.Services;
using BookStore.FrontEnd.Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BookStore.FrontEnd.Site.Controllers
{
    public class MembersController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        // GET: Members
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Result result=HandleRegister(vm); //叫用副程式進行建立新會員的工作,並回傳結果(成功或失敗,失敗訊息)

            if (result.IsSuccess) 
            {
                ModelState.AddModelError(string.Empty,result.ErrorMessage);
                return View(vm);
            }
            return View("RegisterConfirm");//顯示RegisterConfirm網頁內容,不必有action
            //return RedirectToAction("RegisterConfirm");//如果想要轉到某action,就這麼寫
        }

        public ActionResult ActiveRegister(int memberId,string confirmCode)
        {
            Result result = HandleActiveRegister(memberId, confirmCode);

            //if (result.IsSuccess) 
            //{
            //    ModelState.AddModelError(string.Empty, result.ErrorMessage);
            //    return View();
            //}
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                Result result = HandleLogin(vm);
                if (result.IsSuccess)
                {
                    (string url, HttpCookie cookie) = ProcessLogin(vm.Account);

                    Response.Cookies.Add(cookie);

                    return Redirect(url);
                }
                ModelState.AddModelError(
                string.Empty,
                result.ErrorMessage);


            }
            return View();

        }

        [Authorize]
        public ActionResult EditProfile()
        {
            var account = User.Identity.Name;
            MemberDto dto = new MemberRepository().Get(account);
            EditProfileVm vm = WebApiApplication._mapper.Map<EditProfileVm>(dto);
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(EditProfileVm vm)
        {
            string account = User.Identity.Name;
            Result result = HandleUnknownAction(account, vm);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");//更新成功,回會員中心頁
            }
            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(vm);
        }

        private Result HandleUnknownAction(string account, EditProfileVm vm)
        {
            var service = new MemberService();
            try
            {
                EditProfileDto dto = WebApiApplication._mapper.Map<EditProfileDto>(vm);
                service.UpdateProfile(dto);

                return Result.Success();
            }
            catch (Exception ex) 
            {
                return Result.Fail(ex.Message);
            }
        }

        private (string url, HttpCookie cookie) ProcessLogin(string account)
        {
            var roles = string.Empty; // 角色欄位，沒有用到角色權限，所以存入空白

            // 建立一張認證憑證
            var ticket = new FormsAuthenticationTicket(
                1,                              // 版本別，沒有特別用途
                account,                        // 使用者帳號
                DateTime.Now,                   // 發行日
                DateTime.Now.AddDays(2),        // 到期日
                false,                          // 是否記住
                roles,                          // 使用者資料
                "/"                             // cookie 位置
            );

            // 將它加密
            var value = FormsAuthentication.Encrypt(ticket);

            // 存入 cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

            // 取得 return url
            var url = FormsAuthentication.GetRedirectUrl(account, true); // 第二個引數沒有用途

            return (url, cookie);

        }


        private Result HandleLogin(LoginVM vm)
        {
            try
            {
                var service = new MemberService();

                LoginDto dto = WebApiApplication._mapper.Map<LoginDto>(vm); //automapper

                Result validateResult = service.ValidateLogin(dto);

                return validateResult;

            }
            catch (Exception ex) 
            {
                return Result.Fail(ex.Message);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login","Members");

        }



        private Result HandleActiveRegister(int memberId, string confirmCode)
        {
            try
            {
                var service = new MemberService();
                service.ActiveRegister(memberId, confirmCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }


        }



        private Result HandleRegister(RegisterVm vm)
        {
            // 在這裡，可以自行決定要叫用 EF or Service object 進行 create member 的工作
            MemberService service = new MemberService();

            try
            {
                RegisterDto dto = vm.ToDto();
                service.Register(dto);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}