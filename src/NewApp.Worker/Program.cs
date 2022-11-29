using MassTransit;
using NewApp.Worker;
using NServiceBus;

var host = Host.CreateDefaultBuilder(args)
    .UseMassTransit((_, x) =>
    {
        x.AddConsumer<SubmitOrderConsumer>()
            .Endpoint(e => e.Name = "new-order-endpoint");
        
        x.AddConsumer<OrderSubmittedConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Publish<IEvent>(p => p.Exclude = true);
            cfg.Publish<IMessage>(p => p.Exclude = true);

            cfg.UseNServiceBusJsonSerializer();

            cfg.ConfigureEndpoints(context);
        });
    })
    .ConfigureServices(services =>
    {
        services.AddOptions<RabbitMqTransportOptions>()
            .Configure(options =>
            {
                options.Host = "localhost";
            });
    })
    .Build();

await host.RunAsync();