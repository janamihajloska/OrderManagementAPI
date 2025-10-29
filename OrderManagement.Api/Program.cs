using Microsoft.EntityFrameworkCore;
using OrderManagement.Core.Interfaces;
using OrderManagement.Core.Services;
using OrderManagementSystem;
using OrderManagement.Core.Mapping;

var builder = WebApplication.CreateBuilder(args);

// ---- Configuration ----
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---- DbContext ----
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---- Repositories & Services (DI) ----
// Infrastructure (repositories)
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Application services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


// ---- AutoMapper ----
builder.Services.AddAutoMapper(typeof(OrderManagement.Core.Mapping.MappingProfile).Assembly);

var app = builder.Build();

// ---- Swagger in dev ----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---- Routing ----
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();