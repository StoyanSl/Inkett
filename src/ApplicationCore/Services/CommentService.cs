using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Services
{
    public class CommentService: ICommentService
    {
        private readonly IAsyncRepository<Comment> _commentRepository;
        public CommentService(IAsyncRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<Comment> CreateComment(int profileId, int tattoId, string text)
        {
            var comment = new Comment() {
                ProfileId = profileId,
                TattooId = tattoId,
                Text = text
            };
          return await _commentRepository.AddAsync(comment);
        }
    }
}
