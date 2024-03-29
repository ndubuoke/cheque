﻿using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.ChequeLeaves.Queries;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.ChequeLeaves.Queries
{
    public class GetChequeLeavesQueryTest
    {
        private Mock<IApplicationDbContext> _contextMock;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            var chequeLeaves = MockDbContextRepo.GetQueryableMockDbSet(FakeChequeLeavesModel.GetChequeLeaves());
            _contextMock.Setup(x => x.ChequeLeaves).Returns(chequeLeaves);
        }

        [Test]
        public async Task RetrieveChequeLeavesTests_WithSearchValue_Success()
        {
            var request = new GetChequeLeavesQuery
            {
                Skip = 0,
                Take = 0,
                ChequeId = 1,
                SearchValue = "123456789"
            };

            var handler = new GetChequeLeavesQueryHandler(_contextMock.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque leaves retrieved successfully"));
            });
        }

        [Test]
        public async Task RetrieveChequeLeavesTests_WithTakeValue_Success()
        {
            var request = new GetChequeLeavesQuery
            {
                Skip = 0,
                Take = 1,
                ChequeId = 1,
                SearchValue = "123456789"
            };

            var handler = new GetChequeLeavesQueryHandler(_contextMock.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque leaves retrieved successfully"));
            });
        }


        [Test]
        public async Task RetrieveChequeLeavesTests_Failure()
        {
            var request = new GetChequeLeavesQuery
            {
                Skip = 0,
                Take = 0,
                ChequeId = 20
            };

            var handler = new GetChequeLeavesQueryHandler(_contextMock.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }
    }
}


