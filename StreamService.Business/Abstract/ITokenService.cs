using StreamService.Entities.Concrete;

namespace StreamService.Business.Abstract
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(User user);
        void SaveRefreshToken(RefreshToken refreshToken);
        RefreshToken GetRefreshToken(string token);
        void RevokeRefreshToken(RefreshToken refreshToken);
    }
}
