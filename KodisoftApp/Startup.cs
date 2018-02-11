﻿using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace KodisoftApp
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
            services.AddIdentity<IdentityUser, IdentityRole>();
            services
                .AddAuthentication(v =>
                {
                    v.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                    v.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });

            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule
            {
                ConnectionString = Configuration.GetConnectionString("DefaultConnection")
            });

            builder.RegisterModule(new FeedsModule
            {
                Settings = Configuration.GetSection("Feeds").Get<FeedSettings[]>()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUi3Settings
            {
                OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = Configuration["Authentication:Google:ClientId"],
                    ClientSecret = Configuration["Authentication:Google:ClientSecret"],
                    //RedirectUrl = "'http://localhost:5000/signin-google'"
                },
                DocumentProcessors =
                {
                    new SecurityDefinitionAppender("oauth2", new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.OAuth2,
                        Flow = SwaggerOAuth2Flow.Implicit,
                        AuthorizationUrl = GoogleDefaults.AuthorizationEndpoint,
                        TokenUrl = GoogleDefaults.TokenEndpoint,
                        Scopes = new Dictionary<string,string>
                        {
                            ["email"] = "Identifies user"
                        }
                    })
                },
                OperationProcessors =
                {
                    new OperationSecurityScopeProcessor("oauth2")
                }
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }

    public class MyOAuth2ClientSettings : OAuth2ClientSettings
    {
        public string RedirectUrl { get; set; }
    }
}
