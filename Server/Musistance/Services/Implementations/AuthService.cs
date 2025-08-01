using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Musistance.Data.Contexts;
using Musistance.Data.Models.Users;
using Musistance.Dto.Auth;
using Musistance.Services.Interfaces;
using Musistance.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Musistance.Services.Implementations
{
    internal class AuthService: IAuthService
    {
        private readonly JwtSettings _config;
        private readonly MusistanceDbContext _db;

        public AuthService (JwtSettings jwtSettings, MusistanceDbContext context)
        {
            _config = jwtSettings;
            _db = context;
        }

        private string GenerateRandomString (int length)
        {
            Random rng = new Random((int)DateTime.Now.Ticks);
            byte[] randomisable = new byte[length];
            rng.NextBytes(randomisable);
            return Convert.ToBase64String(randomisable);
        }

        private string HashData (string data, string salt)
        {
            string hashable = data + salt;
            byte[] hashableBytes = Encoding.UTF8.GetBytes(hashable);
            for (int i = 0; i < 20; i++)
            {
                hashableBytes = SHA256.HashData(hashableBytes);
            }
            return Convert.ToBase64String(hashableBytes);
        }

        private string GenerateJwt (UserProfile user)
        {
            IList<Claim> claims = new List<Claim>()
            {
                new Claim("uid",user.Id.ToString()),
                new Claim("dsn",user.Name),
                new Claim("src",user.Source switch
                {
                    0 => "Itch",
                    _ => "Unknown"
                }),
                new Claim("sri",user.SourceId.HasValue ? user.SourceId.Value.ToString() : "0")
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_config.LifetimeInHours ?? 24),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Signature)),SecurityAlgorithms.HmacSha512Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> CreateEmptyAccountAsync ()
        {
            UserProfile up = new UserProfile()
            {
                ProfilePicture = [],
                LatestValidationSalt = String.Empty,
                LatestValidatonCode = String.Empty,
                Name = String.Empty
            };
            _db.Profiles.Add(up);
            await _db.SaveChangesAsync();
            return up.Id;
        }

        public async Task<string> GenerateValidationCodeAsync (int userId)
        {
            UserProfile up = await _db.Profiles.Where(p => p.Id == userId).SingleAsync();
            string code = GenerateRandomString(24);
            string salt = GenerateRandomString(16);
            up.LatestValidationSalt = salt;
            up.LatestValidatonCode = HashData(code,salt);
            _db.Profiles.Update(up);
            await _db.SaveChangesAsync();
            return code;
        }

        public async Task<AuthSessionDto> UseValidationCodeAsync (int userId, string validationCode)
        {
            UserProfile up = await _db.Profiles.Where(p => p.Id == userId).Include(p => p.Parent).SingleAsync();
            if (up.LatestValidatonCode != HashData(validationCode,up.LatestValidationSalt))
            {
                throw new Exception("401");
            }
            while (up.Parent != null)
            {
                up = await _db.Profiles.Where(p => p.Id == up.ParentId).Include(p => p.Parent).SingleAsync();
            }
            return new AuthSessionDto()
            {
                UserId = up.Id,
                AccessToken = GenerateJwt(up),
                NextValidationCode = await GenerateValidationCodeAsync(userId)
            };
        }
    }
}
