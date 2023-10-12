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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisherService;
        private readonly IMapper mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            this.publisherService = publisherService;
            this.mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<Publisher> Get()
        {
            var list = publisherService.GetAll();
            return list;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublisher()
        {
            try
            {
                var list = publisherService.GetAll();
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
        public async Task<IActionResult> GetPublisherById(int id)
        {
            try
            {
                var publisher = await publisherService.Get(id);
                return Ok(publisher);
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
        public async Task<IActionResult> AddPublisher(PublisherVM model)
        {
            try
            {
                var publisher = mapper.Map<Publisher>(model);
                var check = await publisherService.Add(publisher);
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
        public async Task<IActionResult> UpdatePublisher(int id, PublisherVM model)
        {
            try
            {
                var publisherDB = await publisherService.Get(id);
                if (publisherDB != null)
                {
                    var publisher = mapper.Map<Publisher>(model);
                    var check = await publisherService.Update(id, publisher);
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
                        Message = "Not Found Publisher"
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
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                var publisherDB = await publisherService.Get(id);
                if (publisherDB != null)
                {
                    var check = await publisherService.Delete(id);
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
                        Message = "Not Found Publisher"
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
