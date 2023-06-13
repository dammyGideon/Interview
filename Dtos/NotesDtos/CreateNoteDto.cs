 using System.ComponentModel.DataAnnotations;

namespace interview.Dtos.NotesDtos;

public class CreateNoteDto{
 
        [Required]
        public string Note {get;set;}
        [Required]
        public int UserId {get;set;}
    
}
