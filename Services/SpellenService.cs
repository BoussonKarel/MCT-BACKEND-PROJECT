using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCT_BACKEND_PROJECT.Models;
using MCT_BACKEND_PROJECT.Repositories;

namespace MCT_BACKEND_PROJECT.Services
{
    public interface ISpellenService
    {
        Task<List<Spel>> GetSpellen();
    }

    public class SpellenService : ISpellenService
    {
        private ISpelRepository _spelRepository;

        public SpellenService(ISpelRepository spelRepository)
        {
            _spelRepository = spelRepository;
        }

        public async Task<List<Spel>> GetSpellen()
        {
            return await _spelRepository.GetSpellen();
        }
    }
}
