using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using viewcomponent_samples.Services.Interfaces;

namespace viewcomponent_samples.Services
{
    public class ApiRequestClient: IApiRequestClient
    {

        private readonly ILogger<ApiRequestClient> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ApiRequestClient(ILogger<ApiRequestClient> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> OmdbGetAsync(string apiRoute, Dictionary<string, string> keyValuePairs)
        {
            string url = QueryHelpers.AddQueryString(apiRoute, keyValuePairs);

            var response = await _clientFactory.CreateClient("omdbpApi").GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }
            
            return response;
        }

        public Task<HttpResponseMessage> OmdbGetAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> OmdbPostAsync<T>(string url, T content)
        {
            throw new NotImplementedException();
        }
    }
}
