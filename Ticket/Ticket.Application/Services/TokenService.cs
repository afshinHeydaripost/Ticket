using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ticket.Application.Interfaces;
using Ticket.Domain.Models;

namespace Ticket.Application.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly byte[] _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);
    }

    public GeneralResponse<string> GenerateAccessToken(User user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
        {
            new Claim("email", user.Email ?? string.Empty),
            new Claim("UserCode", user.FullName ?? string.Empty),
            new Claim("UserId", user.Id)
        };
            var roles = user.Role.Split(",").ToList();
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:AccessTokenExpirationMinutes"])),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);
            return GeneralResponse<string>.Success(userToken);
        }
        catch (Exception e)
        {
            return GeneralResponse<string>.Fail(e);
        }
    }
}
