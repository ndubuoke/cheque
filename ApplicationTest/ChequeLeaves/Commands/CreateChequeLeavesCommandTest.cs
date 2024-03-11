using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.ChequeLeaves.Commands;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.ChequeLeaves.Commands
{
    public class CreateChequeLeavesCommandTest
    {
        private Mock<IApplicationDbContext> _contextMock;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            var chequeLeaves = MockDbContextRepo.GetQueryableMockDbSet(FakeChequeLeavesModel.GetChequeLeaves());
            var cheques = MockDbContextRepo.GetQueryableMockDbSet(FakeChequeModel.GetChequeList());
            _contextMock.Setup(x => x.ChequeLeaves).Returns(chequeLeaves);
            _contextMock.Setup(c => c.Cheques).Returns(cheques);
        }

        [Test]
        public async Task CreateChequeLeaf_Success()
        {
            var handler = new CreateChequeLeavesCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeLeavesCommand
            {
                ChequeId = 2,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque leaves created successfully"));
            });
        }

        [Test]
        public async Task CreateChequeLeaf_StartingSeriesGreaterThanEndingSeries_ReturnFalse()
        {
            var handler = new CreateChequeLeavesCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeLeavesCommand
            {
                ChequeId = 1,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }

        [Test]
        public async Task CreateChequeLeaf_InvalidSeriesNumber_ReturnFalse()
        {
            var handler = new CreateChequeLeavesCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeLeavesCommand
            {
                ChequeId = 3,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }
    }
}
