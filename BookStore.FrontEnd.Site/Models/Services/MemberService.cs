using BookStore.FrontEnd.Site.Models.Infra;
using BookStore.FrontEnd.Site.Models.interfaces;
using BookStore.FrontEnd.Site.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.FrontEnd.Site.Models.Services
{
    public class MemberService
    {
        private IMemberRepository _repo;


        public MemberService()
        {
            _repo = new MemberRepository();
        }

        public MemberService(IMemberRepository repo)
        {
            _repo = repo;
        }



        public void Register(RegisterDto dto)
        {
            //判斷是否帳號已存在
            bool isAccountExist = _repo.IsAccountExist(dto.Account);
            if (isAccountExist) throw new Exception("帳號已存在");

            //建立新會員
            //為密碼進行雜湊
            string salt = HashUtility.GetSalt();
            string hasPassword= HashUtility.ToSHA256(dto.Password, salt);
            //加入confirm code
            string confirmCode=Guid.NewGuid().ToString("N");
            //建立新會員
            dto.ConfirmCode=confirmCode;
            dto.EncryptedPassword = hasPassword;
            _repo.Create(dto);

            //todo寄送驗證信

        }


        public void ActiveRegister(int memberId, string confirmCode)
        {
            // 判斷 memberId 和 confirmCode 是否正確，正確就將會員狀態改為已啟用
            MemberDto dto = _repo.Get(memberId);
            if (dto == null) throw new Exception("會員不存在");
            if (dto.ConfirmCode != confirmCode) throw new Exception("驗證碼錯誤");
            if (dto.IsConfirmed.HasValue && dto.IsConfirmed.Value)
            {
                throw new Exception("會員已啟用");
            }

            // 啟用會員
            _repo.Active(memberId);
        }

    }
}