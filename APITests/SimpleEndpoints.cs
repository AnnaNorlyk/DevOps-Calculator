using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace APITests
{
    [TestFixture]
    public class SimpleEndpointsTests : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public SimpleEndpointsTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Add()
        {
            // Arrange
            var url = "/api/simple/add?a=2&b=3";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Is.EqualTo("5"));
        }

        [Test]
        public async Task Subtract()
        {
            // Arrange
            var url = "/api/simple/subtract?a=9&b=4";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Is.EqualTo("5"));
        }

        [Test]
        public async Task Factorial()
        {
            // Arrange
            var url = "/api/simple/factorial?a=5";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Is.EqualTo("120"));
        }

        [Test]
        public async Task Prime()
        {
            // Arrange
            var url = "/api/simple/prime?a=5";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content, Is.EqualTo("true"));
        }

        [Test]
        public async Task Divide()
        {
            // Arrange
            var url = "/api/simple/divide?a=10&b=0";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
