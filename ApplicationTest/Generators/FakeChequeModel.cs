using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;

namespace Application.UnitTests.Generators
{
    public static class FakeChequeModel
    {
        public static List<Cheque> GetChequeList()
        {
            return new List<Cheque>{
                new Cheque
                {
                    Id = 1,
                    IssueDate = DateTime.Now,
                    SeriesEndingNumber = "01",
                    SeriesStartingNumber = "10",
                    NumberOfChequeLeaf = 10,
                    ObjectCategory = ObjectCategory.Request,
                    ObjectCategoryDesc = ObjectCategory.Request.ToString(),
                    ChequeStatus = ChequeStatus.Initiated,
                    ChequeStatusDesc = ChequeStatus.Initiated.ToString(),
                    CreatedDate = DateTime.Now,
                    UserId = Guid.NewGuid().ToString()
                }
            };
        }
    }
}
