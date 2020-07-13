using PlannerApp.Shared.Models.Requests;
using PlannerApp.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class ToDoItemsService
    {
        private readonly string _baseUrl;

        HttpClient httpClient = new HttpClient();

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public ToDoItemsService(string url)
        {
            _baseUrl = url;
        }

        public string AccessToken { get; set; }

        public async Task<ToDoItemSingleResponse> CreateItemAsync(ToDoItemRequest toDoItemRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            StringContent serializedToDoItemRequest = new StringContent(JsonSerializer.Serialize(toDoItemRequest, serializerOptions), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_baseUrl + "/api/items", serializedToDoItemRequest);
            var responseAsString = await response.Content.ReadAsStringAsync();
            ToDoItemSingleResponse toDoItemSingleResponse = JsonSerializer.Deserialize<ToDoItemSingleResponse>(responseAsString, serializerOptions);
            return toDoItemSingleResponse;
        }

        public async Task<ToDoItemSingleResponse> EditItemAsync(ToDoItemRequest toDoItemRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            StringContent serializedToDoItemRequest = new StringContent(JsonSerializer.Serialize(toDoItemRequest, serializerOptions), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(_baseUrl + "/api/items", serializedToDoItemRequest);
            var responseAsString = await response.Content.ReadAsStringAsync();
            ToDoItemSingleResponse toDoItemSingleResponse = JsonSerializer.Deserialize<ToDoItemSingleResponse>(responseAsString, serializerOptions);
            return toDoItemSingleResponse;
        }

        public async Task<ToDoItemSingleResponse> ChangeItemStateAsync(string id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await httpClient.PutAsync(_baseUrl + "/api/items/" + id, null);
            var responseAsString = await response.Content.ReadAsStringAsync();
            ToDoItemSingleResponse toDoItemSingleResponse = JsonSerializer.Deserialize<ToDoItemSingleResponse>(responseAsString, serializerOptions);
            return toDoItemSingleResponse;
        }

        public async Task<ToDoItemSingleResponse> DeleteItemAsync(string id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await httpClient.DeleteAsync(_baseUrl + "/api/items/" + id);
            var responseAsString = await response.Content.ReadAsStringAsync();
            ToDoItemSingleResponse toDoItemSingleResponse = JsonSerializer.Deserialize<ToDoItemSingleResponse>(responseAsString, serializerOptions);
            return toDoItemSingleResponse;
        }
    }
}
