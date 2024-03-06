using System.Security.Claims;
using System.Text;
using Google.Apis.Drive.v3.Data;
using ChequeMicroservice.Application.Common.Models.Response;
using ChequeMicroservice.Domain.Enums;
using ChequeMicroservice.Domain.Entities;

namespace Application.UnitTests.Cheques.DataGenerator
{
    public static class FakeDataGenerator
    {

        public static List<Cheque> CreateCheques()
        {
            return new List<Cheque>()
            {

            };
        }
    }
}
