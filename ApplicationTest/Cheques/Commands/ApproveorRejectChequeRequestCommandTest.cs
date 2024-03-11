using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.Cheques.ApproveorRejectChequeRequests;
using ChequeMicroservice.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests.Cheques.Commands
{
    public class ApproveorRejectChequeRequestCommandTest
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
        public async Task ApproveChequeRequestCommand_Success()
        {
            var handler = new ApproveorRejectChequeRequestCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new ApproveorRejectChequeRequestCommand
            {
                ChequeId = 1,
                IsApproved = true,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque request approved successfully"));
            });
        }

        [Test]
        public async Task RejectChequeRequestCommand_Success()
        {
            var handler = new ApproveorRejectChequeRequestCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new ApproveorRejectChequeRequestCommand
            {
                ChequeId = 1,
                IsApproved = false,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque request rejected successfully"));
            });
        }

        [Test]
        public async Task ActionChequeRequestCommand_InvalidChequeId_ReturnFalse()
        {
            var handler = new ApproveorRejectChequeRequestCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new ApproveorRejectChequeRequestCommand
            {
                ChequeId = 19,
                IsApproved = false,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }

        [Test]
        public async Task ActionChequeRequestCommand_AlreadyActionedRecord_ReturnFalse()
        {
            var handler = new ApproveorRejectChequeRequestCommandHandler(_contextMock.Object);
            var result = await handler.Handle(new ApproveorRejectChequeRequestCommand
            {
                ChequeId = 3,
                IsApproved = false,
                UserId = Guid.NewGuid().ToString()
            }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }
    }
}
