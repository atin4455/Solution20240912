using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.FrontEnd.Site.Models.interfaces
{
    public interface IMemberRepository
    {
        void Active(int memberId);
        void Create(RegisterDto dto);
        MemberDto Get(int memberId);
        bool IsAccountExist(string account);
    }
}
