using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Bellwether.WebApi.Authorization;
using System;
using Microsoft.Extensions.Logging;
using Core.Akka.ActorAutostart;
using Akka.DI.AutoFac;
using Core.Akka.ActorSystem;

namespace Bellwether.WebApi
{
  public class Startup
  {
    private readonly SecurityKey _issuerSigningKey;
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
      _issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("SecretKey").Value.ToString()));
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var jwtOptions = Configuration.GetSection(nameof(JwtOptions));

      services.Configure<JwtOptions>(options =>
          {
            options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
            options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
            options.TokenName = jwtOptions[nameof(JwtOptions.TokenName)];
            options.Path = jwtOptions[nameof(JwtOptions.Path)];
            options.Subject = jwtOptions[nameof(JwtOptions.Subject)];
            options.ValidFor = TimeSpan.FromMinutes(Convert.ToInt32(jwtOptions[nameof(JwtOptions.ValidFor)]));
            options.SigningCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
          });

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Configuration = new OpenIdConnectConfiguration();
        options.Audience = Configuration["JwtOptions:Audience"];
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          IssuerSigningKey = _issuerSigningKey,
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateIssuerSigningKey = true
        };
      });

      services.AddCors();
      services.AddMvc();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Bellwether api", Version = "v1" });
      });
    }

    public void Configure(IApplicationBuilder app,
                          IHostingEnvironment env,
                          ILoggerFactory loggerFactory,
                          IAutostartActorInitializer autostartActorInitializer)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseCors(x => x
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());

      app.UseMiddleware<JwtTokenMiddleware>();
      app.UseAuthentication();

      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bellwether api v1");
      });

      autostartActorInitializer.FindAndStartActors();
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterModule(new BellwetherWebApiModule());
    }
  }
}
