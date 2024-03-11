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
            var mockHttpContext = new DefaultHttpContext();
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
            mockHttpContext.Request.Headers["Authorization"] = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
        }

        [Test]
        public async Task GetChequeLeavesQuerySuccessTest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<GetChequeLeavesQuery>("Cheque leaves retrieved successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }


        [Test]
        public async Task StopChequeLeafSuccessTest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<StopChequeLeafCommand>("Cheque leaf stopped successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }


        [Test]
        public async Task ConfirmChequeLeafSuccessTest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<ConfirmChequeLeafQuery>("Cheque leaf confirmed successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
