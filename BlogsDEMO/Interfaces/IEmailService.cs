using BlogsDEMO.Application.DTOs.Email;
using System.Threading.Tasks;

namespace BlogsDEMO.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);

        Task SendMultiAsync(EmailMultiRequest request);
    }
}