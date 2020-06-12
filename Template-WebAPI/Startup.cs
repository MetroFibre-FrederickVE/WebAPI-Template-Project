using System.IO;
using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Template_WebAPI.Authentication;
using Template_WebAPI.Events;
using Template_WebAPI.Manager;
using Template_WebAPI.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using MPU.MicroServices.StandardLibrary.CloudMessaging;
using Amazon.SQS;
using Amazon.SimpleNotificationService;
using MPU.MicroServices.StandardLibrary.EnvironmentService;

namespace Template_WebAPI
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
      services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
      {
        builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
      }));
      services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
      services.AddAWSService<IAmazonS3>();
      services.AddAWSService<IAmazonSQS>();
      services.AddAWSService<IAmazonSimpleNotificationService>();
      services.AddSingleton<IEnvironmentService, CloudEnvironmentService>();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      services.AddSingleton<IMongoContext, MongoContext>();
      services.AddSingleton<ITemplateManager, TemplateManager>();
      services.AddSingleton<ITemplateRepository, MongoDBTemplateRepository>();
      services.AddSingleton<IEnumRepository, EnumRepository>();
      services.AddSingleton<ICloudFileManager, AWSCloudFileManager>();
      services.AddHostedService<TemplateDraftUploadDirCleaner>();
      services.AddSingleton<IEventSourceManager, EventSourceManager>();
      services.AddSingleton<IEventSourceRepository, MongoDBEventSourceRepository>();
      services.AddHttpContextAccessor();

      var authenticationEnvironmentVariable = Environment.GetEnvironmentVariable("JWT_SECRET");
      var key = Encoding.ASCII.GetBytes(authenticationEnvironmentVariable);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
        .AddJwtBearer(x =>
        {
          x.RequireHttpsMetadata = false;
          x.SaveToken = true;
          x.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true
          };
        });

      services.AddSingleton<ICloudMessageBus, AWSMessageService>();

      services.AddSingleton<IClaimsRepository, MongoDBClaimsRepository>();

      services.AddSingleton<IClaimsManager, ClaimsManager>();

      services.AddHostedService<ClaimsManager>();

      services.Configure<ApplicationOptions>(Configuration.GetSection("ApplicationOptions"));

      var applicationOptions = Configuration
        .GetSection("ApplicationOptions")
        .Get<ApplicationOptions>();

      services.AddAuthorization((options_CV) => {
        options_CV.AddPolicy("CustomClaimsPolicy - Class Viewer", policy =>
        {
          policy.RequireAuthenticatedUser();
          
          policy.Requirements.Add(new ClaimsRequirment("Class Viewer"));
        });
      });

      services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseCors("ApiCorsPolicy");
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
        RequestPath = new PathString("/Resources")
      });

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

    }
  }
}
