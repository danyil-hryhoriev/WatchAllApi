using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Managers;
using WatchAllApi.Models;
using WatchAllApi.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WatchAllApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IShowRepository, ShowRepository>();
            services.AddTransient<IChannelRepository, ChannelRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<ISeasonRepository, SeasonRepository>();
            services.AddTransient<IEpisodeRepository, EpisodeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IShowManager, ShowManager>();
            services.AddTransient<IAuthorizationManager, AuthorizationManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IPasswordHasher<UserProfile>, PasswordHasher<UserProfile>>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader()
                        .AllowAnyMethod());
            });


            services.Configure<MongoDbConfiguration>(options =>
            {
                options.ConnectionString = _configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = _configuration.GetSection("MongoConnection:Database").Value;
            });

            var authPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();


            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters.ValidateIssuer = true;
                o.TokenValidationParameters.ValidIssuer = "https://identity.watch-all.com/";
                o.TokenValidationParameters.ValidateIssuerSigningKey = true;
                o.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1235467887654321qwerty"));
                o.TokenValidationParameters.ValidateAudience = false;
                o.TokenValidationParameters.ValidateLifetime = true;
                o.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });
            services.AddAuthorization(auth => auth.AddPolicy("Bearer", authPolicy));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "WatchAll Api",
                    Description = "Swagger for WatchAll API",
                });
                options.DescribeAllEnumsAsStrings();
                var path = Path.Combine(AppContext.BaseDirectory, $"{_hostingEnvironment.ApplicationName}.xml");
                options.IncludeXmlComments(path);

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                options.IgnoreObsoleteActions();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(next => context =>
            {
                context.Request.EnableRewind();
                return next(context);
            });
            app.UseAuthentication();
            app.UseCors("AllowSpecificOrigin");
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WatchAll API V1");
            });
        }
    }
}
