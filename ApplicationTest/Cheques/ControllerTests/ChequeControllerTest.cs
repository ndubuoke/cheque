﻿using API.Controllers;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.ChequeLeaves.Queries;
using ChequeMicroservice.Application.Cheques.ApproveorRejectChequeRequests;
using ChequeMicroservice.Application.Cheques.CreateCheques;
using ChequeMicroservice.Application.Cheques.Queries;
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
            var mockHttpContext = new DefaultHttpContext();
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
            mockHttpContext.Request.Headers["Authorization"] = "Bearer SampleAccessToken";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
        }

        [Test]
        public async Task GetChequesQuery_WithoutAccessToken_Test()
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Authorization"] = "";
            _contextAccessorMock.Setup(a => a.HttpContext).Returns(mockHttpContext);
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<GetChequeLeavesQuery>("Cheque leaves retrieved successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task CreateChequeRequestControllerTest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<CreateChequeRequestCommand>("Cheque created successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task ApproveOrRejectControllerTest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<ApproveorRejectChequeRequestCommand>("Cheque request approved successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task GetChequeByIdControllerTest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<GetChequeByIdQuery>("Cheque retrieved successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task GetChequesControllerTest()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success<GetAllChequesQuery>("Cheques retrieved successfully"));
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
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
