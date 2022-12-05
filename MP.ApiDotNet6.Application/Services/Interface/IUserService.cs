using MP.ApiDotNet6.Application.DTOs.User;

namespace MP.ApiDotNet6.Application.Services.Interface
{
    public interface IUserService
    {
        Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO);
    }
}
