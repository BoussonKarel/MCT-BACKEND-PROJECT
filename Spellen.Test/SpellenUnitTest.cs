using System;
using Moq;
using Spellen.API.DTO;
using Spellen.API.Services;
using Spellen.API.Data;
using Xunit;
using Spellen.API.Repositories;
using AutoMapper;
using System.Collections.Generic;
using Spellen.API.Models;

namespace Spellen.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Unique_Items_Should_Return_Count_4() {
            // // Mapper, repo
            // var mockMapper = new MapperConfiguration(cfg =>
            // {
            //     cfg.AddProfile(new AutoMapping());
            // });
            // var mapper = mockMapper.CreateMapper();

            // var dbContextMock = new Mock<GameContext>();
            // var itemRepository = new ItemRepository(dbContextMock.Object);
            // ItemService service = new ItemService(mapper, itemRepository);

            HelperService service = new HelperService();

            Item jantje = new Item() {
                ItemId = new Guid("7f86c075-0731-4ef3-9dc2-303d9ea73c62"),
                Name = "Jantje",
            };

            Item pietje = new Item() {
                ItemId = new Guid("93c0e5c9-9eca-4e81-96e5-69e3d7470c37"),
                Name = "Pietje"
            };

            Item bertje = new Item() {
                ItemId = new Guid("33a3a455-e54e-47e3-9774-35429347c896"),
                Name = "Bertje"
            };

            Item steventje = new Item() {
                ItemId = new Guid("59566ba4-8799-40e7-8b54-1b6dc83fc52f"),
                Name = "Steventje"
            };

            List<Item> items1 = new List<Item>() {
                jantje, pietje, bertje
            };

            List<Item> items2 = new List<Item>() {
                jantje, pietje, steventje
            };

            // 4 unieke
            List<Item> uniqueItems = service.ReturnUniqueItems(new List<List<Item>>() {items1, items2});
            Assert.True(uniqueItems.Count == 4);
        }
        
    }
}
