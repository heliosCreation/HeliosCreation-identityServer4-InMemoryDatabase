using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Client.ApiService
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();

        }

        public Task DeleteMovie(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovie(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = new List<Movie>();
            movies.Add(new Movie
            {
                Id = Guid.NewGuid(),
                Genre = "Drama",
                Title = "The Shawshank Redemption",
                Rating = "9.3",
                ImageUrl = "images/src",
                ReleaseDate = new DateTime(1994, 5, 5),
                OwnerId = "alice"
            });
            return await Task.FromResult(movies);
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
