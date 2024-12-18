using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mirea.Freelance.backend.data;
using System.Text;
using Mirea.Freelance.backend.services;


var builder = WebApplication.CreateBuilder(args);

// Добавляем конфигурацию для подключения к PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем сервисы
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<RoleService>();


var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering(); // Позволяет читать тело запроса несколько раз
    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
    {
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0; // Возвращаем позицию потока
        Console.WriteLine($"Request Body: {body}"); // Логируем тело запроса
    }
    await next();
});


// Добавляем эндпоинт для создания пользователя
app.MapPost("api/users", async (UserService userService, [FromBody] CreateUserDto createUserDto) =>
{
    if (string.IsNullOrWhiteSpace(createUserDto.Username) ||
    string.IsNullOrWhiteSpace(createUserDto.Password) ||
    string.IsNullOrWhiteSpace(createUserDto.Email))
{
    return Results.BadRequest("All fields are required.");
}



    Console.WriteLine($"Received Username: {createUserDto.Username}");
    Console.WriteLine($"Received Password: {createUserDto.Password}");
    Console.WriteLine($"Received Email: {createUserDto.Email}");

    var (success, message, user) = await userService.CreateUserAsync(createUserDto.Username, createUserDto.Password, createUserDto.Email);
    if (success)
    {
        return Results.Created($"/users/{user?.Id}", user);
    }
    return Results.BadRequest(message);
});


app.MapGet("/api/users/{id}", async (UserService userService, int id) =>
{
    var user = await userService.GetUserByIdAsync(id);
    if (user == null)
    {
        return Results.NotFound(new { message = "Пользователь не найден" });
    }


    var userDto = new
    {
        user.Id,
        user.Login,
        RegistrationDate = user.RegistrationDate.ToString("o") // ISO 8601
    };

    return Results.Ok(userDto);
});

// Эндпоинт для удаления пользователя
app.MapDelete("/api/users/{id}", async (UserService userService, int id) =>
{
    var (success, message) = await userService.DeleteUserAsync(id);

    if (success)
    {
        return Results.Ok(message);
    }

    return Results.NotFound(message);
});

//Эндпоинт для поиска профиля по id пользователя
app.MapGet("/api/profiles/{id}", async (ProfileService profileService, int id) =>
{
    var profile = await profileService.GetProfileByIdAsync(id);

    if (profile != null)
    {
        return Results.Ok(profile);
    }

    return Results.NotFound("Профиль не найден");
});

// Эндпоинт для обновления профиля пользователя
app.MapPatch("/api/profiles/{id}", async (ProfileService profileService, int id, [FromBody] Profile updatedProfile) =>
{
    var (success, message) = await profileService.UpdateProfileAsync(id, updatedProfile);

    if (success)
    {
        return Results.Ok(message);
    }

    return Results.NotFound(message);
});
//
//
//Task

// Создание задачи
app.MapPost("/api/tasks", async (TaskService taskService, [FromBody] Task task) =>
{
    var createdTask = await taskService.CreateTaskAsync(task);
    return Results.Created($"/api/tasks/{createdTask.Id}", createdTask);
});

// Получение задачи по ID
app.MapGet("/api/tasks/{id}", async (TaskService taskService, int id) =>
{
    var task = await taskService.GetTaskByIdAsync(id);
    return task != null ? Results.Ok(task) : Results.NotFound("Задача не найдена");
});

// Обновление задачи
app.MapPut("/api/tasks/{id}", async (TaskService taskService, int id, [FromBody] Task updatedTask) =>
{
    var task = await taskService.UpdateTaskAsync(id, updatedTask);
    return task != null ? Results.Ok(task) : Results.NotFound("Задача не найдена");
});

// Удаление задачи
app.MapDelete("/api/tasks/{id}", async (TaskService taskService, int id) =>
{
    var success = await taskService.DeleteTaskAsync(id);
    return success ? Results.Ok("Задача удалена") : Results.NotFound("Задача не найдена");
});


//Эндпоинт для создания роли
app.MapPost("/api/roles", async (RoleService roleService, [FromBody] Role role) =>
{
    var createdRole = await roleService.CreateRoleAsync(role);
    return Results.Created($"/api/roles/{createdRole.Id}", createdRole);
}

//Эндпоинт для изменения роли
app.MapPut("/api/roles/{id}", async (RoleService roleService, int id, [FromBody] Role updatedRole) =>
{
    var role = await roleService.UpdateRoleAsync(id, updatedRole);
    return role != null ? Results.Ok(role) : Results.NotFound("Роль не найдена");
}

//
//
//


// Запуск приложения
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

record CreateUserDto(string Username, string Password, string Email);
