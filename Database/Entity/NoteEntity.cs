using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace interview.Database.Entity{
    

    public class NoteEntity{

        [Key]
        public int Id {get;set;}
        [Required(ErrorMessage ="Note Can not be Empty")]
        public string Note {get;set;}

        [ForeignKey(nameof(UsersEntity))]
        public int UserId {get;set;}

        public UsersEntity UsersEntity {get;set;}
        
    }
}