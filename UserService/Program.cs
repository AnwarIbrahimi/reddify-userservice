using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System;
using UserService.Data;
using UserService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddSingleton(serviceProvider =>
{
    var factory = new ConnectionFactory
    {
        Uri = new Uri(configuration["RabbitMQ:Url"]),
    };

    var connection = factory.CreateConnection();
    return connection;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetRequiredService<UserContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
