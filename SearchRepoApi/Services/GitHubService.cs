using SearchRepoApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchRepoApi.Services
{
    public interface IGitHubService
    {
        public Task<Repository> SearchRepository(string repositoryName);
    }
    public class GitHubService : IGitHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GitHubService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Repository> SearchRepository(string repositoryName)
        {
            HttpClient apiClient = _httpClientFactory.CreateClient();

            string requestUri = "https://api.github.com/search/repositories?q=" + repositoryName;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "SearchRepoApi");

            HttpResponseMessage response = await apiClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Stream content = await response.Content.ReadAsStreamAsync();
                Repository repository = await JsonSerializer.DeserializeAsync<Repository>(content);
                return repository;
            }
            else
            {
                return new Repository();
            }
        }
    }
}
