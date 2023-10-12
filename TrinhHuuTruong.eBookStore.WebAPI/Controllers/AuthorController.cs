using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.Services.Interface;
using TrinhHuuTruong.eBookStore.WebAPI.ModelViews;

namespace TrinhHuuTruong.eBookStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;


        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            this.authorService = authorService;
            this.mapper = mapper;
        }


        [HttpGet]
        [EnableQuery]
        public IEnumerable<Author> Get()
        {
            var list = authorService.GetAll().ToList();
            return list;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                var list = authorService.GetAll();
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
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await authorService.Get(id);
                return Ok(author);
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
        public async Task<IActionResult> AddAuthor(AuthorVM model)
        {
            try
            {
                var author = mapper.Map<Author>(model);
                var check = await authorService.Add(author);
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
        public async Task<IActionResult> UpdateAuthor(int id, AuthorVM model)
        {
            try
            {
                var authorDB = await authorService.Get(id);
                if (authorDB != null)
                {
                    var author = mapper.Map<Author>(model);
                    var check = await authorService.Update(id, author);
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
                        Message = "Not Found Author"
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
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var authorDB = await authorService.Get(id);
                if (authorDB != null)
                {
                    var check = await authorService.Delete(id);
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
                        Message = "Not Found Author"
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
