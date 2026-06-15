using EmployeeApp.Api.Data;
using EmployeeApp.Api.Mappings;
using EmployeeApp.Api.Middlewares;
using EmployeeApp.Api.Repositories;
using EmployeeApp.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"));
});
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.UseMiddleware<MyMiddleware>();
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("Hello world before processing request");
//    var comp=Environment.GetEnvironmentVariable("Company");
//    Console.WriteLine(comp + "=================");
//    await next(context);
//    Console.WriteLine("Hello World after processing the request");

//});

app.Run();
