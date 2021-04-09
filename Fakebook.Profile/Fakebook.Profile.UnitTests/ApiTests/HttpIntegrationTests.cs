using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Fakebook.Profile.RestApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Fakebook.Profile.UnitTests.ApiTests
{
    public class HttpIntegrationTests : IClassFixture<SqliteWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HttpIntegrationTests(SqliteWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        internal async Task UploadImageSize(long length, bool successExpected)
        {
            byte[] data = new byte[length];
            var stream = new MemoryStream(data);
            var formContent = new MultipartFormDataContent
            {
                {new StreamContent(stream), "profilePicture", "file.jpeg"}
            };

            HttpResponseMessage message = await _client.PostAsync("/api/ProfilePicture", formContent);
            if (successExpected)
            {
                Assert.Equal(201, (int)message.StatusCode);
            }
            else
            {
                Assert.Equal(500, (int)message.StatusCode);
            }
        }


        [Fact]
        public async Task UploadValidImageSize()
        {
            await UploadImageSize(1000, true);
        }

        [Fact]
        public async Task UploadInvalidImageSize()
        {
            await UploadImageSize(30 * 1024 * 1024, false);
        }
    }
}
