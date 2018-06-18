using System.Threading.Tasks;
using static LULUTest.Util.EmailService;

namespace LULUTest.Util.Abstract
{
    public interface IEmailService
    {
       Task SendEmailAsync(string recepientName, string recepientEmail, string subject, string body, bool isHtml = true);
    }
}