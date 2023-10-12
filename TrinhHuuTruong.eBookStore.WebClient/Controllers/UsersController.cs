using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TrinhHuuTruong.eBookStore.WebClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client = null;
        private string UserApiUri = "https://localhost:7256/api/User/";

        public UsersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(UserApiUri + "Login", new {email = email, password = password});
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                TempData["LoginFail"] = json["message"].ToString();
                return RedirectToAction(nameof(Login));
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("Role", json["role"].ToString());

                if (json["role"].ToString() == "Admin")
                {
                    //HttpContext.Session.SetString("Name", "Admin");
                    return RedirectToAction("ViewBook", "Books");
                }
                if(json["role"].ToString() == "User")
                {
                    //HttpContext.Session.SetString("Name", "User");
                    return RedirectToAction("");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Role");

            return RedirectToAction(nameof(Login));
        }
    }
}
