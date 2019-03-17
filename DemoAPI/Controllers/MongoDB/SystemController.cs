using System;
using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class SystemController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public SystemController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // Call an initialization - api/system/create
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "create")
            {
                _noteRepository.RemoveAllNotes();
              //  var name = _noteRepository.CreateIndex();

                _noteRepository.AddNote(new Note()
                {
                    Id = "1",
                    Body = "Test note 1",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new NoteImage
                    {
                        ImageSize = 10,
                        Url = "http://localhost/image1.png",
                        ThumbnailUrl = "http://localhost/image1_small.png"
                    }
                });

                _noteRepository.AddNote(new Note()
                {
                    Id = "2",
                    Body = "Test note 2",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new NoteImage
                    {
                        ImageSize = 13,
                        Url = "http://localhost/image2.png",
                        ThumbnailUrl = "http://localhost/image2_small.png"
                    }
                });

                _noteRepository.AddNote(new Note()
                {
                    Id = "3",
                    Body = "Test note 3",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new NoteImage
                    {
                        ImageSize = 14,
                        Url = "http://localhost/image3.png",
                        ThumbnailUrl = "http://localhost/image3_small.png"
                    }
                });

                _noteRepository.AddNote(new Note()
                {
                    Id = "4",
                    Body = "Test note 4",
                    UpdatedOn = DateTime.Now,
                    UserId = 1,
                    HeaderImage = new NoteImage
                    {
                        ImageSize = 15,
                        Url = "http://localhost/image4.png",
                        ThumbnailUrl = "http://localhost/image4_small.png"
                    }
                });

                return "Database NotesDb was created, and collection 'Notes' was filled with 4 sample items";
            }

            return "Unknown";
        }
    }
}