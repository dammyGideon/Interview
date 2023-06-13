using System.ComponentModel.DataAnnotations;

namespace interview.Database.Entity{
    

    public class UsersEntity{
        [Key]
        public int Id {get;set;}
        [Required (ErrorMessage ="Email address can not be empty")]
        [EmailAddress]
        public string Email {get;set;}
        [Required(ErrorMessage ="Password can not be empty")]
        public string Password {get;set;}

        public ICollection<NoteEntity> NoteEntity {get;set;}
    }
}