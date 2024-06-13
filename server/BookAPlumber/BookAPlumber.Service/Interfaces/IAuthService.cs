using BookAPlumber.Core.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace BookAPlumber.Service.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityUser> RegisterUser(RegisterDTO registerDTO);
        Task<LoginResponseDTO> LoginUser(LoginDTO loginDTO);
    }
}
