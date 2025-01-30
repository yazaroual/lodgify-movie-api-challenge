using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiApplicationTests
{
    public class ShowtimesControllerTests
    {        private readonly ShowtimesController _controller;

        public ShowtimesControllerTests()
        {
            _controller = new ShowtimesController();
        }

        [Fact]
        public async Task GetAvailableMovies_ReturnsOkResult_WithListOfMovies()
        {
            // Arrange

            // Act
            var result = await _controller.GetAvailableMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetAvailableMoviesResponse>(okResult.Value);
            Assert.Single(returnValue.Movies);
        }
    }
}
