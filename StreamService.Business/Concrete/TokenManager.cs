using System;
using System.Linq;
using StreamService.Business.Abstract;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.Business.Concrete
{
    public class TokenManager : ITokenService
    {
        private readonly IRefreshTokenDal _refreshTokenDal;
        private readonly ITokenGenerator _tokenGenerator;

        public TokenManager(IRefreshTokenDal refreshTokenDal, ITokenGenerator tokenGenerator)
        {
            _refreshTokenDal = refreshTokenDal;
            _tokenGenerator = tokenGenerator;
        }

        public string GenerateAccessToken(User user)
        {
            // Use tokenGenerator instead of hardcoded value
            return _tokenGenerator.GenerateToken(user);
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };

            return refreshToken;
        }

        public void SaveRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokenDal.CreateAsync(refreshToken);
        }

        public RefreshToken GetRefreshToken(string token)
        {
            return _refreshTokenDal.GetByToken(token);
        }

        public void RevokeRefreshToken(RefreshToken refreshToken)
        {
            refreshToken.Revoked = DateTime.UtcNow;
            _refreshTokenDal.UpdateAsync(refreshToken);
        }
    }
}
