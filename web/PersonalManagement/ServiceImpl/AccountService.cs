using Microsoft.AspNetCore.Http;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonalManagement.ServiceImpl
{
    public class AccountService : IAccountService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private HttpContext Current => _httpContextAccessor?.HttpContext;

        public AccountService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //public bool IsAuthenticated => Current?.User != null ? Current.User. : false;

        public string CurrentUserId
        {
            get
            {
                var user = Current?.User;
                if (user != null)
                {
                    // ASP.NET Core user Id.
                    var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim != null)
                    {
                        //var id = userIdClaim.Value;
                        //if (!string.IsNullOrWhiteSpace(id))
                        //{
                        //    if (id.Length > Lengths.CreatedUpdatedBy)
                        //    {
                        //        return id.Substring(0, Lengths.CreatedUpdatedBy);
                        //    }
                        //    return id;
                        //}
                        return userIdClaim.Value;
                    }
                }
                return "SYSTEM";
            }
        }

    }
}
