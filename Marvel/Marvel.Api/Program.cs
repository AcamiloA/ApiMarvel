using Marvel.Api.Helpers;
using Marvel.Application.Repositories;
using Marvel.Application.Security;
using Marvel.Application.Services;
using Marvel.Infrastruture.Context;
using Marvel.Infrastruture.Repositories;
using Marvel.Infrastruture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddScoped<IComicService, ComicService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition =
    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

var jwtSecuritySettingsSection = builder.Configuration.GetSection(nameof(JwtSecuritySettings));
builder.Services.Configure<JwtSecuritySettings>(jwtSecuritySettingsSection);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Marvel API",
        Version = "v1",
        Description = "API para la consulta y el guardado de información de MARVEL, proyecto realizado por ANDRÉS ALARCÓN para TecnoFactory como prueba técnica.",
        Contact = new OpenApiContact
        {
            Name = "Andrés Camilo Alarcón Rátiva",
            Email = "acalarcon7@gmail.com",
        },
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Jwt Authorization. **Token only**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            securityScheme,
            new List<string>()
        }
    });
});

builder.Services.AddIdentityJwt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
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
