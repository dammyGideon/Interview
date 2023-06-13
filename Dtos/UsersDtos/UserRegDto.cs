using System.ComponentModel.DataAnnotations;

namespace interview.Dtos.UsersDtos;

public class UserRegDto{

        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        public string Password {get;set;}

}