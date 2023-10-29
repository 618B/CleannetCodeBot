using System.Reflection;
using MassTransit;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IMongoDatabase>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDbConnectionString")!;
    var client = new MongoClient(connectionString);
    return client.GetDatabase("Statistics");
});

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    var assembly = Assembly.GetEntryAssembly();
    
    x.AddConsumers(assembly);
    
    x.UsingRabbitMq((hostContext, cfg) =>
    {
        var connectionConfig = builder.Configuration.GetSection("RabbitMqConfig");
        
        cfg.Host(connectionConfig["Host"], connectionConfig["VirtualHost"], h =>
        {
            h.Username(connectionConfig["Username"]);
            h.Password(connectionConfig["Password"]);
        });
        
        cfg.ConfigureEndpoints(hostContext);
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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