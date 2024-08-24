using MarkdownNoteTakingApp.Domain.Entities;

namespace MarkdownNoteTakingApp.Application.Common.Interfaces
{
    public interface INoteRepository
    {
        Task<Guid> AddNoteAsync(Note note);
        Task<Note> GetNoteByIdAsync(Guid id);
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task DeleteNoteAsync(Guid id);
        Task<bool> NoteExistAsync(Guid id);
    }
}
