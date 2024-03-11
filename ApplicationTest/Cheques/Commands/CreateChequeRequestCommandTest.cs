using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.Cheques.CreateCheques;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Cheques.Commands
{
    public class CreateChequeRequestCommandTest
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
        public async Task CreateChequeRequestComand_Success()
        {
            var handler = new CreateChequeRequestCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeRequestCommand 
            { 
                NumberOfChequeLeaf = 1,
                IssueDate = DateTime.Now,
                SeriesEndingNumber = "42",
                SeriesStartingNumber = "20",
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Message, Is.EqualTo("Cheque created successfully"));
            });
        }

        [Test]
        public async Task CreateChequeRequestComand_ExistingSeriesNumbers_Failure()
        {
            var handler = new CreateChequeRequestCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new CreateChequeRequestCommand
            {
                NumberOfChequeLeaf = 1,
                IssueDate = DateTime.Now,
                SeriesEndingNumber = "21",
                SeriesStartingNumber = "10",
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
                Assert.That(result.Entity, Is.Null);
                Assert.That(result.Message, Is.EqualTo("An active cheque already exist"));
            });
        }
    }
}
