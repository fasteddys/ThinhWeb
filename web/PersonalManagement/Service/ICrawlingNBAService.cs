using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Service
{
    public interface ICrawlingNBAService
    {
        Task GetOddOverUnder();
        Task GetOddSpread();
        Task GetMatches(DateTime date);
    }
}
