using MarkdownNoteTakingApp.Application.Common.Models;
using MarkdownNoteTakingApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownNoteTakingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        // inject INoteService
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNoteAsync([FromBody] CreateNoteModel model)
        {
            var noteId = await _noteService.CreateNoteAsync(model.Title, model.Content);

            // Constructing the URI manually
            var locationUrl = Url.Action(nameof(GetNoteByIdAsync), new { id = noteId });

            return Created(locationUrl, noteId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteByIdAsync(Guid id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotesAsync()
        {
            var note = await _noteService.GetAllNotesAsync();
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(Guid id)
        {
            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/render")]
        public async Task<IActionResult> RenderNoteToHtmlAsync(Guid id)
        {
            var html = await _noteService.RenderNoteToHtmlAsync(id);
            return Content(html, "text/html");
        }

        [HttpGet("{id}/check-grammar")]
        public async Task<IActionResult> CheckNoteGrammarAsync(Guid id)
        {
            var correctedText = await _noteService.CheckNoteGrammarAsync(id);
            return Ok(correctedText);
        }
    }
}
