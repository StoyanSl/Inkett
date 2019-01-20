using Inkett.ApplicationCore.Entitites;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface ICommentService
    {
        Task<Comment> CreateComment(int ProfileId, int TattoId, string Text);
    }
}
