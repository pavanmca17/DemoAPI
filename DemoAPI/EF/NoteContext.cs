using DemoAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.EF
{
    public class NoteContext
    {
        // get a reference MongoDatabase
        private readonly IMongoDatabase _mongoDatabase = null;
        

        public NoteContext(IOptions<Settings> settings)
        {
            // Get Mongo Client
            var mongoClient = new MongoClient(settings.Value.MongoDBConnectionString);
         
            if (mongoClient != null)
                _mongoDatabase = mongoClient.GetDatabase(settings.Value.MongoDBConnectionString);
        }

        // get a collection of Notes
        public IMongoCollection<Note> Notes
        {
            get
            {
                return _mongoDatabase.GetCollection<Note>("Note");
            }

        }

        
    }
}
