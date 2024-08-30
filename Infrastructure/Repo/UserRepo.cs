using Application.Contracts;
using Application.DTOs;
using Domine.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repo
{
    internal class UserRepo : IUser
    {
        private readonly AppDbContext _appDbContext;

        public readonly IConfiguration Configuration;

        public UserRepo(AppDbContext appDbContext, IConfiguration configuration)
        {
            this._appDbContext = appDbContext;
            this.Configuration = configuration;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUsers = await FindUserByEmailAsync(loginDTO.Email!);
            if (getUsers == null) return new LoginResponse(false, "User not found");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUsers.Password);

            if (checkPassword)
                return new LoginResponse(true, "Login successfully", GenerateToken(getUsers));
            else
                return new LoginResponse(false, "Invalid Credentials");
        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private async Task<ApplicationUser> FindUserByEmailAsync(string Email)=>
            await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
        

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var getUsers = await FindUserByEmailAsync(registerUserDTO.Email!);
            if (getUsers != null) return new RegistrationResponse(false, "User alredy exist");

            _appDbContext.Add(new ApplicationUser() 
            { 
                Name = registerUserDTO.Name,
                Email = registerUserDTO.Email, 
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
                PhoneNumber = registerUserDTO.PhoneNumber,
                Address = registerUserDTO.Address,
                IdNumber = registerUserDTO.IdNumber,
                Role = Domine.Role.Client
            });    

            await _appDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Registration are completed");

        }
    }
}
