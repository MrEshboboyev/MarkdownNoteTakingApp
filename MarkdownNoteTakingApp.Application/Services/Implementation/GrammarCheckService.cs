using MarkdownNoteTakingApp.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MarkdownNoteTakingApp.Application.Services.Implementation
{
    public class GrammarCheckService : IGrammarCheckService
    {
        // inject HttpClient
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public GrammarCheckService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> CheckGrammarAsync(string content)
        {
            var requestData = CreateRequestData(content);
            var response = await SendRequestAsync(requestData);

            return response.HasValue ? ExtractReplacementText(response.Value) : content;
        }

        #region Private Methods
        private object CreateRequestData(string content) => new
        {
            key = _config["Sapling:ApiKey"],
            text = content,
            session_id = "Test Document UUID"
        };

        private async Task<JsonElement?> SendRequestAsync(object requestData)
        {
            string json = JsonSerializer.Serialize(requestData);
            var response = await _httpClient.PostAsync(
                _config["Sapling:BaseUrl"],
                new StringContent(json, Encoding.UTF8, "application/json")
                );

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonDocument.Parse(jsonResponse).RootElement;
            }

            return null;
        }

        private string ExtractReplacementText(JsonElement root)
        {
            if (root.TryGetProperty("edits", out JsonElement edits) && edits.GetArrayLength() > 0)
            {
                var firstEdit = edits[0];
                if (firstEdit.TryGetProperty("replacement", out JsonElement replacement))
                {
                    return replacement.ToString();
                }
            }

            return string.Empty;
        }
        #endregion
    }
}
