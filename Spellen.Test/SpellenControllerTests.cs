using System.Text;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Spellen.API.Models;
using System.Net.Http.Headers;
using Spellen.API.DTO;
using AutoMapper;

namespace Spellen.Test
{
    public class SpellenControllerTests : IClassFixture<WebApplicationFactory<Spellen.API.Startup>>
    {
        public HttpClient Client { get; }
        public SpellenControllerTests(WebApplicationFactory<Spellen.API.Startup> fixture) {
            Client = fixture.CreateClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkNYUGhpaW5BWWRXeGZjdWRGRVZNRyJ9.eyJpc3MiOiJodHRwczovL2thcmVsYi5ldS5hdXRoMC5jb20vIiwic3ViIjoiVFc5WEVwQ2ZTYVVRUEM1NUg5QlZVdFA1R1VpRzFVcjJAY2xpZW50cyIsImF1ZCI6Imh0dHA6Ly9TcGVsbGVuQVBJIiwiaWF0IjoxNjE5OTUzNDM0LCJleHAiOjE2MjAwMzk4MzQsImF6cCI6IlRXOVhFcENmU2FVUVBDNTVIOUJWVXRQNUdVaUcxVXIyIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIn0.UwZQ3xdsNF19tCpJdTfKydUmNVv8cXhMjJcbwKo3IszqK7_b19DStZIoUxeWYR-G77pPsZUEFBHeA7TJ-Ppk33qGfH8LoE6onvA8Z-OIOGIW5xZXbUTzMdgtDQyP0RQHzypfWuSMp50p4YLOYba-AQ_4pCkD3IYqqxZeUmbA2AfUF2lVmsy99aX_kNIGLmQDGTfupC1mW0nml2lIOnYi5sf-UTm1g9wH2ILHonXzB72870HQz1OtNkANohSalYS8mFBGR41FeoXpBhdzhtYiHy-AKDHpZp6NrlytM3nbshouFeTrsOESX707s41PhpvbMXCfc_koJSpmoE9vofiCxw");
        }

        [Fact]
        public async Task API_Is_Working() {
            var response = await Client.GetAsync("/WeatherForecast");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task API_GET_Games_Should_Return_Ok() {
            var response = await Client.GetAsync("/api/games");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task API_GET_Items_Should_Return_Ok() {
            var response = await Client.GetAsync("/api/items");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task API_GET_Categories_Should_Return_Ok() {
            var response = await Client.GetAsync("/api/categories");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task API_Item_CRUD_Works() {
            ItemDTO testItem = new ItemDTO() { Name = "Test item 4214" };

            var response = await Client.PostAsync("/api/items", new StringContent(JsonConvert.SerializeObject(testItem), Encoding.UTF8, "application/json"));
            // Succesvol geadd = 200 OK + item gereturned
            response.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            ItemDTO addedItem = JsonConvert.DeserializeObject<ItemDTO>(await response.Content.ReadAsStringAsync());
            addedItem.ItemId.Should().NotBeEmpty(); // NON-EMPTY GUID?

            // GET item by id
            var responseByid = await Client.GetAsync($"/api/items/{addedItem.ItemId}");
            responseByid.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            ItemDTO itemById = JsonConvert.DeserializeObject<ItemDTO>(await responseByid.Content.ReadAsStringAsync());
            itemById.ItemId.Should().Be(addedItem.ItemId); // GUID CORRECT?

            // SEARCH for item
            var responseSearch = await Client.GetAsync($"/api/items?search={"4214"}");
            responseSearch.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            List<ItemDTO> searchResults = JsonConvert.DeserializeObject<List<ItemDTO>>(await responseSearch.Content.ReadAsStringAsync());
            searchResults.Should().HaveCountGreaterThan(0);

            // UPDATE this item
            addedItem.Name = "Test item UPDATE";
            var responseUpdate = await Client.PutAsync("/api/items", new StringContent(JsonConvert.SerializeObject(addedItem), Encoding.UTF8, "application/json"));
            responseUpdate.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            ItemDTO itemUpdated = JsonConvert.DeserializeObject<ItemDTO>(await responseUpdate.Content.ReadAsStringAsync());
            itemUpdated.Name.Should().Be(addedItem.Name);

            // DELETE this item
            var responseDelete = await Client.DeleteAsync($"/api/items/{addedItem.ItemId}");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task API_Category_CRUD_Works() {
            CategoryDTO testCategory = new CategoryDTO() { Name = "Test category 4214" };

            var response = await Client.PostAsync("/api/categories", new StringContent(JsonConvert.SerializeObject(testCategory), Encoding.UTF8, "application/json"));
            // Succesvol geadd = 200 OK + categorie gereturned
            response.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            CategoryDTO addedCategory = JsonConvert.DeserializeObject<CategoryDTO>(await response.Content.ReadAsStringAsync());
            addedCategory.CategoryId.Should().NotBeEmpty(); // NON-EMPTY GUID?

            // GET category by id
            var responseByid = await Client.GetAsync($"/api/categories/{addedCategory.CategoryId}");
            responseByid.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            CategoryDTO categoryById = JsonConvert.DeserializeObject<CategoryDTO>(await responseByid.Content.ReadAsStringAsync());
            categoryById.CategoryId.Should().Be(addedCategory.CategoryId); // GUID CORRECT?

            // SEARCH for category
            var responseSearch = await Client.GetAsync($"/api/categories?search={"4214"}");
            responseSearch.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            List<CategoryDTO> searchResults = JsonConvert.DeserializeObject<List<CategoryDTO>>(await responseSearch.Content.ReadAsStringAsync());
            searchResults.Should().HaveCountGreaterThan(0);

            // UPDATE this category
            addedCategory.Name = "Test category UPDATE";
            var responseUpdate = await Client.PutAsync("/api/categories", new StringContent(JsonConvert.SerializeObject(addedCategory), Encoding.UTF8, "application/json"));
            responseUpdate.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            ItemDTO categoryUpdated = JsonConvert.DeserializeObject<ItemDTO>(await responseUpdate.Content.ReadAsStringAsync());
            categoryUpdated.Name.Should().Be(addedCategory.Name);

            // DELETE this category
            var responseDelete = await Client.DeleteAsync($"/api/categories/{addedCategory.CategoryId}");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task API_Game_CRUD_Works() {
            GameAddDTO testGame = new GameAddDTO() {
                Name = "Test game 4214",
                Explanation = "Test game zou moeten werken",
                Duration = "testlengte",
                Terrain = new List<string>() {"test", "test"},
                AgeFrom = 5,
                AgeTo = 10,
                PlayersMin = 2,
                PlayersMax = 20
            };

            var response = await Client.PostAsync("/api/games", new StringContent(JsonConvert.SerializeObject(testGame), Encoding.UTF8, "application/json"));
            // Succesvol geadd = 200 OK + Game gereturned
            response.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            Game addedGame = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
            addedGame.GameId.Should().NotBeEmpty(); // NON-EMPTY GUID?

            // GET game by id
            var responseByid = await Client.GetAsync($"/api/games/{addedGame.GameId}");
            responseByid.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            Game gameById = JsonConvert.DeserializeObject<Game>(await responseByid.Content.ReadAsStringAsync());
            gameById.GameId.Should().Be(addedGame.GameId); // GUID CORRECT?

            // SEARCH for game
            var responseSearch = await Client.GetAsync($"/api/games?search={"4214"}");
            responseSearch.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            List<Game> searchResults = JsonConvert.DeserializeObject<List<Game>>(await responseSearch.Content.ReadAsStringAsync());
            searchResults.Should().HaveCountGreaterThan(0);

            // UPDATE this game
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            var mapper = mockMapper.CreateMapper();
            
            GameUpdateDTO gameToUpdate = mapper.Map<GameUpdateDTO>(addedGame);
            gameToUpdate.Name = "Test game UPDATE";
            gameToUpdate.AgeFrom = 3;
            gameToUpdate.PlayersMax = 99;

            var responseUpdate = await Client.PutAsync("/api/games", new StringContent(JsonConvert.SerializeObject(gameToUpdate), Encoding.UTF8, "application/json"));
            responseUpdate.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            Game categoryUpdated = JsonConvert.DeserializeObject<Game>(await responseUpdate.Content.ReadAsStringAsync());
            categoryUpdated.Name.Should().Be(gameToUpdate.Name);

            // DELETE this game
            var responseDelete = await Client.DeleteAsync($"/api/games/{addedGame.GameId}");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
