//using System.Reflection;
//using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Добавление сервиса в контейнер.

//builder.Services.AddControllers();


//builder.Services.AddSwaggerGen(c =>
//{
//    c.EnableAnnotations();
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Weather API",
//    });
//    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//});
//var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//// Настраиваем конвейер HTTP-запросов.

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.OpenApi.Models;
using System.Diagnostics;

public class Program
{
    private const string _commandTest = "http —url=2qALO3UKM9clYkw6SsTr81OAyrq_rKnhYYKKymwzdmQzCysH";

    public static async Task Main(string[] args)
    {
        string desktopPath = Path.Combine(Environment.CurrentDirectory, "ngrok.exe");
        RunExternalApp(desktopPath, _commandTest);

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
        });
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "Weather API",
                Description = "Приложение представляет собой API для получения " +
                "данных о погоде в городе с использованием ASP.NET Core. " +
                "Оно включает контроллер для обработки запросов и " +
                "сервис для обращения к внешнему API, предоставляющему информацию о погоде." +
                "API позволяет пользователям запрашивать текущую погоду по названию города." +
                "Используя контроллеры, сервисы и модели, приложение организует процесс получения и обработки данных, что делает его легко расширяемым и управляемым.",

            });
        });
        // Настройки CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });

        // Добавляем контроллеры
        builder.Services.AddControllers();
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // Включаем использование CORS
        app.UseCors("AllowAllOrigins");
        // Включаем маршрутизацию
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        // Настраиваем маршруты для контроллеров
        app.MapControllers();

        // Запускаем приложение
        app.Run();
    }
    private static void RunExternalApp(string filePath, string arguments)
    {
        try
        {
            // Настраиваем процесс для запуска внешнего приложения
            var processStartInfo = new ProcessStartInfo
            {
                FileName = filePath, // Указанный путь к ngrok.exe
                Arguments = arguments, // Аргументы для ngrok
                UseShellExecute = true, // Позволяет запускать приложение в отдельной консоли
                CreateNoWindow = false // Создает отдельное окно для приложения
            };
            // Запускаем процесс
            Process.Start(processStartInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to run external app: " + ex.Message);
        }
    }
}

