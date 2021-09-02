using Microsoft.Extensions.Logging;
using Movies.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Data
{
    public class MovieContextSeed
    {
        public static async Task SeedAsync(MoviesContext moviesContext, ILogger<MovieContextSeed> logger)
        {
            if (!moviesContext.Movie.Any())
            {
                moviesContext.Movie.AddRange(GetPreconfiguredOrders());
                await moviesContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(MoviesContext).Name);
            }
        }

        private static IEnumerable<Movie> GetPreconfiguredOrders()
        {
            return new List<Movie>
            {
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Drama",
                        Title = "The Shawshank Redemption",
                        Rating = "9.3",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        OwnerId = "5BE86359-073C-434B-AD2D-A3932222DABE"
                    },
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Crime",
                        Title = "The Godfather",
                        Rating = "9.2",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1972, 5, 5),
                        OwnerId = "5BE86359-073C-434B-AD2D-A3932222DABE"
                    },
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Action",
                        Title = "The Dark Knight",
                        Rating = "9.1",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(2008, 5, 5),
                        OwnerId = "5BE86359-073C-434B-AD2D-A3932222DABE"
                    },
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Crime",
                        Title = "12 Angry Men",
                        Rating = "8.9",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1957, 5, 5),
                        OwnerId = "5BE86359-073C-434B-AD2D-A3932222DABE"
                    },
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Biography",
                        Title = "Schindler's List",
                        Rating = "8.9",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1993, 5, 5),
                        OwnerId = "88421113"
                    },
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Drama",
                        Title = "Pulp Fiction",
                        Rating = "8.9",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        OwnerId = "88421113"
                    },
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Drama",
                        Title = "Fight Club",
                        Rating = "8.8",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1999, 5, 5),
                        OwnerId = "88421113"
                    },
                    new Movie
                    {
                        Id = Guid.NewGuid(),
                        Genre = "Romance",
                        Title = "Forrest Gump",
                        Rating = "8.8",
                        ImageUrl = "images/src",
                        ReleaseDate = new DateTime(1994, 5, 5),
                        OwnerId = "88421113"
                    }
            };
        }
    }
}

