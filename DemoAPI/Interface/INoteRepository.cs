using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Interface
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(string id);

        // query after multiple parameters
        Task<IEnumerable<Note>> GetNote(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        // add new note document
        Task AddNote(Note item);

        // remove a single document / note
        Task<bool> RemoveNote(string id);

        // update just a single document / note
        Task<bool> UpdateNote(string id, string body);

        // demo interface - full document update
        Task<bool> UpdateNoteDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllNotes();
    }
}
