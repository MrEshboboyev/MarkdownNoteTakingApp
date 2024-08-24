using MarkdownNoteTakingApp.Domain.Entities;

namespace MarkdownNoteTakingApp.Application.Services.Interfaces
{
    public interface INoteService
    {
        Task<Guid> CreateNoteAsync(string title, string content);
        Task<Note> GetNoteByIdAsync(Guid id);
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task DeleteNoteAsync(Guid id);
        Task<string> RenderNoteToHtmlAsync(Guid id);
        Task<string> CheckNoteGrammarAsync(Guid id);
    }
}
