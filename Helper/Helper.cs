using System.Net.Http;
using System;

namespace frontend_csharp.Helper
{
    public class BackendApi
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://test-crud.azurewebsites.net/");
            // client.BaseAddress = new Uri("https://localhost:5001/");
            // client.BaseAddress = new Uri("http://localhost:5000/");
            return client;
        }
    }
}