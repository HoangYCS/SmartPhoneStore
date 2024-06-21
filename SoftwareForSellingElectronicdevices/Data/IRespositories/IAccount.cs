using Data.DTOs;
using Data.Models;
using Data.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRespositories
{
    public interface IAccount
    {
        Task<PopularResponse<string>> RegisterAsync(UserRegisterDTO userDTO);
        Task<PopularResponse<string>> LogInAsync(UserLoginDTO user);
    }
}
