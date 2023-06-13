using AutoMapper;
using interview.Database.Entity;
using interview.Dtos.NotesDtos;

namespace interview.Mappings.NoteMapper;

public class CreateNoteMapper : Profile {
    
    public CreateNoteMapper(){
        CreateMap<NoteEntity, ReadNoteDto>();
        CreateMap<CreateNoteDto, NoteEntity> ();
        CreateMap<UpdateNoteDto, NoteEntity> ();
    }
}
