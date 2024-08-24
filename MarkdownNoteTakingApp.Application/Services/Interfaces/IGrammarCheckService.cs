namespace MarkdownNoteTakingApp.Application.Services.Interfaces
{
    public interface IGrammarCheckService
    {
        Task<string> CheckGrammarAsync(string content);
    }
}
