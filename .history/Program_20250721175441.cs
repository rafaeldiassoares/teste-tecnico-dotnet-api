using dotnet_api.Controllers;
using dotnet_api.Infra;
using dotnet_api.Infra.Middleware;
using dotnet_api.Infra.Repository.Addreess;
using dotnet_api.Infra.Repository.Customer;
using dotnet_api.Infra.Repository.User;
using dotnet_api.Routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

// Register controllers
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<CustomerController>();
builder.Services.AddScoped<CustomerAddressesController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply authentication middleware to specific routes
app.UseWhen(context => 
    context.Request.Path.StartsWithSegments("/users") && 
    !context.Request.Path.StartsWithSegments("/users/login"),
    appBuilder => appBuilder.UseAuthMiddleware());

app.UseWhen(context => 
    context.Request.Path.StartsWithSegments("/customers") && 
    context.Request.Method != "GET",
    appBuilder => appBuilder.UseAuthMiddleware());

app.UseWhen(context => 
    context.Request.Path.StartsWithSegments("/customers") && 
    context.Request.Path.StartsWithSegments("/addresses", StringComparison.OrdinalIgnoreCase),
    appBuilder => appBuilder.UseAuthMiddleware());

// Register routes
app.MapUserRoutes();
app.MapCustomerRoutes();
app.MapCustomerAddressesRoutes();
app.MapAddressRoutes();

app.UseHttpsRedirection();

app.Run();