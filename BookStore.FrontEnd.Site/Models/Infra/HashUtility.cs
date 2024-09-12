using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BookStore.FrontEnd.Site.Models.Infra
{
    public static class HashUtility
    {
        public static string GetSalt()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Salt"]; //去AppSetting取得salt
        }

        public static string ToSHA256(string plainText, string salt = null)
        {
            salt = salt ?? GetSalt(); // 若 salt 是 null 就取得 GetSalt() 的值

            using (var mySHA256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(plainText + salt); // 把密碼加上 salt 並轉成 byte[]
                var hash = mySHA256.ComputeHash(bytes);//進行SHA256加密(hash是加密過後的byte)

                var sb = new StringBuilder(); //用來存放加密後的字串
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("X2"));  //把byte轉成16進位字字串,字母是大寫
                }

                return sb.ToString(); // 回傳加密後的字串
            }
        }
    }
}
