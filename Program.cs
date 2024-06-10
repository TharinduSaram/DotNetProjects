using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Newtonsoft.Json;
using northwind.MYSQL.Procedures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DB Cobtext as service to, use this service in the controller
builder.Services.AddDbContext<NorthwindContext>(
options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("NorthwindDB"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});
//After adding Microsoft.AspNetCore.Mvc.NewtonsoftJson package from dotnet or from Nuget, need to add below code in order to to get json object as output for tailored HTTP requests. 
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<NorthWindContextProcedures>(
options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("NorthwindDB"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});
//Use to Build the Application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
