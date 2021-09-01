using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Movies.Client.Handlers;
using System;

namespace Movies.Client.Extensions.ServiceExtensions
{
    public static class ClientFactory
    {
        public static IServiceCollection ProduceHttpClientFactory(this IServiceCollection services)
        {
            // create an HttpClient used for accessing the API
            services.AddHttpClient("MovieAPIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerTokenHandler>();

            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            return services;

            //Used in the code flow version to access the token endpoint to retrieve API Valid token. 
            //services.AddSingleton(new ClientCredentialsTokenRequest
            //{
            //    Address = "https://localhost:5005/connect/token",
            //    ClientId = "movieApiClient",
            //    ClientSecret = "secret",
            //    Scope = "movieAPI"
            //}); return services;
        }
    }
}
