using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Inkett.ApplicationCore.Interfaces.Repositories;
using System.Linq;
using Inkett.ApplicationCore.Specifications;

namespace Inkett.ApplicationCore.Services
{
    public class StyleService : IStyleService
    {
        private readonly IMemoryCache _cache;
        private readonly IAsyncRepository<Style> _styleRepository;
        private static readonly string _stylesKey = "styles";
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(3600);
        public StyleService(IMemoryCache cache,
            IAsyncRepository<Style> styleRepository)
        {
            _styleRepository = styleRepository;
            _cache = cache;
        }

        public async Task<IReadOnlyList<Style>> GetStyles()
        {
            return await _cache.GetOrCreateAsync(_stylesKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                var styles= await _styleRepository.ListAllAsync();
                return styles;
            });
        }
        public async Task<Style> GetStyleById(int id)
        {
            var styles = await GetStyles();

            return styles.FirstOrDefault(s=>s.Id==id);
        }
        public async Task<Style> GetStyleWithTattoos(int id)
        {
            var spec = new StyleWithTattoosSpecification(id);
            return await _styleRepository.GetSingleBySpec(spec);
        }
    }
}
