using Microsoft.AspNetCore.Http;

namespace MarkdownNoteTakingApp.Application.Common.Models
{
    public class CreateNoteModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        // for file 
        public IFormFile? File { get; set; }
    }
}
