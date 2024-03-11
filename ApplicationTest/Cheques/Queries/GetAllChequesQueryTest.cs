using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.Cheques.Queries;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Cheques.Queries
{
    public class GetAllChequesQueryTest
    {
        private Mock<IApplicationDbContext> _contextMock;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task RetrieveChequesTests_Success()
        {
            var cheques = MockDbContextRepo.GetQueryableMockDbSet(FakeChequeModel.GetChequeList());
            _contextMock.Setup(x => x.Cheques).Returns(cheques);
            var handler = new GetAllChequesQueryHandler(_contextMock.Object);
            var result = await handler.Handle(new GetAllChequesQuery(), CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Message, Is.EqualTo("Cheques retrieved successfully"));
            });
        }

        [Test]
        public async Task RetrieveChequesTests_EmptyList_Failure()
        {
            var cheques = MockDbContextRepo.GetQueryableMockDbSet(FakeChequeModel.GetEmptyChequeList());
            _contextMock.Setup(x => x.Cheques).Returns(cheques);
            var handler = new GetAllChequesQueryHandler(_contextMock.Object);
            var result = await handler.Handle(new GetAllChequesQuery(), CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }
    }
}
