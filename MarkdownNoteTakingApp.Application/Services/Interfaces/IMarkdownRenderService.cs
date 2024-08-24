namespace MarkdownNoteTakingApp.Application.Services.Interfaces
{
    public interface IMarkdownRenderService
    {
        Task<string> RenderMarkdownToHtmlAsync(string markdownContent);
    }
}
