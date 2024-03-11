using ChequeMicroservice.Application.Common.Models;

namespace Application.UnitTests.Generators
{
    public static class FakeUtilityModel
    {

        public static Result GetSuccessfulResult()
        {
            return Result.Success("Success");
        }
    }
}
