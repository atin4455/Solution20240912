using BookStore.FrontEnd.Site.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pwd = "1234";
            string result=HashUtility.ToSHA256(pwd);
            Console.WriteLine(result);
            //A6654A5920422F9D417E4867EDC4FB840A1F3FFF1FA07E998E86F7F7A27AE3(pwd=123)
            //034C674216F3E15C761EB1A5E255F067953623C8B388B4459E13F978D7C846F4(pwd=1234)

        }
    }
}
