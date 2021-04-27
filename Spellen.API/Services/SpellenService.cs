using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spellen.API.Models;
using Spellen.API.Repositories;

namespace Spellen.API.Services
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
