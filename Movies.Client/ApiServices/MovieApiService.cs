using Movies.Client.Models;

namespace Movies.Client.ApiServices
{
	public class MovieApiService : IMovieApiService
	{
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
			var movieList = new List<Movie>();
			movieList.Add(new Movie
			{
				Id = 1,
				Genre = "Comics",
				Title = "Fantasy",
				ImageUrl = "img/src",
				Rating = "5.6",
				ReleaseDate = DateTime.Now,
				Owner = "jho"
			});
			movieList.Add(new Movie
			{
				Id = 2,
				Genre = "Drama",
				Title = "Queen",
				ImageUrl = "img/src",
				Rating = "8.6",
				ReleaseDate = DateTime.Now,
				Owner = "jho"
			});

			return await Task.FromResult(movieList);
		}

		public Task<Movie> UpdateMovie(Movie movie)
		{
			throw new NotImplementedException();
		}
	}
}
