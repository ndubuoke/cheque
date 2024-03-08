using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.Cheques.Queries;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Cheques.Queries
{
    public class GetChequeByIdQueryTest
    {
        private Mock<IApplicationDbContext> _contextMock;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            var cheques = MockDbContextRepo.GetQueryableMockDbSet(FakeChequeModel.GetChequeList());
            _contextMock.Setup(x => x.Cheques).Returns(cheques);
        }


        [Test]
        public async Task RetrieveChequeByIdTests_Success()
        {
            var handler = new GetChequeByIdQueryHandler(_contextMock.Object);
            var result = await handler.Handle(new GetChequeByIdQuery { ChequeId = 1 }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque retrieved successfully"));
            });
        }

        [Test]
        public async Task RetrieveChequeByIdTests_Failure()
        {
            var handler = new GetChequeByIdQueryHandler(_contextMock.Object);
            var result = await handler.Handle(new GetChequeByIdQuery { ChequeId = 100 }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
                Assert.That(result.Entity, Is.Null);
            });
        }
    }
}
