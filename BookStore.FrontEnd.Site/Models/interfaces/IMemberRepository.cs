using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.FrontEnd.Site.Models.interfaces
{
    public interface IMemberRepository
    {
        void create(RegisterDto dto);
        bool IsAccountExist(string account);
    }
}
