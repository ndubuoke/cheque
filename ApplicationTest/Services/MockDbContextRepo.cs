using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;

namespace Application.UnitTests.Services
{
    public static class MockDbContextRepo
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable().BuildMockDbSet();
            return queryable.Object;
        }
    }
}
