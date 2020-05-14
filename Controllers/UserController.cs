using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using frontend_csharp.Models;

using frontend_csharp.Helper;
using System.Net.Http;

using Newtonsoft.Json;

namespace frontend_csharp.Controllers
{
    public class UserController : Controller
    {
        BackendApi _api = new BackendApi();
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger){
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Usuarios";

            List<UserData> model = new List<UserData>();
            HttpClient client = _api.Initial();

            HttpResponseMessage res = await client.GetAsync("api/users");

            if(res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<UserData>>(results);
            }
            
            return View(model);
        }
        
    }
}