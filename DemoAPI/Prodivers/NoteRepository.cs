using DemoAPI.EF;
using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Impl
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteContext _context = null;

        public NoteRepository(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings);
        }

        // Get all Notes
        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            try
            {
                return await _context.Notes.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }      
        }

        // query after Id or InternalId (BSonId value)
        public async Task<Note> GetNote(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Notes.Find(note => note.Id == id  || note.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        // query after body text, updated time, and header image size
        public async Task<IEnumerable<Note>> GetNote(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.Notes.Find(note => note.Body.Contains(bodyText) &&
                                                note.UpdatedOn >= updatedFrom &&
                                                note.HeaderImage.ImageSize <= headerSizeLimit);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddNote(Note item)
        {
            try
            {
                await _context.Notes.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveNote(string id)
        {
            try
            {
                DeleteResult actionResult  = await _context.Notes.DeleteOneAsync(Builders<Note>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged  && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(string id, string body)
        {
            var filter = Builders<Note>.Filter.Eq(s => s.Id, id);
            var update = Builders<Note>.Update.Set(s => s.Body, body).CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult  = await _context.Notes.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged  && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(string id, Note item)
        {
            try
            {
                ReplaceOneResult actionResult  = await _context.Notes.ReplaceOneAsync(n => n.Id.Equals(id), 
                                                                                      item , 
                                                                                      new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged  && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<bool> UpdateNoteDocument(string id, string body)
        {
            var item = await GetNote(id) ?? new Note();
            item.Body = body;
            item.UpdatedOn = DateTime.Now;
            return await UpdateNote(id, item);
        }

        public async Task<bool> RemoveAllNotes()
        {
            try
            {
                DeleteResult actionResult  = await _context.Notes.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged  && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
