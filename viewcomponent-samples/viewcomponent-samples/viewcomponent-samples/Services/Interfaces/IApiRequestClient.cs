using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace viewcomponent_samples.Services.Interfaces
{
    public interface IApiRequestClient
    {
        public Task<HttpResponseMessage> OmdbGetAsync(string url, Dictionary<string,string> keyValuePairs);
        public Task<HttpResponseMessage> OmdbGetAsync(string url);
        public Task<HttpResponseMessage> OmdbPostAsync<T>(string url, T content);


    }
}
