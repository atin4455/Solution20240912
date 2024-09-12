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

            if (result.Success) 
            {
                ModelState.AddModelError(string.Empty,result.ErrorMessage);
                return View(vm);
            }
            return View("RegisterConfirm");//顯示RegisterConfirm網頁內容,不必有action
            //return RedirectToAction("RegisterConfirm");//如果想要轉到某action,就這麼寫
        }
    }
}