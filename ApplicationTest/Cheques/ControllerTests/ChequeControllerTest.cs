using API.Controllers;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.Cheques.ApproveorRejectChequeRequests;
using ChequeMicroservice.Application.Cheques.CreateCheques;
using ChequeMicroservice.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Cheques.ControllerTests
{
    public class ChequeControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IHttpContextAccessor> _contextAccessorMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
            mockHttpContext.Request.Headers.Authorization= "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
        }

        [Test]
        public async Task CreateChequeRequest_InvalidToken_Test()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers.Authorization = "";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque created successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.CreateChequeRequest(new CreateChequeRequestCommand()) as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CreateChequeRequestControllerTest()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque created successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.CreateChequeRequest(new CreateChequeRequestCommand()) as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task ApproveOrRejectControllerTest_InvalidToken()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque request approved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.ApproveorRejectChequeRequest(new ApproveorRejectChequeRequestCommand()) as OkObjectResult;
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task ApproveOrRejectControllerTest()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque request approved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.ApproveorRejectChequeRequest(new ApproveorRejectChequeRequestCommand()) as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetChequeByIdControllerTest_InvalidToken_Failure()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque retrieved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.GetChequeById(1) as OkObjectResult;
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task GetChequeByIdControllerTest()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheque retrieved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.GetChequeById(1) as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetChequesControllerTest_InvalidToken_Failure()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheques retrieved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.GetChequeList() as OkObjectResult;
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task GetChequesControllerTest()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("Cheques retrieved successfully"));
            var httpContext = _mediatorMock.CreateHttpContextWithMediator();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var controller = new ChequesController(_contextAccessorMock.Object)
            {
                ControllerContext = controllerContext,
            };
            var result = await controller.GetChequeList() as OkObjectResult;
            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }
    }
}
