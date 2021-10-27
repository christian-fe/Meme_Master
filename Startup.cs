using Memes.Data;//*
using Memes.Repository;//*
using Memes.Repository.IRepository;//*
using AutoMapper;//*
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//*
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Memes.MemeMapper;//*
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Memes.Helpers;

namespace Memes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //dependency injection for database migration
            services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("Memes")));

            //Dependency Injection for Category Resource
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //Dependency Injection for Photo Resource
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            //Dependency Injection for User Resource
            services.AddScoped<IUserRepository, UserRepository>();

            /*Add token dependency*/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
                );

            services.AddAutoMapper(typeof(MemeMappers));

            //Start the documentation configuration
            services.AddSwaggerGen(options =>
            {
                /*------------------------------CATEGORY--------------------------------*/
                options.SwaggerDoc("Meme_Master", new OpenApiInfo()
                {
                    Title = "Api-Category",
                    Version = "1",
                    Description = "BackEnd Meme",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "meme.factory@madthings.com",
                        Name = "UpSkillingProject",
                        Url = new Uri("https://upSkilling.com")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "NIT Licence",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }

                });
                /*------------------------------PHOTO-----------------------------------*/
                options.SwaggerDoc("Api-Photo", new OpenApiInfo()
                {
                    Title = "Api Photo",
                    Version = "1",
                    Description = "BackEnd Meme",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "meme.factory@madthings.com",
                        Name = "UpSkillingProject",
                        Url = new Uri("https://upSkilling.com")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "NIT Licence",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }

                });
                /*------------------------------USER------------------------------------*/
                options.SwaggerDoc("Api-User", new OpenApiInfo()
                {
                    Title = "Api User",
                    Version = "1",
                    Description = "BackEnd Meme",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "meme.factory@madthings.com",
                        Name = "UpSkillingProject",
                        Url = new Uri("https://upSkilling.com")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "NIT Licence",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }

                });

                var xmlFileComents = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var pathApiComents = Path.Combine(AppContext.BaseDirectory, xmlFileComents);
                options.IncludeXmlComments(pathApiComents);

                //First definition schema
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Autentication JWT (Bearer)",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    }
                );

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type= ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }

                });

            });
                services.AddControllers();

                //Bring CORS support
                services.AddCors();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            var error = context.Features.Get<IExceptionHandlerFeature>();

                            if(error != null)
                            {
                                context.Response.AddAppicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message);
                            }
                        });
                });
            }

            app.UseHttpsRedirection();

            //Api documentation with Swagger
            app.UseSwagger();

            //End Point
            app.UseSwaggerUI(options=>
            {
                options.SwaggerEndpoint("/swagger/Meme_Master/swagger.json", "Api-Category");
                options.SwaggerEndpoint("/swagger/Api-Photo/swagger.json", "Api-Photo");
                options.SwaggerEndpoint("/swagger/Api-User/swagger.json", "Api-User");
                options.RoutePrefix = "";
            });


            app.UseRouting();

            //Autentication and Autorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Bring CORS support
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
