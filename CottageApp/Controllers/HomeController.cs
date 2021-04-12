using CottageApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CottageApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cottages()
        {
            List<Cottage> cottageList = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44325/api/Cottages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cottageList = JsonConvert.DeserializeObject<List<Cottage>>(apiResponse);
                }
            }
            return View(cottageList);
        }


        public ViewResult AddCottage() => View();

        [HttpPost]
        public async Task<IActionResult> AddCottage(Cottage cottage)
        {
            Cottage receivedCottage = new Cottage();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cottage), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44325/api/Cottages", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedCottage = JsonConvert.DeserializeObject<Cottage>(apiResponse);
                }
            }

            List<Cottage> cottageList = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44325/api/Cottages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cottageList = JsonConvert.DeserializeObject<List<Cottage>>(apiResponse);
                }
            }

            return View(viewName: "Cottages", cottageList);
        }

        public async Task<IActionResult> EditCottage(int id)
        {
            Cottage cottage = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44325/api/Cottages/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cottage = JsonConvert.DeserializeObject<Cottage>(apiResponse);
                }
            }
            return View(cottage);
        }

        [HttpPost]
        public async Task<IActionResult> EditCottage(Cottage cottage)
        {
            Cottage receivedCottage = new Cottage();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cottage), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44325/api/Cottages/" + cottage.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedCottage = JsonConvert.DeserializeObject<Cottage>(apiResponse);
                }
            }

            List<Cottage> cottageList = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44325/api/Cottages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cottageList = JsonConvert.DeserializeObject<List<Cottage>>(apiResponse);
                }
            }

            return View(viewName: "Cottages", cottageList);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCottage(int cottageId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44325/api/Cottages/" + cottageId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            List<Cottage> cottageList = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44325/api/Cottages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cottageList = JsonConvert.DeserializeObject<List<Cottage>>(apiResponse);
                }
            }

            return View(viewName: "Cottages", cottageList);
            // return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
