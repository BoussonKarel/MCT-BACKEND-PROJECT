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
        Task<List<Materiaal>> GetMateriaal();
        Task<List<Categorie>> GetCategorieen();
    }

    public class SpellenService : ISpellenService
    {
        private ISpelRepository _spelRepository;
        private IMateriaalRepository _materiaalRepository;
        private ICategorieRepository _categorieRepository;

        public SpellenService(ISpelRepository spelRepository, IMateriaalRepository materiaalRepository, ICategorieRepository categorieRepository)
        {
            _spelRepository = spelRepository;
            _materiaalRepository = materiaalRepository;
            _categorieRepository = categorieRepository;
        }

        public async Task<List<Spel>> GetSpellen()
        {
            return await _spelRepository.GetSpellen();
        }

        public async Task<List<Materiaal>> GetMateriaal() {
            return await _materiaalRepository.GetMateriaal();
        }

        public async Task<List<Categorie>> GetCategorieen() {
            return await _categorieRepository.GetCategorieen();
        }
    }
}
