﻿using MarkdownNoteTakingApp.Application.Common.Models;
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
        public async Task<IActionResult> CreateNoteAsync([FromForm] CreateNoteModel model)
        {
            var noteId = await _noteService.CreateNoteAsync(model);
            return Ok(noteId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNoteAsync(Guid id, [FromForm] UpdateNoteModel model)
        {
            if (!await _noteService.NoteExistAsync(id))
                return NotFound();

            model.Id = id;
            await _noteService.UpdateNoteAsync(model);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteByIdAsync(Guid id)
        {
            if (!await _noteService.NoteExistAsync(id))
                return NotFound();

            var note = await _noteService.GetNoteByIdAsync(id);
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
            if (!await _noteService.NoteExistAsync(id))
                return NotFound();

            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/render")]
        public async Task<IActionResult> RenderNoteToHtmlAsync(Guid id)
        {
            if (!await _noteService.NoteExistAsync(id))
                return NotFound();

            var result = await _noteService.RenderNoteToHtmlAsync(id);
            return Content(result, "text/html");
        }

        [HttpGet("{id}/check-grammar")]
        public async Task<IActionResult> CheckNoteGrammarAsync(Guid id)
        {
            if (!await _noteService.NoteExistAsync(id))
                return NotFound();

            var result = await _noteService.CheckNoteGrammarAsync(id);
            return Ok(result);
        }
    }
}
