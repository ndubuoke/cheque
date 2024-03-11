using API.Controllers;
using ChequeMicroservice.Application.ChequeLeaves.Commands;
using MediatR;
using Moq;
using ChequeMicroservice.Application.Common.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.UnitTests.ChequeLeaves.ControllerTests
{
    public class ChequeLeafControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private ChequesController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _controller = new ChequesController(_httpContextAccessor.Object);
        }

        [Test]
        public async Task StopChequeLeaf_ValidLeafNumber_ReturnsOk()
        {
            // Arrange
            var result = _controller.GetChequeById(1);
            Assert.AreEqual(true, result.IsCompletedSuccessfully);
        }

    }
}
