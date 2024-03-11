using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Application.UnitTests.Services
{
    public static class HttpContextExtensions
    {
        public static HttpContext CreateHttpContextWithMediator(this Mock<IMediator> mediatorMock)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton(mediatorMock.Object)
                .BuildServiceProvider();

            return httpContext;
        }
    }
}
