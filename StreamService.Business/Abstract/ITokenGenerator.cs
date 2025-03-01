using StreamService.Entities.Concrete;

namespace StreamService.Business.Abstract;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}
