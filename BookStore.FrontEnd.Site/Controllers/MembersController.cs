using BookStore.FrontEnd.Site.Models;
using BookStore.FrontEnd.Site.Models.Services;
using BookStore.FrontEnd.Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.FrontEnd.Site.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Register()
        {
            return View();
        }

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