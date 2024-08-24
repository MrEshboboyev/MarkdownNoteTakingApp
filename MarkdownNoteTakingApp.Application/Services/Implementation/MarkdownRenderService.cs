using Markdig;
using MarkdownNoteTakingApp.Application.Services.Interfaces;

namespace MarkdownNoteTakingApp.Application.Services.Implementation
{
    public class MarkdownRenderService : IMarkdownRenderService
    {
        public async Task<string> RenderMarkdownToHtmlAsync(string markdownContent)
        {
            // Use MarkDig to convert MarkDown to Html
            var pipeline = new MarkdownPipelineBuilder().Build();
            var html = Markdown.ToHtml(markdownContent, pipeline);
            return await Task.FromResult(html);
        }
    }
}
