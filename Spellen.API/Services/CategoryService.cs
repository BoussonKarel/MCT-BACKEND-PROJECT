using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Spellen.API.DTO;
using Spellen.API.Models;
using Spellen.API.Repositories;

namespace Spellen.API.Services
{
    public interface ICategoryService
    {
        Task<Category> AddCategory(CategoryDTO category);
        Task DeleteCategory(Guid categoryId);
        Task<List<Category>> GetCategories(string searchQuery = null);
        Task<Category> GetCategoryById(Guid categoryId);
        Task<List<GameCategory>> GetCategoriesOfGame(Guid gameId);
        Task<List<GameCategory>> UpdateCategoriesOfGame(Guid gameId, List<GameCategory> categories);
        Task UpdateCategory(CategoryDTO category);
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

        public async Task<List<Category>> GetCategories(string searchQuery = null)
        {
            return await _categoryRepository.GetCategories(searchQuery);
        }

        public async Task<Category> GetCategoryById(Guid categoryId) {
            return await _categoryRepository.GetCategoryById(categoryId);
        }

        public async Task<Category> AddCategory(CategoryDTO category)
        {
            Category newCategory = _mapper.Map<Category>(category);

            return await _categoryRepository.AddCategory(newCategory);
        }

        public async Task UpdateCategory(CategoryDTO category)
        {
            Category catToUpdate = _mapper.Map<Category>(category);
            await _categoryRepository.UpdateCategory(catToUpdate);
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            await _categoryRepository.DeleteCategory(categoryId);
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
