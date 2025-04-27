
namespace CukraszdaAPI
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

            var corsPolicy = "AllowBlazor";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://localhost:44324")
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowBlazor");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
