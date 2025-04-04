using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace OpenAiBackend.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public OpenAiService(HttpClient httpClient)
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("OpenAI API key is missing");
            }
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<string?> SendRequestToChatGpt(string prompt)
        {
            Console.WriteLine($"Prompt to send to OPEN AI : {prompt}");
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 4096
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
            return jsonResponse.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }

    }
}
