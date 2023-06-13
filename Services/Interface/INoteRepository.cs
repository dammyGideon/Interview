using interview.Database.Entity;

namespace interview.Services.Interface;

public interface INoteRepository{

   Task<IEnumerable<NoteEntity>> GetAllNotes();
    Task <NoteEntity> GetNoteEntityById (int id);
    Task CreateNoteAsync(NoteEntity noteEntity);

    Task UpdateNoteAsync(NoteEntity noteEntity);
    Task DeleteNoteAsync(int id);

}