using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using ToolbeltUtilities.IDataAccess;

namespace ToolbeltUtilities.DataAccess
{
    public class APIDataAccess : IAPIDataAccess
    {
        private readonly ILogger<APIDataAccess> _log;
        private readonly HttpClient _client;

        public APIDataAccess(ILogger<APIDataAccess> log)
        {
            _log = log;
            _client = new HttpClient();
        }
        public T Get<T>(string address)
        {
            var response = _client.GetAsync(address).GetAwaiter().GetResult();
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }
    }
}
