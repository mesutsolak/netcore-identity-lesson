using Identity.Core.Helpers.Abstract;
using Identity.Domain.Models.Token;
using Identity.Domain.Results;
using Identity.Domain.Results.Token;
using Identity.Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Identity.Core.Helpers.Concrete
{
    public sealed class TokenHelper : ITokenHelper
    {
        private readonly ITokenSetting _tokenSetting;
        private readonly ISecurityHelper _securityHelper;

        public TokenHelper(IServiceProvider serviceProvider)
        {
            _tokenSetting = serviceProvider.GetRequiredService<ITokenSetting>();
            _securityHelper = serviceProvider.GetRequiredService<ISecurityHelper>();
        }

        public AccessTokenResult CreateAccessToken(CreateAccessTokenModel createAccessTokenModel)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenSetting.AccessTokenExpiration);

            var securityKey = _securityHelper.GetSecurityKey(_tokenSetting.SecurityKey);

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(issuer: _tokenSetting.Issuer,
                audience: _tokenSetting.Audience, expires: accessTokenExpiration, notBefore: DateTime.Now,
                claims: GetClaims(createAccessTokenModel), signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            return new AccessTokenResult
            {
                Token = token,
                RefreshToken = CreateRefreshToken(),
                Expiration = accessTokenExpiration
            };
        }

        public void RevokeRefreshToken(RevokeRefreshToken revokeRefreshToken)
        {
            throw new NotImplementedException();
        }
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var mg = RandomNumberGenerator.Create();
            mg.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(CreateAccessTokenModel createAccessTokenModel)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,createAccessTokenModel.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,createAccessTokenModel.Email),
                new Claim(ClaimTypes.Name,createAccessTokenModel.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            return claims;
        }
    }
}
