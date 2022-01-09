using System.Threading.Tasks;

namespace PropertyAgencyMobileApp.Services
{
    public interface IFeedbackService
    {
        Task<bool> Ask(string message);
        Task Inform(string message);
        Task Warn(string message);
        Task Panic(string message);
    }
}
