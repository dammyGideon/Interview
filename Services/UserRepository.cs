using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using interview.Database;
using interview.Database.Entity;
using interview.Dtos.UsersDtos;
using interview.Helper;
using interview.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace interview.Services;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;
    private readonly JwtSettings jwtSettings;
    public UserRepository(ApplicationDbContext dbContext,  IOptions<JwtSettings> options){
        context = dbContext;
        jwtSettings=options.Value;
        
    }
    
    public async Task<bool> RegisterUserAsync(UserRegDto registrationDto)
    {
      var EmailExists = await context.UsersEntities.FirstOrDefaultAsync(u => u.Email == registrationDto.Email);

        if(EmailExists !=null){
          throw new Exception("This user already exists.");
        }
        var newUser = new UsersEntity{
            Email = registrationDto.Email,
            Password= PasswordHelper.EncryptPassword(registrationDto.Password)
        };
        context.UsersEntities.Add(newUser);
        await context.SaveChangesAsync();

       return true;

    }

   public async Task<Object> Login(UserLoginDto registerationDto){
    var user = await context.UsersEntities.FirstOrDefaultAsync(item => item.Email == registerationDto.Email);
    if (user != null)
    {
        var hashedPassword = user.Password;
        var enterpassword = PasswordHelper.VerifyPassword(registerationDto.Password, hashedPassword);
        if (enterpassword)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(jwtSettings.securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            var response = new LoginResponse{
                 Success = true,
                 Message = "User Login Successful",
                 AccessToken = accessToken
            };
            return response;
        }
        else
        {
            var response = new LoginResponse{
                 Success = false,
                 Message = "Invalid User Credentials"
            };
            return response;
        }
    }
    else
    {
        var response = new LoginResponse {
             Success = false,
             Message = "Unauthorized user"
        };
        return response;
    }
}

   
}