using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;        
       

        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
          _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovie(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            ////
           /// WAY 1:
           
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies/");

            var response  = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(content);
            return movieList;

            ////// ///// ///// ////// ///////
            /// WAY 2:
            

            //var apiClientCredentials = new ClientCredentialsTokenRequest
            //{
            //    Address = string.Concat(_configuration[$"Authority:Url"],"/connect/token"),
            //    ClientId = "movieClient",
            //    ClientSecret = "secret",

            //    Scope = "movieAPI"
            //};

            //var client = new HttpClient();

            //var disco = await client.GetDiscoveryDocumentAsync(_configuration["Authority:Url"]);
            //if (disco.IsError)
            //{
            //    return null;
            //}

            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
            //if (tokenResponse.IsError)
            //{
            //    return null;
            //}


            //var handler = new HttpClientHandler();
            //handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
           
            //var apiclient = new HttpClient(handler);

            //apiclient.SetBearerToken(tokenResponse.AccessToken);

            //var response = await apiclient.GetAsync(string.Concat(_configuration[$"Movie_App:Url"],"/api/movies"));
            //response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();

            //List<Movie> movieList = JsonConvert.DeserializeObject<List<Movie>>(content);

            //         var movieList = new List<Movie>();
            //movieList.Add(new Movie
            //{
            //	Id = 1,
            //	Genre = "Comics",
            //	Title = "Fantasy",
            //	ImageUrl = "img/src",
            //	Rating = "5.6",
            //	ReleaseDate = DateTime.Now,
            //	Owner = "jho"
            //});
            //movieList.Add(new Movie
            //{
            //	Id = 2,
            //	Genre = "Drama",
            //	Title = "Queen",
            //	ImageUrl = "img/src",
            //	Rating = "8.6",
            //	ReleaseDate = DateTime.Now,
            //	Owner = "jho"
            //});

            return await Task.FromResult(movieList);
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
