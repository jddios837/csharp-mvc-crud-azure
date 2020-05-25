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
    public class ProductController : Controller
    {
        BackendApi _api = new BackendApi();
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger){
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Usuarios";

            List<ProductData> model = new List<ProductData>();
            HttpClient client = _api.Initial();

            HttpResponseMessage res = await client.GetAsync("api/products");

            if(res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ProductData>>(results);
            }
            
            return View(model);
        }

        public async Task<IActionResult> New()
        {
            ProductData model = new ProductData();

            List<UserData> modelList = new List<UserData>();
            HttpClient client = _api.Initial();

            HttpResponseMessage resList = await client.GetAsync("api/users");

            if(resList.IsSuccessStatusCode)
            {
                var resultsList = resList.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<UserData>>(resultsList);
            }


            ProductUsersViewModel myModel = new ProductUsersViewModel();
            myModel.Users = modelList;
            myModel.Product = model;

            return View(myModel);
        }

        public async  Task<IActionResult> Create(ProductData product)
        {
            HttpClient client = _api.Initial();
            string json = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PostAsync("api/products/", content);


            if (res.IsSuccessStatusCode)
            {   
                return RedirectToAction("Index");
            }

            return RedirectToAction("New", new { id = product.Id});
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ProductData model = new ProductData();
            List<UserData> modelList = new List<UserData>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/products/" + id);

            if(res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductData>(results);

                HttpResponseMessage resList = await client.GetAsync("api/users");

                if(resList.IsSuccessStatusCode)
                {
                    var resultsList = resList.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<UserData>>(resultsList);
                }
            }   

            ProductUsersViewModel myModel = new ProductUsersViewModel();
            myModel.Users = modelList;
            myModel.Product = model;

            return View(myModel);
        }

        public async  Task<IActionResult> Update(ProductData product)
        {
            HttpClient client = _api.Initial();
            string json = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync("api/products/" + product.Id, content);


            if (res.IsSuccessStatusCode)
            {   
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new { id = product.Id});
        }

        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/products/" + id);

            return RedirectToAction("Index");
        }
        
    }
}