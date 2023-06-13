 using System.ComponentModel.DataAnnotations;

namespace interview.Dtos.NotesDtos;

public class ReadNoteDto{
 
        public int Id {get;set;}
        public string Note {get;set;}
        public int UserId {get;set;}
    
}
