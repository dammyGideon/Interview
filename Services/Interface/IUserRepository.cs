using interview.Dtos.UsersDtos;

namespace interview.Services.Interface;

public interface IUserRepository{
    
     Task <bool> RegisterUserAsync(UserRegDto registrationDto);
     Task<Object>Login(UserLoginDto userLoginDto);
}