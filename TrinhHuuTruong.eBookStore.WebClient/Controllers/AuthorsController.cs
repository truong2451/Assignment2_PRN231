using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.WebClient.Models;

namespace TrinhHuuTruong.eBookStore.WebClient.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient client = null;
        private string AuthorApiUri = "https://localhost:7256/api/Author/";

        public AuthorsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public async Task<IActionResult> ViewAuthor()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(AuthorApiUri + "GetAllAuthor");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                List<Author> authors = System.Text.Json.JsonSerializer.Deserialize<List<Author>>(strData, options);

                return View(authors);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }           
        }

        [HttpGet]
        public async Task<IActionResult> CreateAuthor()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(Author author)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(AuthorApiUri + "AddAuthor", author);
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Add Success")
                {
                    return RedirectToAction(nameof(ViewAuthor));
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View();
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditAuthor(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(AuthorApiUri + $"GetAuthorById?id={id}");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Author author = System.Text.Json.JsonSerializer.Deserialize<Author>(strData, options);

                return View(author);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuthor(int id, Author author)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(AuthorApiUri + $"UpdateAuthor?id={id}", author);
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Update Success")
                {
                    return RedirectToAction(nameof(ViewAuthor));
                }
            }
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View();
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(AuthorApiUri + $"GetAuthorById?id={id}");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Author author = System.Text.Json.JsonSerializer.Deserialize<Author>(strData, options);

                return View(author);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }           
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(AuthorApiUri + $"DeleteAuthor?id={id}");
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Delete Success")
                {
                    return RedirectToAction(nameof(ViewAuthor));
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View();
            }

            return View();
        }
    }
}
