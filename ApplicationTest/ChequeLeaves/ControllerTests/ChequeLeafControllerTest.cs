using API.Controllers;
using ChequeMicroservice.Application.ChequeLeaves.Commands;
using MediatR;
using Moq;
using ChequeMicroservice.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.UnitTests.Services;
using NUnit.Framework;
using ChequeMicroservice.Application.ChequeLeaves.Queries;

namespace Application.UnitTests.ChequeLeaves.ControllerTests
{
    public class ChequeLeafControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IHttpContextAccessor> _contextAccessorMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        [Test]
        public async Task GetChequeLeavesQuery_WithoutAccessToken_Test()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers.Authorization = "";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque leaves retrieved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequeLeavesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.GetChequeLeaves(0, 0, 1) as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }


        [Test]
        public async Task GetChequeLeavesQuerySuccessTest()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers.Authorization = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque leaves retrieved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequeLeavesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.GetChequeLeaves(0, 0, 1) as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }


        [Test]
        public async Task StopChequeLeafSuccessTest()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers.Authorization = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque leaf stopped successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequeLeavesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.StopChequeLeaf("12345") as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }


        [Test]
        public async Task ConfirmChequeLeafSuccessTest()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers.Authorization = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque leaf confirmed successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequeLeavesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.ConfirmChequeLeaf("12345") as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }
    }
}
