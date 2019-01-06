using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface ICommentService
    {
        Task CreateComment(int ProfileId, int TattoId, string Text);
    }
}
