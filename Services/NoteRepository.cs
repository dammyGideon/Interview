using System.Security.Claims;
using interview.Database;
using interview.Database.Entity;
using interview.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace interview.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext context;
       private readonly IHttpContextAccessor httpContextAccessor;

        public NoteRepository(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor)
        {
            context = _context;
            httpContextAccessor = _httpContextAccessor;
        }
// create notes
        public async Task CreateNoteAsync(NoteEntity noteEntity)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId != null)
            {
                noteEntity.UserId = int.Parse(userId);
               
            }
             context.NoteEntities.Add(noteEntity);
                await context.SaveChangesAsync();
        }
// delete single note
        public async Task DeleteNoteAsync(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            var noteToDelete = await context.NoteEntities
                .FirstOrDefaultAsync(note => note.UserId.ToString() == userId && note.Id == id);

            if (noteToDelete != null)
            {
                context.NoteEntities.Remove(noteToDelete);
                await context.SaveChangesAsync();
            }
        }
// get all Notes
        public async Task<IEnumerable<NoteEntity>> GetAllNotes(){
            var userId = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var notes = await context.NoteEntities.ToListAsync();
            return notes;
        }
// get Note by Id
        public async Task<NoteEntity> GetNoteEntityById(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var singleNote = await context.NoteEntities.FirstOrDefaultAsync(note => note.Id == id);
              return singleNote;
        }
//update         
        public async Task UpdateNoteAsync(NoteEntity noteEntity)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
                var updateNote = await context.NoteEntities
                .FirstOrDefaultAsync(u => u.UserId.ToString() == userId && u.Id == noteEntity.Id);

            if (updateNote != null)
            {
                updateNote.Note = noteEntity.Note;
                updateNote.Note = noteEntity.Note;
                context.NoteEntities.Update(updateNote);
                await context.SaveChangesAsync();
            }
        }
    }
}
