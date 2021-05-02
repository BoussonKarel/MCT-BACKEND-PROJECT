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
        public async Task API_GET_Games_Returns_Ok() {
            var response = await Client.GetAsync("/api/games");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task API_Item_Functionality_Works() {
            ItemDTO testItem = new ItemDTO() { Name = "Test item" };

            var response = await Client.PostAsync("/api/items", new StringContent(JsonConvert.SerializeObject(testItem), Encoding.UTF8, "application/json"));
            // Succesvol geadd = 200 OK + Guid aangemaakt
            response.StatusCode.Should().Be(HttpStatusCode.OK); // 200 OK?
            ItemDTO item = JsonConvert.DeserializeObject<ItemDTO>(await response.Content.ReadAsStringAsync());
            item.ItemId.Should().NotBe(Guid.Empty); // NON-EMPTY GUID?

            // GET this item en check if it is inserted into the database
            var responseCheckAdd = await Client.GetAsync($"/api/items/{item.ItemId}");
            responseCheckAdd.StatusCode.Should().Be(HttpStatusCode.OK);
            ItemDTO itemCheckAdd = JsonConvert.DeserializeObject<ItemDTO>(await responseCheckAdd.Content.ReadAsStringAsync());
            itemCheckAdd.ItemId.Should().Be(item.ItemId);

            // UPDATE this item
            item.Name = "Test item UPDATE";
            var responseUpdate = await Client.PutAsync("/api/items", new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));
            responseUpdate.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseCheckUpdate = await Client.GetAsync($"/api/items/{item.ItemId}");
            responseCheckUpdate.StatusCode.Should().Be(HttpStatusCode.OK);
            ItemDTO itemCheckUpdate = JsonConvert.DeserializeObject<ItemDTO>(await responseCheckUpdate.Content.ReadAsStringAsync());

            // DELETE this item
            var responseDelete = await Client.DeleteAsync($"/api/items/{item.ItemId}");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseCheckDelete = await Client.GetAsync($"/api/items/{item.ItemId}");
            responseCheckDelete.StatusCode.Should().Be(HttpStatusCode.NoContent);
            
            Assert.True(itemCheckUpdate.Name.Equals(item.Name));
        }
    }
}
