using System.Net;
using AutoMapper;
using interview.Database.Entity;
using interview.Dtos.NotesDtos;
using interview.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using interview.Helper;

namespace interview.Controllers
{
    [Authorize]
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly INoteRepository noteRepository;

        public NoteController(IMapper mapper, INoteRepository noteRepository)
        {
            this.mapper = mapper;
            this.noteRepository = noteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadNoteDto>>> GetAll()
        {
            var notes = await noteRepository.GetAllNotes();
            var payload = mapper.Map<IEnumerable<ReadNoteDto>>(notes);
            return Ok(payload);
        }

        [HttpGet("{id}", Name = "GetNoteById")]
        public async Task<ActionResult<ReadNoteDto>> GetNoteById(int id)
        {
            var singleNote = await noteRepository.GetNoteEntityById(id);

            if (singleNote == null)
            {
                return NotFound();
            }

            var readDto = mapper.Map<ReadNoteDto>(singleNote);
            return Ok(readDto);
        }

        [HttpPost]
        public async Task<ActionResult<ReadNoteDto>> CreateNote(CreateNoteDto createNoteDto)
        {
            var createNote = mapper.Map<NoteEntity>(createNoteDto);
            await noteRepository.CreateNoteAsync(createNote);

            var readDto = mapper.Map<ReadNoteDto>(createNote);
            return CreatedAtRoute(nameof(GetNoteById), new { Id = readDto.Id }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadNoteDto>> UpdateNote(int id, UpdateNoteDto updateNoteDto)
        {
            var updateNote = await noteRepository.GetNoteEntityById(id);

            if (updateNote == null)
            {
                return NotFound("Note not found");
            }

            mapper.Map(updateNoteDto, updateNote);
            await noteRepository.UpdateNoteAsync(updateNote);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            await noteRepository.DeleteNoteAsync(id);
            return NoContent();
        }
    }
}
