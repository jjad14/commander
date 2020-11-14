using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Commander.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) 
        {
            services.AddSwaggerGen(s => 
            {
                s.SwaggerDoc("v1", new OpenApiInfo {Title="Commander API", Version="v1"});
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => 
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Commander API v1");
            });
            
            return app;
        }

    }
}