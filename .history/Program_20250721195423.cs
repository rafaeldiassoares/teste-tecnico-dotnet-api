using dotnet_api.Controllers;
using dotnet_api.Infra;
using dotnet_api.Infra.Repository.Addreess;
using dotnet_api.Infra.Repository.Customer;
using dotnet_api.Infra.Repository.User;
using dotnet_api.Routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Corrigir Bug de serialização de JSON
// encontrei formas de resolver, mas essa é a mais simples
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<CustomerController>();
builder.Services.AddScoped<CustomerAddressesController>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapUserRoutes();
app.MapCustomerRoutes();
app.MapCustomerAddressesRoutes();

app.UseHttpsRedirection();

app.Run();