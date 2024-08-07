using System.Reflection;
using System.Text;
using DongleSimulator.Application;
using DongleSimulator.Domain.Services.Tokens;
using DongleSimulator.Filters;
using DongleSimulator.Infra;
using DongleSimulator.Infra.Database.Migrations;
using DongleSimulator.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// Filters
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// Authentication & Authorization
var signInKey = builder.Configuration.GetSection("JWT:Secret").Value!;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(config =>
{
    config.SaveToken = false;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey))
    };
});

// Auth Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        EX: 'Bearer 123456abc'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Migration
await Migrate(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
return;

async Task Migrate(IConfiguration config)
{
    await DatabaseMigration.MigrateDb(config);
    await DatabaseMigration.MigrateTables(config);
}