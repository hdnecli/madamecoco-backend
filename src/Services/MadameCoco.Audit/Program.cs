using MadameCoco.Audit.Consumers;
using MadameCoco.Audit.Jobs;
using MadameCoco.Audit.Services;
using MassTransit;
using Quartz;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = Host.CreateApplicationBuilder(args);

// Register AuditService
builder.Services.AddSingleton<AuditService>();

// Quartz Configuration
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("DailyReportJob");
    q.AddJob<DailyReportJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DailyReportJob-trigger")
        .WithCronSchedule("0 * * ? * *")); // Every minute for testing
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// MassTransit Configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", h => 
        {
            h.Username(builder.Configuration["RabbitMq:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMq:Password"] ?? "guest");
        });

        cfg.ReceiveEndpoint("order-created-audit", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();
