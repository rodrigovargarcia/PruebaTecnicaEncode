using Microsoft.EntityFrameworkCore;
using PruebaTecnicaEncode.Repositories;
using static PruebaTecnicaEncode.Entities.Usuario;

namespace PruebaTecnicaEncode
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? 
                                    Configuration.GetConnectionString("defaultConnection");

            //Console.WriteLine($"Connection String: {connectionString}");

            //var connectionString2 = Configuration.GetConnectionString("defaultConnection");
            //Console.WriteLine($"Connection String 2: { connectionString2}");
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<IgnorePropertiesSchemaFilter>();
            });
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
