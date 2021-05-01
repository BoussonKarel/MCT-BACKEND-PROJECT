using System;
using AutoMapper;
using Spellen.API.Models;

namespace Spellen.API.DTO
{
    public class AutoMapping : Profile
    {
        public AutoMapping() {
            CreateMap<Game, GameAddDTO>();
            CreateMap<GameAddDTO, Game>();

            CreateMap<Game, GameUpdateDTO>();
            CreateMap<GameUpdateDTO, Game>();

            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}
