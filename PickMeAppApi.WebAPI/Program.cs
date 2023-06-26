using Microsoft.OpenApi.Models;
using PickMeAppApi.Application.Queries;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(GetWeatherForecastQuery).Assembly));

if (!builder.Environment.IsProduction())
{
    builder.Services.AddSwaggerGen(options =>
    {
        OpenApiSecurityScheme? securityScheme = new()
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            Description =
                $"JWT Authorization header using the Bearer scheme. {Environment.NewLine}{Environment.NewLine}" +
                $"Enter 'Bearer' [space] and then your token in the text input below. {Environment.NewLine}{Environment.NewLine}" +
                "Example: 'Bearer [JWT Token]'",
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition("Bearer", securityScheme);
    });
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(configuration.GetSection("AllowedOrigins").Value.Split(","));
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();