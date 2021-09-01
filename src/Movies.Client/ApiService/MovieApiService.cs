using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;

        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/v1/movies/");

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(stream, _options);
            return movies;
        }

        public async Task<Movie> GetMovie(Guid? id)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"/api/v1/movies/{id}");

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var movie = await JsonSerializer.DeserializeAsync<Movie>(stream, _options);
            return movie;
        }

        public async Task CreateMovie(Movie movie)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/movies")
            {
                Content = JsonContent.Create(movie)
            };
            
            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateMovie(Movie movie)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/v1/movies/{movie.Id}")
            {
                Content = JsonContent.Create(movie)
            };

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMovie(Guid id)
        {
            var client = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Delete,
                $"/api/v1/movies/{id}");

            var response = await client.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}
