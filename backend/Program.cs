using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров
builder.Services.AddControllers();

// Подключаем DbContext с PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем репозитории
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Регистрируем сервисы
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<RoleService>();

// Добавляем Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Mirea Freelance API", Version = "v1" });
});

var app = builder.Build();

// Настраиваем pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mirea Freelance API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();