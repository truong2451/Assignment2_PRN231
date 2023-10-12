using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.Services.Interface;
using TrinhHuuTruong.eBookStore.WebAPI.ModelViews;

namespace TrinhHuuTruong.eBookStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService bookAuthorService;
        private readonly IMapper mapper;

        public BookAuthorController(IBookAuthorService bookAuthorService, IMapper mapper)
        {
            this.bookAuthorService = bookAuthorService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllById(int bookId)
        {
            try
            {
                var list = bookAuthorService.GetAllById(bookId);
                return Ok(list);
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
        public async Task<IActionResult> GetAllBookAuthor()
        {
            try
            {
                return Ok(bookAuthorService.GetAll());
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
        public async Task<IActionResult> AddBookAuthor(BookAuthorVM model)
        {
            try
            {
                var bookAuthor = mapper.Map<BookAuthor>(model);
                bookAuthor.RoyaltyPercentage = 0;
                bookAuthor.AuthorOrder = "";
                var check = await bookAuthorService.Add(bookAuthor);
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
    }
}
