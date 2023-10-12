using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;

namespace TrinhHuuTruong.eBookStore.WebClient.Controllers
{
    public class PublishersController : Controller
    {
        private readonly HttpClient client = null;
        private string PublisherApiUri = "https://localhost:7256/api/Publisher/";

        public PublishersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public async Task<IActionResult> ViewPublisher()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(PublisherApiUri + "GetAllPublisher");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                List<Publisher> publishers = System.Text.Json.JsonSerializer.Deserialize<List<Publisher>>(strData, options);

                return View(publishers);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }           
        }

        [HttpGet]
        public async Task<IActionResult> CreatePublisher()
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
        public async Task<IActionResult> CreatePublisher(Publisher publisher)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(PublisherApiUri + "AddPublisher", publisher);
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Add Success")
                {
                    return RedirectToAction(nameof(ViewPublisher));
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View();
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditPublisher(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(PublisherApiUri + $"GetPublisherById?id={id}");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Publisher publisher = System.Text.Json.JsonSerializer.Deserialize<Publisher>(strData, options);

                return View(publisher);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPublisher(int id, Publisher publisher)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(PublisherApiUri + $"UpdatePublisher?id={id}", publisher);
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Update Success")
                {
                    return RedirectToAction(nameof(ViewPublisher));
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
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
                HttpResponseMessage response = await client.GetAsync(PublisherApiUri + $"GetPublisherById?id={id}");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Publisher publisher = System.Text.Json.JsonSerializer.Deserialize<Publisher>(strData, options);

                return View(publisher);
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
            HttpResponseMessage response = await client.DeleteAsync(PublisherApiUri + $"DeletePublisher?id={id}");
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Delete Success")
                {
                    return RedirectToAction(nameof(ViewPublisher));
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
