using BookAPlumber.Core.Exceptions;
using BookAPlumber.Core.Interfaces;
using BookAPlumber.Core.Models.DTO;
using BookAPlumber.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookAPlumber.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public AuthService(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IdentityUser> RegisterUser(RegisterDTO registerDTO)
        {
            //TODO check if email is already existing
            var existingEmail = await userManager.FindByEmailAsync(registerDTO.Username);

            if (existingEmail != null)
            {
                throw new DuplicateException($"The username already exists.");
            }

            var identityUser = new IdentityUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerDTO.Password);

            if (identityResult.Succeeded) 
            {
                //Add roles to this user
                if (registerDTO.Roles != null) 
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerDTO.Roles);

                    if(identityResult.Succeeded)
                    {
                        return identityUser;
                    }
                }
            }

            throw new BadRequestException("There was a problem when registering the user");
        }
        public async Task<LoginResponseDTO> LoginUser(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Username);

            if(user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginDTO.Password);

                if (checkPasswordResult)
                {
                    //Get roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        //Create token
                        var token = unitOfWork.Tokens.CreateToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = token,
                        };

                        return response;
                    }
                }
                else
                {
                    throw new BadRequestException("Username or password incorrect.");
                }
            }
            throw new BadRequestException("Username or password incorrect.");
        }
    }
}
