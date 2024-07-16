
using ApiAplication.Interface;
using ApiAplication.Servicios;
using CapaDatos;
using Microsoft.EntityFrameworkCore;

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
            
            //Configurar el acceso a datos
            var conn = builder.Configuration.GetConnectionString("DefaultConnection");//crea una variable con la cadena de conexion 
            builder.Services.AddDbContext<CrudContext>(x => x.UseSqlServer(conn));//construye el contexto

            //Configurar el acceso a las interfaces para que el controlador las pueda usar
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
