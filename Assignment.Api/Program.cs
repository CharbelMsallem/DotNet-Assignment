using Assignment.Data;
using Microsoft.EntityFrameworkCore;
using Assignment.Services.Interfaces;
using Assignment.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers
builder.Services.AddControllers();

// 2. Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Register the Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. REGISTER CUSTOM SERVICE or REGISTERED SERVICES
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IRegisteredUserService, RegisteredUserService>();

var app = builder.Build();

// 5. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();