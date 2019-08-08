using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;

namespace Masha.Foundation.Web
{
    /// <summary>
    /// Configure Extension
    /// </summary>
    public static class ConfigureExtension
    {
        /// <summary>
        /// Api Configure
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ApiValidation(this IServiceCollection services, IConfiguration configuration)
        {
            //Validate Models
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                      .Where(a => a.Value.Errors.Count > 0)
                      .SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage))
                      .ToList();
                    return new BadRequestObjectResult(
                        new { Code = ErrorCodes.InputInvalid, Message = string.Join(",", errors) });
                };
            });

            //Validate Token

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Security:Tokens:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:Tokens:Key"]))
                   };
               });


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });



            return services;

        }
    }
}
