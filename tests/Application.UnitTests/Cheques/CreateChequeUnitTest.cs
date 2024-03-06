using Application.UnitTests.ContextMock;
using Application.UnitTests.Documents;
using FluentAssertions;
using Moq;
using ChequeMicroservice.Application.Common.Interfaces;
using ChequeMicroservice.Application.Documents.Commands;
using ChequeMicroservice.Domain.Entities;

using static Application.UnitTests.Documents.ServiceMockUp;
using ChequeMicroservice.Application.Cheques.CreateCheques;

namespace Application.UnitTests.SigningRequests;

[TestFixture]
public class CreateChequeUnitTest
{
    private Mock<IApplicationDbContext> _context;
    private string _userId;
    CreateChequeCommandHandler _commandHandlerMock;

    [SetUp]
    public void Setup()
    {
        
        _context = new Mock<IApplicationDbContext>();
        _userId = Guid.NewGuid().ToString();
        _commandHandlerMock = new CreateChequeCommandHandler(_context.Object);

    }

    [Test]
    public async Task CreateChequeCommandUnitTest()
    {
        //Arrange
        var mockQuery = new CreateChequeCommand
        {
            AccessToken = "",
            ChequeId=1,
            UserId = _userId,
        };

        //var data = DbContextMock.GetQueryableMockDbSet<Cheque>(FakeDataGenerator.CreateCheques());
       // _context.Setup(x => x.Cheques).ReturnsDbSet(data);

        var result = await _commandHandlerMock.Handle(mockQuery, default);
        var response = "Cheque created successfully!";

        result.Succeeded.Should().BeTrue();
        result.Entity.Should().BeNull();
        result.Message.Should().Be(response);
    }

}