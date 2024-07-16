
using ApiAplication.Interface;
using ApiAplication.Servicios;

namespace ApiAplication
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
            builder.Services.AddScoped<IPersona, PersonaServices>();
            builder.Services.AddScoped<IContacto, ContactoServices>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PoliticaAplicacion", app =>
                {
                    app.AllowAnyHeader()
                    .AllowAnyOrigin()
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

            app.UseCors("PoliticaAplicacion");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
