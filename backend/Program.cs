using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.services;

var builder = WebApplication.CreateBuilder(args);

// GitLab integration
builder.Services.Configure<GitLabSettings>(
    builder.Configuration.GetSection("GitLab"));
builder.Services.AddHttpClient<GitLabClient>();
builder.Services.AddScoped<IGitDocumentService, GitDocumentService>();

// MVC controllers
builder.Services.AddControllers();

// PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Existing services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<RoleService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
        b.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Mirea Freelance API", Version = "v1" });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mirea Freelance API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
