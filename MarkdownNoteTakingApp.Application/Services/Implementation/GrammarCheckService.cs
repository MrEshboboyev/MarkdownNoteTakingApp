using MarkdownNoteTakingApp.Application.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace MarkdownNoteTakingApp.Application.Services.Implementation
{
    public class GrammarCheckService : IGrammarCheckService
    {
        // inject HttpClient
        private readonly HttpClient _httpClient;

        public GrammarCheckService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CheckGrammarAsync(string content)
        {
            var response = await _httpClient.PostAsJsonAsync("https://api.grammarcheck.com/check", new { text = content });

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var correctedText = JsonSerializer.Deserialize<string>(jsonResponse);
                return correctedText;
            }

            return content; // return original content if the check fails
        }
    }
}
