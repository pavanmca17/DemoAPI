namespace DemoAPI.Controllers
{
    public class NoteParam
    {        
        public string Id { get; set; }

        public string Body { get; set; } = string.Empty;  

        public int UserId { get; set; } = 0;
       
    }
}