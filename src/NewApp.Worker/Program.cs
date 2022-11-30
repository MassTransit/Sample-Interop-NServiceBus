using MassTransit;
using NewApp.Worker;

var host = Host.CreateDefaultBuilder(args)
    .UseMassTransit((_, x) =>
    {
        x.AddConsumer<SubmitOrderConsumer>()
            .Endpoint(e => e.Name = "new-order-endpoint");
        
        x.AddConsumer<OrderSubmittedConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.UseNServiceBusJsonSerializer();

            cfg.ConfigureEndpoints(context);
        });
    })
    .ConfigureServices(services =>
    {
        services.AddOptions<RabbitMqTransportOptions>()
            .Configure(options =>
            {
                options.Host = "192.168.1.25";
            });
    })
    .Build();

await host.RunAsync();