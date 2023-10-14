using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrinhHuuTruong.eBookStore.Services.Interface;
using TrinhHuuTruong.eBookStore.WebAPI.ModelViews;

namespace TrinhHuuTruong.eBookStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginVM model)
        //{
        //    try
        //    {
        //        var builder = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json", true, true);

        //        var config = builder.Build();
        //        var admin = config.GetSection("Admin");

        //        if (admin["Email"] == model.Email && admin["Password"] == model.Password)
        //        {
        //            return StatusCode(200, new
        //            {
        //                Status = "Success",
        //                Role = "Admin",
        //                Message = "Login Admin Success"
        //            });
        //        }

        //        var user = await userService.CheckLogin(model.Email);
        //        if(user != null)
        //        {
        //            if(user.Password == model.Password)
        //            {
        //                return StatusCode(200, new
        //                {
        //                    Status = "Success",
        //                    Role = "User",
        //                    Message = "Login User Success"
        //                });
        //            }
        //            else
        //            {
        //                return StatusCode(400, new
        //                {
        //                    Status = "Error",
        //                    Message = "Invalid Password"
        //                });
        //            }
        //        }

        //        return StatusCode(404, new
        //        {
        //            Status = "Error",
        //            Message = "Login Fail"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            Status = "Error",
        //            Message = ex.Message
        //        });
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true);

                var config = builder.Build();
                var admin = config.GetSection("Admin");

                if (admin["Email"] == email && admin["Password"] == password)
                {
                    return StatusCode(200, new
                    {
                        Status = "Success",
                        Role = "Admin",
                        Message = "Login Admin Success"
                    });
                }

                var user = await userService.CheckLogin(email);
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        return StatusCode(200, new
                        {
                            Status = "Success",
                            Role = "User",
                            Message = "Login User Success"
                        });
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            Status = "Error",
                            Message = "Invalid Password"
                        });
                    }
                }

                return StatusCode(404, new
                {
                    Status = "Error",
                    Message = "Login Fail"
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
