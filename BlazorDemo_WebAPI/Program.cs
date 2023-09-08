using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using BlazorDemo_WebAPI.Context;
using BlazorDemo_WebAPI.Extensions;
using BlazorDemo_WebAPI.Migration;
using BlazorDemo_WebAPI.Models;
using BlazorDemo_WebAPI.Repositories;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json", true, true)
   .Build();

// Console Logging
builder.Logging.ClearProviders();
builder.Logging.AddColorConsoleLogger(configuration =>
{
    // Replace warning value from appsettings.json of "Cyan"
    configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
    // Replace warning value from appsettings.json of "Red"
    configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
});

// Adds services required for using options.
builder.Services.AddOptions();

// Add settings data for dependency injection in controllers.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.Configure<AppOptions>(myOptions =>
{
    myOptions.DefaultConnection = configuration.GetConnectionString("SqlConnection");
});

// Add services to the container.
// Warning! Bad service declerations can cause app o fail without error message.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<Database>();

// Logging
builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
.AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddSqlServer2012()
            .WithGlobalConnectionString(configuration.GetConnectionString("SqlConnection"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container for dependency injection.
var conectionString = configuration.GetConnectionString("SqlConnection");
builder.Services.AddScoped<I_RoleRepository, _RoleRepository>(sc => new _RoleRepository(conectionString));
builder.Services.AddScoped<I_UserRepository, _UserRepository>(sc => new _UserRepository(conectionString));
builder.Services.AddScoped<I_UserRoleRepository, _UserRoleRepository>(sc => new _UserRoleRepository(conectionString));

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        // ValidIssuer = builder.Configuration["Jwt:Issuer"],
        // ValidAudience = builder.Configuration["Jwt:Audience"],
        // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings")["Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireSuperAdmin",
        policy => policy.RequireRole("SuperAdmin"));
});

// Define Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorDemo_WebAPI - With JWT authentication", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Confirming that console logging is running properly.
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation(3, "Backend service starting..."); // Logs in ConsoleColor.DarkGreen

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API with JWT authentication V1");
    });
}

// Trigger databasemigration
app.MigrateDatabase();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseDeveloperExceptionPage();

app.Run();