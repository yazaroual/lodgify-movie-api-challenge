using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Repositories.Abstractions;
using ApiApplication.Dto;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace ApiApplicationTests
{
    public class ShowtimesControllerTests
    {
        private readonly ShowtimesController _controller;
        private IApiClientGrpc _apiClientGrpc;
        private IAuditoriumsRepository _auditoriumsRepository;
        private IShowtimesRepository _showtimesRepository;
        private CancellationTokenSource _cancellationTokenSource;

        public ShowtimesControllerTests()
        {
            _cancellationTokenSource = new CancellationTokenSource(1000);

            _apiClientGrpc = Substitute.For<IApiClientGrpc>();
            _auditoriumsRepository = Substitute.For<IAuditoriumsRepository>();
            _showtimesRepository = Substitute.For<IShowtimesRepository>();

            _controller = new ShowtimesController(_apiClientGrpc, _auditoriumsRepository, _showtimesRepository);
        }

        [Fact]
        public async Task GetAvailableMovies_ReturnsOkResult_WithListOfMovies()
        {
            _apiClientGrpc.GetAll().Returns(new List<MovieDto> { new MovieDto { ImdbId = "1", Title = "Test" } });

            var result = await _controller.GetAvailableMovies();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetAvailableMoviesResponse>(okResult.Value);
            Assert.Single(returnValue.Movies);
        }

        [Fact]
        public async Task GetAvailableAuditoriums_ReturnsOkResult_WithListOfAuditoriums()
        {
            _auditoriumsRepository.GetAllAsync(_cancellationTokenSource.Token).Returns(new List<AuditoriumEntity>() { new AuditoriumEntity { Id = 1 } });

            var result = await _controller.GetAvailableAuditoriums(_cancellationTokenSource.Token);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetAvailableAuditoriumsResponse>(okResult.Value);
            Assert.Single(returnValue.Auditoriums);
        }

        [Fact]
        public async Task CreateShowTIme_ReturnsOkResult_WithShowtime()
        {
            var request = new CreateShowtimeRequest
            {
                MovieId = "1",
                SessionDate = DateTime.Now,
                AuditoriumId = 1
            };

            _showtimesRepository.CreateShowtime(Arg.Any<ShowtimeEntity>(), _cancellationTokenSource.Token)
                .Returns(new ShowtimeEntity { Id = 1, Movie = new MovieEntity { ImdbId = "1" }, AuditoriumId = 1, SessionDate = DateTime.Now });

            var result = await _controller.CreateShowtime(request, _cancellationTokenSource.Token);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateShowtimeResponse>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}
