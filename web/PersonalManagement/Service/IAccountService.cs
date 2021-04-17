using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Service
{
    public interface IAccountService
    {
        string CurrentUserId { get; }
        //bool IsAuthenticated { get; }
    }
}
