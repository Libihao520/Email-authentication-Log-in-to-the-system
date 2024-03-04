using Model.Dto.User;

namespace Interface;

public interface ICustomJWTService
{
    string GetToken(UserRes user);
}