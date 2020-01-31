using BlogSystem.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IUserManage
    {
        Task Register(string email, string password);
        bool Login(string email, string password,out Guid userId);
        Task ChangePassword(string email, string oldPwd, string newPwd);
        Task ChangeUserInformation(string email, string siteName, string imagePath);
        Task<UserInformationDto> GetUserByEamil(string email);
    }
}
