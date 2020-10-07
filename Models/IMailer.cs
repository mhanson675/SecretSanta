using System.Threading.Tasks;

namespace SecretSanta.Models
{
    public interface IMailer
    {
        Task SendEmailAsync(SantaMessage message);
    }
}
