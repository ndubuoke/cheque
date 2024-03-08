using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.ChequeLeaves.Commands;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.ChequeLeaves.Commands
{
    public class StopChequeLeafCommandTest
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
        public async Task StopChequeLeaf_Success()
        {
            var handler = new StopChequeLeafCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new StopChequeLeafCommand
            {
                LeafNumber = "123456789",
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque leaf stopped successfully"));
            });
        }

        [Test]
        public async Task StopChequeLeaf_InvalidLeafNumber_ReturnFailure()
        {
            var handler = new StopChequeLeafCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new StopChequeLeafCommand
            {
                LeafNumber = Guid.NewGuid().ToString(),
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }
    }
}
