using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class AuthenticationService
    {
        private readonly string _baseUrl;

        HttpClient httpClient = new HttpClient();

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public AuthenticationService(string url)
        {
            _baseUrl = url;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterRequest registerRequest)
        {
            StringContent serializedRegisterRequest = new StringContent(JsonSerializer.Serialize(registerRequest, serializerOptions), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_baseUrl + "/api/auth/register", serializedRegisterRequest);
            var responseAsString = await response.Content.ReadAsStringAsync();
            UserManagerResponse userManagerResponse = JsonSerializer.Deserialize<UserManagerResponse>(responseAsString, serializerOptions);
            return userManagerResponse;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            StringContent serializedLoginRequest = new StringContent(JsonSerializer.Serialize(loginRequest, serializerOptions), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_baseUrl + "/api/auth/login", serializedLoginRequest);
            var responseAsString = await response.Content.ReadAsStringAsync();
            UserManagerResponse userManagerResponse = JsonSerializer.Deserialize<UserManagerResponse>(responseAsString, serializerOptions);
            return userManagerResponse;
        }
    }
}
