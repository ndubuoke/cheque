using ChequeMicroservice.Application.Common.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendBulkEmail(List<EmailVM> emailVms);
        Task<string> SendEmail(EmailVM emailVm);
    }
}
