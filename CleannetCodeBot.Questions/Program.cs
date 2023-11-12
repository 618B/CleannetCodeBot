using System.Reflection;
using CleannetCodeBot.Questions;
using CleannetCodeBot.Questions.Infrastructure;
using CleannetCodeBot.Questions.Questions;
using MassTransit;
using MongoDB.Driver;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddSingleton<IMongoDatabase>(_ =>
    {
        var connectionString = context.Configuration.GetConnectionString("MongoDbConnectionString")!;
        var client = new MongoClient(connectionString);
        return client.GetDatabase("Questions");
    });

    services.AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();

        var assembly = Assembly.GetEntryAssembly();
    
        x.AddConsumers(assembly);
    
        x.UsingRabbitMq((hostContext, cfg) =>
        {
            var connectionConfig = context.Configuration.GetSection("RabbitMqConfig");
        
            cfg.Host(connectionConfig["Host"], connectionConfig["VirtualHost"], h =>
            {
                h.Username(connectionConfig["Username"]);
                h.Password(connectionConfig["Password"]);
            });
        
            cfg.ConfigureEndpoints(hostContext);
        });
    });

    services.AddSingleton<IQuestionsRepository, QuestionsRepository>();
});

IHost host = builder.Build();



using (var scope = host.Services.CreateScope())
{
    var qs = scope.ServiceProvider.GetRequiredService<QuestionsService>();

    var fs = File.ReadLines("data.csv");
    var questions = fs.Select(CsvQuestionParser.ParseOne);
    
    qs.AddQuestions(questions);
}

host.Run();