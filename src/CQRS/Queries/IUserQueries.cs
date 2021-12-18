using CQRS.Dto.In.User;
using CQRS.Dto.Out.UserDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Queries
{
    public interface IUserQueries
    {
        Task<GetUserInforOutDto> GetUserInforByEmail(string email);
        Task<GetUserInforOutDto> GetUserInforById(string id);
        Task<GetUsersOutDto> GetUsers(GetUsersInDto getUsersInfor);
    }
}
