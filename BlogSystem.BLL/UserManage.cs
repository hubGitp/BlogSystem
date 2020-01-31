using BlogSystem.DAL;
using BlogSystem.Dto;
using BlogSystem.IBLL;
using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class UserManage : IUserManage
    {


        public bool Login(string email, string password,out Guid userId)
        {
            using (IUserService UserService = new UserService())
            {
                var user= UserService.GetAll().FirstOrDefaultAsync(predicate: m => m.Email == email && m.PassWord == password);
                user.Wait();
                var data = user.Result;
                if(data == null)
                {
                    userId = new Guid();
                    return false;
                }
                else
                {
                    userId = data.Id;
                    return true;
                }
            }
        }

        public async Task Register(string email, string password)
        {
            using (IUserService UserService = new UserService())
            {
                await UserService.CreateAsync(new User()
                {
                    Email = email,
                    PassWord = password,
                    SiteName = "默认的文字",
                    ImagePath = "default.png"
                });
            }
        }
        public async Task ChangePassword(string email, string oldPwd, string newPwd)
        {
            using (IUserService UserService = new UserService())
            {
                 if(await UserService.GetAll().AnyAsync(predicate: m => m.Email == email && m.PassWord == oldPwd))
                {
                   var user=  await UserService.GetAll().FirstAsync(m => m.Email == email);
                    user.PassWord = newPwd;
                    await UserService.EditAsync(user);

                }
            }
        }

        public async Task ChangeUserInformation(string email, string siteName, string imagePath)
        {
            using (IUserService UserService = new UserService())
            {
                    var user = await UserService.GetAll().FirstAsync(m => m.Email == email);
                    user.SiteName = siteName;
                    user.ImagePath = imagePath;
                    await UserService.EditAsync(user);
            }
        }

        public async Task<UserInformationDto> GetUserByEamil(string email)
        {
            using (IUserService UserService = new UserService())
            {
                if (await UserService.GetAll().AnyAsync(predicate: m => m.Email == email))
                {
                    return await UserService.GetAll().Where(m => m.Email == email).Select(m=>new UserInformationDto()
                    {
                        Id = m.Id,
                        Eamil = m.Email,
                        ImagePath = m.ImagePath,
                        SiteName = m.SiteName,
                        FocusCount = m.FocusCount,
                        FunsCount = m.FunsCount
                    }).FirstAsync();
                }
                else
                {
                   throw new ArgumentException(message:"邮箱地址不存在");
                }
            }
        }

     
    }
}
