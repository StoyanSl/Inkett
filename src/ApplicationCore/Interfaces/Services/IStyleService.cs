using Inkett.ApplicationCore.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface IStyleService
    {
        Task<IReadOnlyList<Style>> GetStyles();
    }
}
