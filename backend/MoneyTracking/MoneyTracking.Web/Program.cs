using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data;
using MoneyTracking.Data.Entities;
using MoneyTracking.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
// Inject repositories
builder.Services.AddScoped<IRepositoryBase<User>, UserRepository>();
builder.Services.AddScoped<IRepositoryBase<IncomeCategory>, IncomeCategoryRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoneyTrackingContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    });

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