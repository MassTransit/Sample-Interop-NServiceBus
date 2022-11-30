using LegacyApp.Contracts.Commands;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNServiceBus(_ =>
{
    const string endpointName = "order-endpoint";

    var endpointConfiguration = new EndpointConfiguration(endpointName);

    endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
    endpointConfiguration.EnableInstallers();

    endpointConfiguration.Conventions()
        .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("LegacyApp.Contracts.Commands"));
    endpointConfiguration.Conventions()
        .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("LegacyApp.Contracts.Events"));

    TransportExtensions<RabbitMQTransport>? transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString("host=localhost;username=guest;password=guest;virtualhost=/");

    transport.UseConventionalRoutingTopology(QueueType.Classic);

    RoutingSettings<RabbitMQTransport>? routing = transport.Routing();
    routing.RouteToEndpoint(typeof(SubmitOrder), "new-order-endpoint");

    return endpointConfiguration;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();