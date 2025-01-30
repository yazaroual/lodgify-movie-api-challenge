using System.Collections.Generic;
using System.Threading.Tasks;
using ApiApplication.Dto;

public interface IApiClientGrpc
{
    /// <summary>
    /// Retrieve all the movies
    /// </summary>
    /// <returns>A list of movies</returns>
    Task<List<MovieDto>> GetAll();
}