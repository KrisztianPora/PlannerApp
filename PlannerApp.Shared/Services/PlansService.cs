using PlannerApp.Shared.Models;
using PlannerApp.Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class PlansService
    {
        private readonly string _baseUrl;

        HttpClient httpClient = new HttpClient();

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public PlansService(string url)
        {
            _baseUrl = url;
        }

        public string AccessToken { get; set; }

        public async Task<PlansCollectionPagingResponse> GetAllPlansByPageAsync(int page = 1)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await httpClient.GetAsync(_baseUrl + "/api/plans?page=" + page);
            var responseAsString = await response.Content.ReadAsStringAsync();
            PlansCollectionPagingResponse plansCollectionPagingResponse = JsonSerializer.Deserialize<PlansCollectionPagingResponse>(responseAsString, serializerOptions);
            return plansCollectionPagingResponse;
        }

        public async Task<PlanSingleResponse> GetPlanByIdAsync(string id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await httpClient.GetAsync(_baseUrl + "/api/plans/" + id);
            var responseAsString = await response.Content.ReadAsStringAsync();
            PlanSingleResponse planSingleResponse = JsonSerializer.Deserialize<PlanSingleResponse>(responseAsString, serializerOptions);
            return planSingleResponse;
        }

        public async Task<PlansCollectionPagingResponse> SearchPlansByPageAsync(string query, int page = 1)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await httpClient.GetAsync(_baseUrl + "/api/plans/search?query=" + query + "&page=" + page);
            var responseAsString = await response.Content.ReadAsStringAsync();
            PlansCollectionPagingResponse plansCollectionPagingResponse = JsonSerializer.Deserialize<PlansCollectionPagingResponse>(responseAsString, serializerOptions);
            return plansCollectionPagingResponse;
        }

        public async Task<PlanSingleResponse> PostPlanAsync(PlanRequest planRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var multiForm = new MultipartFormDataContent()
            {
                {new StringContent(planRequest.Title), "Title"},
                {new StringContent(planRequest.Description), "Description"},
            };
            if (planRequest.CoverFile != null)
            {
                multiForm.Add(new StreamContent(planRequest.CoverFile), "CoverFile", planRequest.FileName);
            }

            var response = await httpClient.PostAsync(_baseUrl + "/api/plans", multiForm);
            var responseAsString = await response.Content.ReadAsStringAsync();
            PlanSingleResponse planSingleResponse = JsonSerializer.Deserialize<PlanSingleResponse>(responseAsString, serializerOptions);
            return planSingleResponse;
        }

        public async Task<PlanSingleResponse> EditPlanAsync(PlanRequest planRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var multiForm = new MultipartFormDataContent()
            {
                {new StringContent(planRequest.Id), "Id"},
                {new StringContent(planRequest.Title), "Title"},
                {new StringContent(planRequest.Description), "Description"}, 
            };
            if (planRequest.CoverFile != null)
            {
                multiForm.Add(new StreamContent(planRequest.CoverFile), "CoverFile", planRequest.FileName);
            }

            var response = await httpClient.PutAsync(_baseUrl + "/api/plans", multiForm);
            var responseAsString = await response.Content.ReadAsStringAsync();
            PlanSingleResponse planSingleResponse = JsonSerializer.Deserialize<PlanSingleResponse>(responseAsString, serializerOptions);
            return planSingleResponse;
        }

        public async Task<PlanSingleResponse> DeletePlanAsync(string id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await httpClient.DeleteAsync(_baseUrl + "/api/plans/" + id);
            var responseAsString = await response.Content.ReadAsStringAsync();
            PlanSingleResponse planSingleResponse = JsonSerializer.Deserialize<PlanSingleResponse>(responseAsString, serializerOptions);
            return planSingleResponse;
        }
    }
}
