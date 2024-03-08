using ChequeMicroservice.Domain.Entities;
using ChequeMicroservice.Domain.Enums;

namespace Application.UnitTests.Generators
{
    public static class FakeChequeLeavesModel
    {
        public static ChequeLeaf GetChequeLeaf()
        {
            return new ChequeLeaf
            {
                ChequeId = 1,
                ChequeLeafId = Guid.NewGuid(),
                LeafNumber = Guid.NewGuid().ToString(),
                ChequeLeafStatus = ChequeLeafStatus.Available,
                ChequeLeafStatusDesc = ChequeLeafStatus.Available.ToString(),
                CreatedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid().ToString()
            };
        }

        public static List<ChequeLeaf> GetChequeLeaves()
        {
            List<ChequeLeaf> leaves = new List<ChequeLeaf>()
            {
                new ChequeLeaf
                {
                    ChequeId = 1,
                    ChequeLeafId = Guid.NewGuid(),
                    LeafNumber = "123456789",
                    ChequeLeafStatus = ChequeLeafStatus.Available,
                    ChequeLeafStatusDesc = ChequeLeafStatus.Available.ToString(),
                    CreatedDate = DateTime.Now,
                    CreatedBy = Guid.NewGuid().ToString()
                },
                new ChequeLeaf
                {
                    ChequeId = 1,
                    ChequeLeafId = Guid.NewGuid(),
                    LeafNumber = "1234567890",
                    ChequeLeafStatus = ChequeLeafStatus.Available,
                    ChequeLeafStatusDesc = ChequeLeafStatus.Available.ToString(),
                    CreatedDate = DateTime.Now,
                    CreatedBy = Guid.NewGuid().ToString()
                }
            };
            return leaves;
        }
    }
}
