using InventoryAPI.CQRS.Commands;
using InventoryAPI.CQRS.Handlers.SQL;
using InventoryAPI.Infrastructure.Contexts;
using ERPModels;
using InventoryAPI.Repositories;
using InventoryAPI.Services;

using MassTransit;

using MediatR;

using Microsoft.EntityFrameworkCore;


using System.Reflection;
using InventoryAPI.CQRS.Handlers.MongoDB;
using InventoryAPI.CQRS.Queries;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InventoryDbContext>(p => p.UseSqlServer(configuration.GetConnectionString("InventorySQLConn")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IInventoryDbContext, InventoryDbContext>();
builder.Services.AddScoped<IInventoryService, InventoryService>();


builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped<IRequestHandler<AddInventoryCommand, InventoryDTO>, AddInventoryCommandMongoHandler>();
builder.Services.AddScoped<IRequestHandler<GetInventoryByItemQuery, InventoryDTO>, GetInventoryByItemQueryMongoHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateInventoryCommand, InventoryDTO>, UpdateInventoryCommandMongoHandler>();


builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});


builder.Services.AddHostedService<InventoryBackgroundWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
