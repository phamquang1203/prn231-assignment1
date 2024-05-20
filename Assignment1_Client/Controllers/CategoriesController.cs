using Assignment1_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Assignment1_Client.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5267/api/Categories");
        private readonly HttpClient client;

        public CategoriesController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode) {
                string data = response.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<Category>>(data);
            }

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = client.PostAsJsonAsync(client.BaseAddress, category).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Category category = null;
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(data);
            }

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = null;
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(data);
            }

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category) {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = client.PutAsJsonAsync($"{client.BaseAddress}/{category.CategoryId}", category).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(category);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category category = null;
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(data);
            }

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                HttpResponseMessage res = client.DeleteAsync($"{client.BaseAddress}/{category.CategoryId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(category);
        }

    }
}
