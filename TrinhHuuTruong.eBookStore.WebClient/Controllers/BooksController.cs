using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.WebClient.Models;

namespace TrinhHuuTruong.eBookStore.WebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient client = null;
        private string BookApiUri = "https://localhost:7256/api/Book/";

        public BooksController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public async Task<List<Publisher>> GetAllPublisher()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7256/api/Publisher/GetAllPublisher");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<Publisher> publishers = System.Text.Json.JsonSerializer.Deserialize<List<Publisher>>(strData, options);

            return publishers;
        }

        [HttpGet]
        public async Task<IActionResult> ViewBook(string? search)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                //HttpResponseMessage response = await client.GetAsync(BookApiUri + $"GetAllBook?search={search}");
                //string strData = await response.Content.ReadAsStringAsync();

                //var options = new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //};

                //List<Book> books = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(strData, options);
                var api = "https://localhost:7256/odata/Book";
                if(search != null)
                {
                    if(double.TryParse(search, out double price))
                    {
                        api += $"?$filter=Price eq {price}";
                    }
                    else
                    {
                        api += $"?$filter=BookName eq '{search}'";
                    }
                }
                HttpResponseMessage response = await client.GetAsync(api);
                List<Book> books = new List<Book>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    string strData = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
                    books = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json["value"].ToString(), options);
                }
                return View(new BooksVM
                {
                    Search = search,
                    Books = books
                });
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "PublisherId", "PublisherName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (book.Price < 0 || book.Royalty < 0 || book.YtdSales < 0)
            {
                if (book.Price < 0)
                {
                    ViewData["Price"] = "Price cannot be less than 0";
                }
                if (book.Royalty < 0)
                {
                    ViewData["Royalty"] = "Royalty cannot be less than 0";
                }
                if (book.YtdSales < 0)
                {
                    ViewData["YtdSales"] = "YtdSales cannot be less than 0";
                }
            }

            HttpResponseMessage response = await client.PostAsJsonAsync(BookApiUri + "AddBook", book);
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Add Success")
                {
                    return RedirectToAction(nameof(ViewBook));
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "PublisherId", "PublisherName");
                return View();
            }

            ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "PublisherId", "PublisherName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(BookApiUri + $"GetBookById?id={id}");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Book book = System.Text.Json.JsonSerializer.Deserialize<Book>(strData, options);

                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "PublisherId", "PublisherName");

                return View(book);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, Book book)
        {
            if (book.Price < 0 || book.Royalty < 0 || book.YtdSales < 0)
            {
                if (book.Price < 0)
                {
                    ViewData["Price"] = "Price cannot be less than 0";
                }
                if (book.Royalty < 0)
                {
                    ViewData["Royalty"] = "Royalty cannot be less than 0";
                }
                if (book.YtdSales < 0)
                {
                    ViewData["YtdSales"] = "YtdSales cannot be less than 0";
                }
            }

            HttpResponseMessage response = await client.PutAsJsonAsync(BookApiUri + $"UpdateBook?id={id}", book);
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Update Success")
                {
                    return RedirectToAction(nameof(ViewBook));
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "PublisherId", "PublisherName");
                return View();
            }

            ViewData["PublisherId"] = new SelectList(await GetAllPublisher(), "PublisherId", "PublisherName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(BookApiUri + $"GetBookById?id={id}");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Book book = System.Text.Json.JsonSerializer.Deserialize<Book>(strData, options);

                return View(book);
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
            HttpResponseMessage response = await client.DeleteAsync(BookApiUri + $"DeleteBook?id={id}");
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Delete Success")
                {
                    return RedirectToAction(nameof(ViewBook));
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception(json["message"].ToString());
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailBook(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewData["Role"] = role;
            if (role == "Admin")
            {
                HttpResponseMessage response = await client.GetAsync(BookApiUri + $"GetBookById?id={id}");
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Book book = System.Text.Json.JsonSerializer.Deserialize<Book>(strData, options);

                ViewData["AuthorId"] = new SelectList(await GetAllAuthor(), "AuthorId", "LastName");

                return View(new BooksDetailVM
                {
                    Book = book,
                    BookAuthors = await GetAllBookAuthorById(id)
                });
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DetailBook(int bookId, int authorId)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("", new {bookId = bookId, authorId = authorId });
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Add Success")
                {
                    return RedirectToAction(nameof(DetailBook));
                }
            }
            if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return View();
            }
            return View();
        }

        public async Task<List<Author>> GetAllAuthor()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7256/api/Author/GetAllAuthor");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<Author> authors = System.Text.Json.JsonSerializer.Deserialize<List<Author>>(strData, options);

            return authors;
        }

        [HttpGet]
        public async Task<List<BookAuthor>> GetAllBookAuthorById(int bookId)
        {
            HttpResponseMessage response = await client.GetAsync("" + $"?bookId={bookId}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<BookAuthor> bookAuthors = System.Text.Json.JsonSerializer.Deserialize<List<BookAuthor>>(strData, options);

            return bookAuthors;
        }
    }
}
