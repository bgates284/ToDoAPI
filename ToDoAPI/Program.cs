using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddDbContext<Context>(opt =>
{
    //opt.UseSqlServer("Server=soul-collector.database.windows.net;Database=ToDo;User Id=wjg; Password='Bill_Gates!22'; Trusted_Connection=false;");
    opt.UseSqlServer("Server=tcp:soul-collector.database.windows.net,1433;User Id=wjg; Password='Bill_Gates!22';Initial Catalog=ToDo;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .WithExposedHeaders("content-disposition")
        .AllowAnyHeader()
        .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "MyAllowSpecificOrigins", builder =>
//     {
//         builder.AllowAnyOrigin()
//         .AllowAnyMethod()
//         .WithExposedHeaders("content-disposition")
//         .AllowAnyHeader()
//         .AllowCredentials()
//         .SetPreflightMaxAge(TimeSpan.FromSeconds(3600))
//     };
//    //builder.WithOrigins("http://localhost:8080", "http://localhost:1", "http://localhost","localhost:1",
//    //    "localhost/:1");
//});
//});
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
