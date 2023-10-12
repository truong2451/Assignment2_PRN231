using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using TrinhHuuTruong.eBookStore.Repositories.DataAccess;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.Repositories.Repository;
using TrinhHuuTruong.eBookStore.Repositories.Repository.Interface;
using TrinhHuuTruong.eBookStore.Services;
using TrinhHuuTruong.eBookStore.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(options =>
{
    var build = new ODataConventionModelBuilder();

    build.EntitySet<Author>("Author");
    build.EntitySet<Book>("Book");
    build.EntitySet<BookAuthor>("BookAuthor");
    build.EntitySet<Publisher>("Publisher");
    build.EntitySet<Role>("Role");
    build.EntitySet<User>("User");

    options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(null)
        .AddRouteComponents("odata", build.GetEdmModel());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Assignment2API", Version = "v1" })
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<BookStoreDBContext>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();

//Test middleware
//app.Use((context, next) =>
//{
//    var endpoint = context.GetEndpoint();
//    if (endpoint == null)
//    {
//        return next(context);
//    }

//    IEnumerable<string> templates;
//    IODataRoutingMetadata metadata = endpoint.Metadata.GetMetadata<IODataRoutingMetadata>();

//    if (metadata != null)
//    {
//        templates = metadata.Template.GetTemplates();
//    }

//    return next(context);
//});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseEndpoints(endponts =>
//{
//    endponts.MapODataRoute("odata", "odata", GetEdmModel());
//});

app.Run();

//static IEdmModel GetEdmModel()
//{
//    ODataConventionModelBuilder build = new ODataConventionModelBuilder();
//    build.EntitySet<Author>("Authors");
//    build.EntitySet<Book>("Books");
//    build.EntitySet<BookAuthor>("BookAuthors");
//    build.EntitySet<Publisher>("Publishers");
//    build.EntitySet<Role>("Roles");
//    build.EntitySet<User>("Users");

//    return build.GetEdmModel();
//}