using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiApplication.Dto;
using ApiApplication.Extensions;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using ProtoDefinitions;

namespace ApiApplication
{
    public class ApiClientGrpc : IApiClientGrpc
    {
        private readonly string _apiKey;

        public ApiClientGrpc(IOptions<ApiClientGrpcSettings> settings)
        {
            _apiKey = settings.Value.ApiKey;
        }

        public async Task<List<MovieDto>> GetAll()
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress("https://localhost:7443", new GrpcChannelOptions()
            {
                HttpHandler = httpHandler,
            });

            var client = new MoviesApi.MoviesApiClient(channel);

            // Included the api key from the configuration
            var all = await client.GetAllAsync(new Empty(), new Metadata
            {
                { "X-Apikey", _apiKey }
            });

            all.Data.TryUnpack<showListResponse>(out var data);
            return data.Shows.Select(s => s.ToMovieDto()).ToList();
        }
    }
}
