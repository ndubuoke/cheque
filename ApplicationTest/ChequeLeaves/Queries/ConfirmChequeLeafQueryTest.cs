using Application.UnitTests.Generators;
using Application.UnitTests.Services;
using ChequeMicroservice.Application.ChequeLeaves.Queries;
using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.ChequeLeaves.Queries
{
    public class ConfirmChequeLeafQueryTest
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
        public async Task ConfirmChequeLeaf_Success()
        {
            var leafNumber = "123456789";
            var handler = new ConfirmChequeLeafQueryHandler(_contextMock.Object);
            var result = await handler.Handle(new ConfirmChequeLeafQuery { LeafNumber = leafNumber }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.True);
                Assert.That(result.Entity, Is.Not.Null);
                Assert.That(result.Message, Is.EqualTo("Cheque leaf confirmed successfully"));
            });
        }


        [Test]
        public async Task ConfirmChequeLeaf_Failure()
        {
            var handler = new ConfirmChequeLeafQueryHandler(_contextMock.Object);
            var result = await handler.Handle(new ConfirmChequeLeafQuery { LeafNumber = Guid.NewGuid().ToString() }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Succeeded, Is.False);
            });
        }
    }
}
