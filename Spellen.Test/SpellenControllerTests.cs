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

namespace Spellen.Test
{
    public class SpellenControllerTests : IClassFixture<WebApplicationFactory<Spellen.API.Startup>>
    {
        public HttpClient Client { get; }
        public SpellenControllerTests(WebApplicationFactory<Spellen.API.Startup> fixture) {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task API_Is_Working() {
            var response = await Client.GetAsync("/WeatherForecast");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Spellen() {
            var response = await Client.GetAsync("/api/spellen");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var spellen = JsonConvert.DeserializeObject<List<Spel>>(await response.Content.ReadAsStringAsync());
            Assert.True(spellen.Count > 0);
        }
    }
}
