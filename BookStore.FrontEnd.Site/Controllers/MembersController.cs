﻿using BookStore.FrontEnd.Site.Models;
using BookStore.FrontEnd.Site.Models.Services;
using BookStore.FrontEnd.Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookStore.FrontEnd.Site.Controllers
{
    public class MembersController : Controller
    {
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

        public ActionResult Login(LoginVM vm)
        {
            return View();
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