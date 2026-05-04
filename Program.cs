using BuildingStore.Web.Components;
using BuildingStore.Web.Data;
using BuildingStore.Web.Models;
using BuildingStore.Web.Repositories;
using BuildingStore.Web.Services;
using BuildingStore.Web.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddValidatorsFromAssemblyContaining<MaterialValidator>();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserSessionService, UserSessionService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery(); 


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StoreContext>();

    
    db.Database.Migrate();

    
    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new Category { Name = "Сухие смеси" },
            new Category { Name = "Кирпич и блоки" },
            new Category { Name = "Отделочные материалы" },
            new Category { Name = "Кровля" },
            new Category { Name = "Изоляция" },
            new Category { Name = "Сантехника" },
            new Category { Name = "Электрика" },
            new Category { Name = "Инструменты" }
        );
        db.SaveChanges();
    }

    
    if (!db.Materials.Any())
    {
        var categories = db.Categories.ToList();

        var materials = new List<BuildingMaterial>
        {
            // Сухие смеси
            new BuildingMaterial { Name = "Цемент М500 Д0 (50 кг)", Price = 480.00m, Stock = 150, CategoryId = categories[0].Id },
            new BuildingMaterial { Name = "Цемент М400 Д20 (50 кг)", Price = 420.00m, Stock = 200, CategoryId = categories[0].Id },
            new BuildingMaterial { Name = "Штукатурка гипсовая Knauf Rotband (30 кг)", Price = 450.00m, Stock = 80, CategoryId = categories[0].Id },
            new BuildingMaterial { Name = "Шпаклёвка финишная Vetonit LR+ (25 кг)", Price = 650.00m, Stock = 60, CategoryId = categories[0].Id },
            new BuildingMaterial { Name = "Клей плиточный Ceresit CM11 (25 кг)", Price = 380.00m, Stock = 120, CategoryId = categories[0].Id },
            new BuildingMaterial { Name = "Наливной пол Старатели (20 кг)", Price = 320.00m, Stock = 45, CategoryId = categories[0].Id },
            
            // Кирпич и блоки
            new BuildingMaterial { Name = "Кирпич красный полнотелый М150", Price = 18.50m, Stock = 5000, CategoryId = categories[1].Id },
            new BuildingMaterial { Name = "Кирпич облицовочный коричневый", Price = 24.00m, Stock = 3000, CategoryId = categories[1].Id },
            new BuildingMaterial { Name = "Блок газобетонный 600x300x200", Price = 165.00m, Stock = 200, CategoryId = categories[1].Id },
            new BuildingMaterial { Name = "Блок керамический 14.3 НФ", Price = 125.00m, Stock = 150, CategoryId = categories[1].Id },
            new BuildingMaterial { Name = "Пеноблок 600x300x200", Price = 145.00m, Stock = 180, CategoryId = categories[1].Id },
            
            // Отделочные материалы
            new BuildingMaterial { Name = "Плитка керамическая 600x600 мм (м²)", Price = 1200.00m, Stock = 250, CategoryId = categories[2].Id },
            new BuildingMaterial { Name = "Плитка настенная 200x300 мм (м²)", Price = 850.00m, Stock = 180, CategoryId = categories[2].Id },
            new BuildingMaterial { Name = "Ламинат 33 класс дуб натуральный (м²)", Price = 950.00m, Stock = 120, CategoryId = categories[2].Id },
            new BuildingMaterial { Name = "Линолеум бытовой Таркетт (м²)", Price = 450.00m, Stock = 200, CategoryId = categories[2].Id },
            new BuildingMaterial { Name = "Обои флизелиновые (рулон 10 м)", Price = 1200.00m, Stock = 80, CategoryId = categories[2].Id },
            new BuildingMaterial { Name = "Краска водоэмульсионная белая 10 л", Price = 850.00m, Stock = 60, CategoryId = categories[2].Id },
            new BuildingMaterial { Name = "Грунтовка глубокого проникновения 10 л", Price = 450.00m, Stock = 90, CategoryId = categories[2].Id },
            
            // Кровля
            new BuildingMaterial { Name = "Профнастил С8 оцинкованный (м²)", Price = 380.00m, Stock = 300, CategoryId = categories[3].Id },
            new BuildingMaterial { Name = "Профнастил НС35 с полимерным покрытием (м²)", Price = 520.00m, Stock = 250, CategoryId = categories[3].Id },
            new BuildingMaterial { Name = "Металлочерепица Монтеррей (м²)", Price = 650.00m, Stock = 180, CategoryId = categories[3].Id },
            new BuildingMaterial { Name = "Ондулин коричневый (лист)", Price = 580.00m, Stock = 150, CategoryId = categories[3].Id },
            new BuildingMaterial { Name = "Шифер 8-волновой", Price = 420.00m, Stock = 200, CategoryId = categories[3].Id },
            new BuildingMaterial { Name = "Гидроизоляция кровельная (рулон 10 м)", Price = 1200.00m, Stock = 80, CategoryId = categories[3].Id },
            
            // Изоляция
            new BuildingMaterial { Name = "Минеральная вата Rockwool 50 мм (м²)", Price = 280.00m, Stock = 200, CategoryId = categories[4].Id },
            new BuildingMaterial { Name = "Минеральная вата Rockwool 100 мм (м²)", Price = 450.00m, Stock = 150, CategoryId = categories[4].Id },
            new BuildingMaterial { Name = "Пенопласт ПСБ-С 25 50 мм (м²)", Price = 180.00m, Stock = 250, CategoryId = categories[4].Id },
            new BuildingMaterial { Name = "Пеноплекс 50 мм (м²)", Price = 320.00m, Stock = 180, CategoryId = categories[4].Id },
            new BuildingMaterial { Name = "Пароизоляционная плёнка (рулон 50 м)", Price = 850.00m, Stock = 60, CategoryId = categories[4].Id },
            
            // Сантехника
            new BuildingMaterial { Name = "Труба полипропиленовая 20 мм (2 м)", Price = 85.00m, Stock = 300, CategoryId = categories[5].Id },
            new BuildingMaterial { Name = "Труба полипропиленовая 25 мм (2 м)", Price = 120.00m, Stock = 250, CategoryId = categories[5].Id },
            new BuildingMaterial { Name = "Кран шаровый 1/2\"", Price = 180.00m, Stock = 150, CategoryId = categories[5].Id },
            new BuildingMaterial { Name = "Смеситель для кухни", Price = 2500.00m, Stock = 40, CategoryId = categories[5].Id },
            new BuildingMaterial { Name = "Унитаз компакт с бачком", Price = 4500.00m, Stock = 25, CategoryId = categories[5].Id },
            new BuildingMaterial { Name = "Ванна акриловая 170x70 см", Price = 12500.00m, Stock = 15, CategoryId = categories[5].Id },
            new BuildingMaterial { Name = "Раковина керамическая", Price = 1800.00m, Stock = 35, CategoryId = categories[5].Id },
            
            // Электрика
            new BuildingMaterial { Name = "Кабель ВВГнг 3x1.5 (м)", Price = 45.00m, Stock = 500, CategoryId = categories[6].Id },
            new BuildingMaterial { Name = "Кабель ВВГнг 3x2.5 (м)", Price = 65.00m, Stock = 450, CategoryId = categories[6].Id },
            new BuildingMaterial { Name = "Розетка внутренняя белая", Price = 120.00m, Stock = 200, CategoryId = categories[6].Id },
            new BuildingMaterial { Name = "Выключатель одноклавишный", Price = 95.00m, Stock = 180, CategoryId = categories[6].Id },
            new BuildingMaterial { Name = "Автоматический выключатель 16А", Price = 180.00m, Stock = 120, CategoryId = categories[6].Id },
            new BuildingMaterial { Name = "Щиток распределительный 12 модулей", Price = 850.00m, Stock = 40, CategoryId = categories[6].Id },
            new BuildingMaterial { Name = "LED лампа 10W E27", Price = 120.00m, Stock = 300, CategoryId = categories[6].Id },
            
            // Инструменты
            new BuildingMaterial { Name = "Перфоратор SDS-Plus 800W", Price = 4500.00m, Stock = 20, CategoryId = categories[7].Id },
            new BuildingMaterial { Name = "Шуруповёрт 18V с аккумулятором", Price = 3800.00m, Stock = 25, CategoryId = categories[7].Id },
            new BuildingMaterial { Name = "Болгарка 125 мм 900W", Price = 2800.00m, Stock = 30, CategoryId = categories[7].Id },
            new BuildingMaterial { Name = "Набор отвёрток (6 предметов)", Price = 650.00m, Stock = 50, CategoryId = categories[7].Id },
            new BuildingMaterial { Name = "Молоток столярный 500г", Price = 350.00m, Stock = 60, CategoryId = categories[7].Id },
            new BuildingMaterial { Name = "Уровень строительный 60 см", Price = 450.00m, Stock = 45, CategoryId = categories[7].Id }
        };

        db.Materials.AddRange(materials);
        db.SaveChanges();
    }
}


app.Run();