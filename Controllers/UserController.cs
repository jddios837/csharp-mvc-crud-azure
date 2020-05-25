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

        public IActionResult New()
        {
            UserData model = new UserData();

            return View(model);
        }

        public async  Task<IActionResult> Create(UserData user)
        {
            HttpClient client = _api.Initial();
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PostAsync("api/users/", content);


            if (res.IsSuccessStatusCode)
            {   
                return RedirectToAction("Index");
            }

            return RedirectToAction("New", new { id = user.Id});
        }

        public async Task<IActionResult> Edit(int? id)
        {
            UserData model = new UserData();
            HttpClient client = _api.Initial();

            HttpResponseMessage res = await client.GetAsync("api/users/" + id);

            if(res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserData>(results);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/users/" + id);

            return RedirectToAction("Index");
        }

        public async  Task<IActionResult> Update(UserData user)
        {
            HttpClient client = _api.Initial();
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync("api/users/" + user.Id, content);


            if (res.IsSuccessStatusCode)
            {   
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new { id = user.Id});
        }
    }
}