using SrezBlazor.ApiRequest.Model;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SrezBlazor
{
    public class ApiRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiRequestService> _logger;

        public ApiRequestService(HttpClient httpClient, ILogger<ApiRequestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<UserGet>> GetUsersAsync()
        {
            var url = "api/Users/getAllUsers";

            try
            {
                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (string.IsNullOrEmpty(responseContent))
                {
                    _logger.LogWarning("Ответ от сервера пуст.");
                    return [];
                }

                var usersData = JsonSerializer.Deserialize<List<UserGet>>(responseContent, new JsonSerializerOptions //
                {
                    PropertyNameCaseInsensitive = true
                });

                return usersData ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запросе");
                return [];
            }
        }

        public async void EditUserAsync(UserProd user)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync("http://localhost:5215/api/Users/EditUser", user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запросе");
            }
        }

        public async void DeleteUserAsync(int Id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5215/api/Users/DeleteUser/{Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запросе");
            }
        }

        public async Task<string> CreateUserAsync(AddUser user)
        {
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:5215/api/Users/createNewUserAndLogin", user);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Authorization failed: {errorContent}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent, options);
            var token = tokenResponse.Token;

            return token;
        }

        public async Task<string> AuthorizationAsync(UserGet auth)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5215/api/Users/Authorization", auth);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Authorization failed: {errorContent}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent, options);
            var token = tokenResponse.Token;

            return token;
        }

        public class TokenResponse
        {
            public bool Status { get; set; }
            public string Token { get; set; }
        }

    }
}
