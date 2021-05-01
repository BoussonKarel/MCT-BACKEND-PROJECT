using System;
using AutoMapper;
using Spellen.API.Models;

namespace Spellen.API.DTO
{
    public class AutoMapping : Profile
    {
        public AutoMapping() {
            CreateMap<Game, GameDTO>();
            CreateMap<GameDTO, Game>();
            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<VariCombi, VariCombiDTO>();
            CreateMap<VariCombiDTO, VariCombi>();
        }
    }
}
