using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Entities.Users;
using TestingSystem.Service.DTOs.Users;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Extensions;
using TestingSystem.Service.Helpers;
using TestingSystem.Service.Interfaces;

namespace StarBucks.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IConfiguration configuration;

        public AuthService(IGenericRepository<User> userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }
        public async ValueTask<string> GenerateToken(string username, string password)
        {
            User user = await userRepository.GetAsync(u =>
                u.Username == username && u.Password.Equals(password.Encrypt()));

            if (user is null)
                throw new TestingSystemException(400, "Login or Password is incorrect");
            
            var correct = await userRepository.GetAsync(
                u => u.IpAddress == HttpContextHelper.IpAddress && u.Username == username);

            if (correct == null)
                throw new TestingSystemException(400, "Another device is already logined");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMonths(int.Parse(configuration["JWT:Lifetime"])),
                Issuer = configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public async ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO)
        {
            var alreadyExistUser = await userRepository.GetAsync(u => u.Username == userForCreationDTO.Username && u.Email == userForCreationDTO.Email);

            if (alreadyExistUser != null)
                throw new TestingSystemException(400, "User with such username or email already exist");

            var alreadyLoginedDevice = await userRepository.GetAsync(u => u.IpAddress == HttpContextHelper.IpAddress);

            if (alreadyLoginedDevice != null)
                throw new TestingSystemException(400, "This device is alredy signed");
            userForCreationDTO.Password = userForCreationDTO.Password.Encrypt();

            var user = await userRepository.CreateAsync(userForCreationDTO.Adapt<User>());
            user.IpAddress = HttpContextHelper.IpAddress;
            await userRepository.SaveChangesAsync();
            return user.Adapt<UserForViewDTO>();
        }
    }
}
