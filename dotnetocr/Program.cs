
using Tesseract;

namespace dotnetocr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var path = builder.Environment.ContentRootPath + "\\ocrfiles";
            TesseractEngine engine = new TesseractEngine(path, "eng", EngineMode.Default);
            builder.Services.AddSingleton<TesseractEngine>(engine);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}