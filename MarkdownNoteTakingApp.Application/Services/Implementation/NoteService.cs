using MarkdownNoteTakingApp.Application.Common.Interfaces;
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

        public async Task<Guid> CreateNoteAsync(string title, string content)
        {
            var note = new Note 
            {
                Id = Guid.NewGuid(),
                Title = title, 
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            await _noteRepository.AddNoteAsync(note);
            return note.Id;
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
