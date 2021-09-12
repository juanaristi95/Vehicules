using Vehicules.Common.Models;

namespace Vehicules.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
