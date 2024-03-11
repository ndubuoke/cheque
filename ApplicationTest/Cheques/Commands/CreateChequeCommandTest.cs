using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.Cheques.CreateCheques;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Cheques.Commands
{
    public class CreateChequeCommandTest
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
        public async Task CreateChequeComand_Success()
        {
            var handler = new CreateChequeCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeCommand
            {
                ChequeId = 1,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Message, Is.EqualTo("Cheque created successfully"));
            });
        }

        [Test]
        public async Task CreateChequeComand_InvalidCheque_Failure()
        {
            var handler = new CreateChequeCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeCommand
            {
                ChequeId = 13,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }

        [Test]
        public async Task CreateChequeComand_InvalidChequeRecord_Failure()
        {
            var handler = new CreateChequeCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeCommand
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
