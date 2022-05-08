using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TvMaze.Api.Controllers;
using TvMaze.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace TvMaze.Test.Api
{
    public class ShowControllerTests
    {
        /// <summary>
        /// Test get request responds with a <see cref="StatusCodes.Status404NotFound"/> when the show name is not known.
        /// </summary>
        [Fact]
        public void Get_UnknownShowName_Returns_404()
        {
            // Arrange
            var controller = this.GetShowControllerUnderTestWithConfiguredServices();
            var request = new Mock<HttpRequest>();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = Mock.Of<HttpContext>(_ => _.Request == request.Object),
            };

            // Act
            var result = controller.Get("TestShow2");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Test get request responds with a <see cref="StatusCodes.Status200OK"/> when the show name is known.
        /// </summary>
        [Fact]
        public void Get_KnownShowName_Returns_200()
        {
            // Arrange
            var controller = this.GetShowControllerUnderTestWithConfiguredServices();
            var request = new Mock<HttpRequest>();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = Mock.Of<HttpContext>(_ => _.Request == request.Object),
            };

            // Act
            var result = controller.Get("TestShow");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Get a <see cref="UftpController"/> instance with mocked services and configured services for testing purposes.
        /// </summary>
        /// <returns>A <see cref="UftpController"/> instance ready for testing.</returns>
        /// <param name="uftpPublicKey">The UFTP publick key which is used to sign the test messages.</param>
        /// <param name="bus">The bus used to test sending AMQP messages.</param>
        private TvMaze.Api.Controllers.ShowController GetShowControllerUnderTestWithConfiguredServices()
        {
            var serviceProvider = new ServiceCollection().BuildServiceProvider();
            var logger = new Mock<ILogger<TvMaze.Api.Controllers.ShowController>>();
            var showRepository = new Mock<IShowRepository>();
            showRepository.Setup(x => x.GetShowByName("TestShow")).Returns(new Core.Entities.Show() { Id = 1 });
            showRepository.Setup(x => x.GetShowByName("TestShow2")).Returns((Core.Entities.Show)null);

            return new TvMaze.Api.Controllers.ShowController(logger.Object, showRepository.Object);
        }
    }
}