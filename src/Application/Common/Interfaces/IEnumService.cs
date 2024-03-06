using System;
namespace ChequeMicroservice.Application.Common.Interfaces
{
    public interface IEnumService
    {
        public string GenerateDisplayName(Enum value);
        public string DisplayCamelCase(string value);
    }
}
