using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Scraping.Application.Services;
using Scraping.Domain.Interfaces;
using Scraping.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Scraping API",
        Description = "API para realizar scraping de qualquer site.",
        Contact = new OpenApiContact
        {
            Name = "Victor Persike",
            Email = "dev.victor.persike@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/victor-persike-78515b71/")
        }
    });
});

builder.Services.AddScoped<ISwuHandler, SwuHandler>();
builder.Services.AddScoped<SwuScraperService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scraping API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
