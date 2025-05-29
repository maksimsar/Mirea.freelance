using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Prometheus;

using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ---------- GitLab integration ----------
        builder.Services.Configure<GitLabSettings>(
            builder.Configuration.GetSection("GitLab"));
        builder.Services.AddHttpClient<GitLabClient>();
        builder.Services.AddScoped<IGitDocumentService, GitDocumentService>();

        // ---------- MVC controllers ----------
        builder.Services.AddControllers();

        // ---------- PostgreSQL DbContext ----------
        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // ---------- Identity ----------
        builder.Services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddRoleManager<RoleManager<IdentityRole<int>>>();
        
        // ---------- JwtOptions ----------
        builder.Services.Configure<JwtOptions>(
            builder.Configuration.GetSection(JwtOptions.SectionName));

        // ---------- JWT-аутентификация ----------
        builder.Services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => // Изменено: убрано serviceProvider, используется Action<JwtBearerOptions>
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>().Value
                ?? throw new InvalidOperationException("JWT configuration is missing.");

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.Key))
            };
        });

        builder.Services.AddAuthorization();

        // ---------- Repositories & Services ----------
        builder.Services.AddScoped<IUserRepository,    UserRepository>();
        builder.Services.AddScoped<IOrderRepository,   OrderRepository>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        builder.Services.AddScoped<IRoleRepository,    RoleRepository>();

        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<OrderService>();
        
       builder.Services.AddScoped<IOrderStatusStrategy, DefaultOrderStatusStrategy>(); 
        builder.Services.AddScoped<ProfileService>();
        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<JwtService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ITaskService, TaskService>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        // ---------- CORS ----------
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("AllowAll", p => p
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        // ---------- Swagger + bearerAuth ----------
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Mirea Freelance API", Version = "v1" });
              c.CustomSchemaIds(type => type.FullName);
            // 🛡 bearerAuth schema
            var jwtScheme = new OpenApiSecurityScheme
            {
                Name         = "Authorization",
                Description  = "Введите токен в формате **Bearer {token}**",
                In           = ParameterLocation.Header,
                Type         = SecuritySchemeType.Http,
                Scheme       = "bearer",
                BearerFormat = "JWT",
                Reference    = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "bearerAuth"
                }
            };

            c.AddSecurityDefinition("bearerAuth", jwtScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtScheme, Array.Empty<string>() }
            });
        });

        // ---------- build ----------
        var app = builder.Build();

        app.UseCors("AllowAll");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mirea Freelance API v1"));
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        //Prometheus
        app.UseMetricServer(); // Экспорт метрик по /metrics
        app.UseHttpMetrics();  // Сбор HTTP-метрик (запросы, длительность, статусы)

        app.MapControllers();
        app.Run();
    }
}
