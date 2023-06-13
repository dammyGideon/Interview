using AutoMapper;
using interview.Database.Entity;
using interview.Dtos.UsersDtos;
using interview.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace interview.Controllers;
[Route("api/controller")]
[ApiController]
public class AuthController : ControllerBase {
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    public AuthController(IUserRepository IuserRepository, IMapper _mapper){
        userRepository =IuserRepository;
        mapper = _mapper;
    }

[HttpPost("register")]
public async Task<IActionResult> RegisterUser(UserRegDto registrationDto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var userRegDto = mapper.Map<UserRegDto>(registrationDto);
    bool isRegistered = await userRepository.RegisterUserAsync(userRegDto);

    if (isRegistered)
    {
        return Ok("User registered successfully.");
    }
    else
    {
        return BadRequest("User registration failed.");
    }
}

 [HttpPost("Login")]
 public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto){
    try{
        var login_user = await userRepository.Login(userLoginDto);
        return Ok(login_user);
    }catch (Exception ex){
        throw new Exception(ex.Message);
    }
 }

    
}