using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class NotesController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        
        [HttpGet]
        public async Task<IEnumerable<Note>> Get()
        {
            return await _noteRepository.GetAllNotes();
        }

        // GET api/notes/5 - retrieves a specific note using either Id or InternalId (BSonId)
        [HttpGet("{id}")]
        public async Task<Note> Get(string id)
        {
            return await _noteRepository.GetNote(id) ?? new Note();
        }

        // GET api/notes/text/date/size
        // ex: http://localhost:53617/api/notes/Test/2018-01-01/10000
        [HttpGet(template: "{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IEnumerable<Note>> Get(string bodyText,
                                                 DateTime updatedFrom,
                                                 long headerSizeLimit)
        {
            return await _noteRepository.GetNote(bodyText, updatedFrom, headerSizeLimit)
                        ?? new List<Note>();
        }

        // POST api/notes - creates a new note
        [HttpPost]
        public void Post([FromBody] NoteParam newNote)
        {
            _noteRepository.AddNote(new Note
            {
                Id = newNote.Id,
                Body = newNote.Body,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                UserId = newNote.UserId
            });
        }

        // PUT api/notes/5 - updates a specific note
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _noteRepository.UpdateNoteDocument(id, value);
        }

        // DELETE api/notes/5 - deletes a specific note
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _noteRepository.RemoveNote(id);
        }
    }
}