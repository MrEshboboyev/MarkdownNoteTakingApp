using MarkdownNoteTakingApp.Application.Common.Interfaces;
using MarkdownNoteTakingApp.Domain.Entities;
using MarkdownNoteTakingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MarkdownNoteTakingApp.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        // inject Database
        private readonly ApplicationDbContext _db;

        public NoteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> AddNoteAsync(Note note)
        {
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
            return note.Id;
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            var note = _db.Notes.FirstOrDefault(x => x.Id == id);
            if (note != null)
            {
                _db.Notes.Remove(note);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _db.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            return await _db.Notes.FindAsync(id);
        }
    }
}
