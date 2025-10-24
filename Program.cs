using Apteka_razor.Data;
using Apteka_razor.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 База данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Добавляем сервис авторизации
builder.Services.AddScoped<AuthService>();

// 🔹 Поддержка Razor Pages и сессий
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// 🔹 Создание баз данных без миграций
using (var scope = app.Services.CreateScope())
{
    var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var pharmacyContext = scope.ServiceProvider.GetRequiredService<PharmacyDbContext>();

    // Если нужно полностью пересоздать базы, раскомментировать:
    // appContext.Database.EnsureDeleted();
    // pharmacyContext.Database.EnsureDeleted();

    appContext.Database.EnsureCreated();
    pharmacyContext.Database.EnsureCreated();
}

// 🔹 Конфигурация пайплайна
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.MapRazorPages();
app.Run();
