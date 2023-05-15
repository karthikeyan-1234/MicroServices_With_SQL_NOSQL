using MassTransit;

using MediatR;

using Microsoft.EntityFrameworkCore;

using PurchaseAPI.CQRS.Commands;
using PurchaseAPI.CQRS.Handlers.Mongo;
using PurchaseAPI.CQRS.Handlers.RabbitMQ;
using PurchaseAPI.CQRS.Handlers.Sql;
using PurchaseAPI.CQRS.Notifications;
using PurchaseAPI.CQRS.Queries;
using PurchaseAPI.Infrastructure.Contexts;
using ERPModels;
using PurchaseAPI.Repositories;
using PurchaseAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PurchaseDBContext>(p => p.UseSqlServer(configuration.GetConnectionString("PurchaseDbConn")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IPurchaseDBContext, PurchaseDBContext>();

builder.Services.AddScoped<IPurchaseService,PurchaseService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.Lifetime = ServiceLifetime.Scoped;
});

builder.Services.AddScoped<IRequestHandler<AddPurchaseCommand,PurchaseDTO>, AddPurchaseCommandSQLHandler>();
builder.Services.AddScoped<IRequestHandler<DeletePurchaseCommand,bool>, DeletePurchaseCommandSQLHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllPurchasesQuery, IEnumerable<PurchaseDTO>>, GetAllPurchasesQuerySQLHandler>();
builder.Services.AddScoped<IRequestHandler<GetPurchaseByIDQuery, PurchaseDTO>, GetPurchaseByIDQuerySQLHandler>();
builder.Services.AddScoped<IRequestHandler<AddPurchaseDetailCommand,PurchaseDetailDTO>, AddPurchaseDetailCommandSQLHandler>();
//builder.Services.AddScoped<INotificationHandler<PurchaseAddedNotification>, PurchaseAddedNotificationRMQHandler>();


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
