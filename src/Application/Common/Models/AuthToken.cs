using System.Text.Json.Serialization;

namespace ChequeMicroservice.Application.Common.Models
{
    public class AuthToken
    {
        [JsonIgnore]
        public string AccessToken { get; set; }
    }
}