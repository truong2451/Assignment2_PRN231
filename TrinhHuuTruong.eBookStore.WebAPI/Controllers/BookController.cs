using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.Services;
using TrinhHuuTruong.eBookStore.Services.Interface;
using TrinhHuuTruong.eBookStore.WebAPI.ModelViews;

namespace TrinhHuuTruong.eBookStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IMapper mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            this.bookService = bookService;
            this.mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<Book> Get()
        {
            var list = bookService.GetAll();
            return list;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook(string? search)
        {
            try
            {
                if(search != null)
                {
                    return Ok(bookService.Search(search));
                }
                else
                {
                    var list = bookService.GetAll();
                    return Ok(list);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await bookService.Get(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookVM model)
        {
            try
            {
                var book = mapper.Map<Book>(model);
                var check = await bookService.Add(book);
                return check ? StatusCode(200, new
                {
                    Message = "Add Success"
                }) : StatusCode(500, new
                {
                    Message = "Add Fail"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(int id, BookVM model)
        {
            try
            {
                var bookDB = await bookService.Get(id);
                if (bookDB != null)
                {
                    var book = mapper.Map<Book>(model);
                    var check = await bookService.Update(id, book);
                    return check ? StatusCode(200, new
                    {
                        Message = "Update Success"
                    }) : StatusCode(500, new
                    {
                        Message = "Update Fail"
                    });
                }
                else
                {
                    return StatusCode(404, new
                    {
                        Message = "Not Found Book"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var bookDB = await bookService.Get(id);
                if (bookDB != null)
                {
                    var check = await bookService.Delete(id);
                    return check ? StatusCode(200, new
                    {
                        Message = "Delete Success"
                    }) : StatusCode(500, new
                    {
                        Message = "Delete Fail"
                    });
                }
                else
                {
                    return StatusCode(404, new
                    {
                        Message = "Not Found Book"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
    }
}
