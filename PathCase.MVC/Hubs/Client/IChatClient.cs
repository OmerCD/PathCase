using System.Threading.Tasks;

namespace PathCase.MVC.Hubs.Client
{
    public interface IChatClient
    {
        Task Message(string message, string sender);
        Task ServerMessage(string message);
    }
}