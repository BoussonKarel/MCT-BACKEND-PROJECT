using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Spellen.API.Models;
using Spellen.API.Repositories;

namespace Spellen.API.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId);
        Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories);
    }

    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }

        public async Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId)
        {
            return await _categoryRepository.GetCategoriesOfGame(gameId);
        }

        public async Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories)
        {
            return await _categoryRepository.UpdateCategoriesOfGame(gameId, categories);
        }
    }
}
