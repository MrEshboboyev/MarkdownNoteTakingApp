namespace MarkdownNoteTakingApp.Domain.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        // New properties to store file information
        public string FileName { get; set; }    
        public string FilePath { get; set; }    
        public long? FileSize { get; set; }    
    }
}
