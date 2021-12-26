using Domain;

namespace Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string CreateToken(AppUser user);
    }
}