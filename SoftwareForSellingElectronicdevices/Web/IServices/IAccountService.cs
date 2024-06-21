using Data.DTOs;
using Data.Responses;

namespace Web.IServices
{
    public interface IAccountService
    {
        Task<PopularResponse<string>> RegisterAsync(UserRegisterDTO userDTO);
        Task<PopularResponse<string>> LogInAsync(UserLoginDTO user);
    }
}
