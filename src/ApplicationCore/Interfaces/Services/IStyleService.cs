using Inkett.ApplicationCore.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface IStyleService
    {
        Task<IReadOnlyList<Style>> GetStyles();
        Task<Style> GetStyleById(int id);
        Task<Style> GetStyleWithTattoos(int id);
    }
}
