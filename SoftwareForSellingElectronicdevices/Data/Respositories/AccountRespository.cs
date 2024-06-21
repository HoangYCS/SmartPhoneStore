using AutoMapper;
using Data.DTOs;
using Data.IRespositories;
using Data.ModelDbContext;
using Data.Models;
using Data.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories
{
    public class AccountRespository : IAccount
    {
        private readonly MyDBContext myDBContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountRespository(MyDBContext myDBContext, IMapper mapper, UserManager<User> userManager)
        {
            this.myDBContext = myDBContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PopularResponse<string>> LogInAsync(UserLoginDTO userLoginDTO)
        {
            try
            {
               var user = await myDBContext.Users
                    .FirstOrDefaultAsync(x=>x.UserName == userLoginDTO.UserName);
                if (user == null)
                {
                    return new PopularResponse<string>(false, "UserName not yet registered", default(string));
                }

                var result = (new PasswordHasher<User>())
                    .VerifyHashedPassword(user, user.PasswordHash!, userLoginDTO.Password);

                if(result == PasswordVerificationResult.Success)
                {
                    
                    return new PopularResponse<string>(true, "Login success", default(string));
                }
                else
                {
                    return new PopularResponse<string>(false, "Incorrect password", default(string));
                }
            }
            catch (Exception ex)
            {
                return new PopularResponse<string>(false, ex.ToString(), default(string));
            }
        }

        public async Task<PopularResponse<string>> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userRegisterDTO);
                user.PasswordHash = (new PasswordHasher<User>()).HashPassword(user,userRegisterDTO.Password);
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return new PopularResponse<string>(true, "Register Success", user.Id.ToString());
                }else
                {
                    return new PopularResponse<string>(false, string.Join(" ",result.Errors), null);
                }
            }
            catch (Exception ex)
            {
                return new PopularResponse<string>(false, ex.ToString(), null);
                throw;
            }
        }
    }
}
