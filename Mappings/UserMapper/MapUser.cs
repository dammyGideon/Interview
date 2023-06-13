using AutoMapper;
using interview.Database.Entity;
using interview.Dtos.UsersDtos;
using interview.Helper;
namespace interview.Mappings.UserMapper;

public class MapUser : Profile {
    
    public MapUser(){
        CreateMap<UserRegDto, UsersEntity>()
        .ForMember(dest=>dest.Email, opt=>opt.MapFrom(src=>src.Email))
        .ForMember(dest=>dest.Password, opt=>opt.MapFrom(src=>PasswordHelper.EncryptPassword(src.Password)));

    }
}