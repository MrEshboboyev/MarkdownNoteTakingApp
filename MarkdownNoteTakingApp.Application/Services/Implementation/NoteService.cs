using MarkdownNoteTakingApp.Application.Common.Interfaces;
using MarkdownNoteTakingApp.Application.Common.Models;
using MarkdownNoteTakingApp.Application.Services.Interfaces;
using MarkdownNoteTakingApp.Domain.Entities;

namespace MarkdownNoteTakingApp.Application.Services.Implementation
{
    public class NoteService : INoteService
    {
        // inject INoteRepository, IGrammarCheckService, IMarkdownRenderService 
        private readonly INoteRepository _noteRepository;
        private readonly IGrammarCheckService _grammarCheckService;
        private readonly IMarkdownRenderService _markdownRenderService;

        public NoteService(INoteRepository noteRepository, 
            IGrammarCheckService grammarCheckService,
            IMarkdownRenderService markdownRenderService)
        {
            _noteRepository = noteRepository;
            _grammarCheckService = grammarCheckService;
            _markdownRenderService = markdownRenderService;
        }

        public async Task<string> CheckNoteGrammarAsync(Guid id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            return await _grammarCheckService.CheckGrammarAsync(note.Content);
        }

        public async Task<Guid> CreateNoteAsync(CreateNoteModel model)
        {
            // handle file upload
            string fileName = null;
            string filePath = null;
            long? fileSize = null;

            if (model.File != null)
            {
                fileName = $"{Guid.NewGuid()}_{model.File.FileName}";
                filePath = Path.Combine("wwwroot", "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                fileSize = model.File.Length;
            }

            var note = new Note 
            {
                Id = Guid.NewGuid(),
                Title = model.Title, 
                Content = model.Content,
                FileName = fileName,
                FilePath = filePath,
                FileSize = fileSize,
                CreatedAt = DateTime.UtcNow,
            };

            await _noteRepository.CreateNoteAsync(note);
            return note.Id;
        }

        public async Task UpdateNoteAsync(UpdateNoteModel model)
        {
            var note = await _noteRepository.GetNoteByIdAsync(model.Id);

            if (note == null) 
            {
                throw new Exception("Not found!");
            }

            // Update note properties
            note.Title = model.Title;
            note.Content = model.Content;
            note.LastModifiedAt = DateTime.UtcNow;

            // Handle new file upload, if a new file is provided
            if (model.File != null)
            {
                if (!string.IsNullOrEmpty(note.FilePath) && File.Exists(note.FilePath))
                {
                    File.Delete(note.FilePath);
                }

                note.FileName = model.File.FileName;
                note.FilePath = Path.Combine("wwwroot", "uploads", note.FileName);

                using (var stream = new FileStream(note.FilePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                note.FileSize = model.File.Length;
            }

            await _noteRepository.UpdateNoteAsync(note);
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            await _noteRepository.DeleteNoteAsync(id);
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _noteRepository.GetAllNotesAsync();
        }

        public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            return await _noteRepository.GetNoteByIdAsync(id);
        }

        public async Task<string> RenderNoteToHtmlAsync(Guid id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            return await _markdownRenderService.RenderMarkdownToHtmlAsync(note.Content);
        }

        public async Task<bool> NoteExistAsync(Guid id)
        {
            return await _noteRepository.NoteExistAsync(id);
        }
    }
}
