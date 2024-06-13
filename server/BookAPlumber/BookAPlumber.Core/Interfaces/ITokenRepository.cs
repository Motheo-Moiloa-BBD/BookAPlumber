using Microsoft.AspNetCore.Identity;

namespace BookAPlumber.Core.Interfaces
{
    public interface ITokenRepository
    {
        //Add methods that are specific to the Token entity
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
