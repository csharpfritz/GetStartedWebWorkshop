using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCollection.ThumbnailWorker.Services;
using NServiceBus;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSingleton<ThumbnailGenerator>();

var endpointConfiguration = new EndpointConfiguration("MyCollection.ThumbnailWorker");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();


var transport = endpointConfiguration.UseTransport<LearningTransport>();
transport.StorageDirectory(
    Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", ".learningtransport")));

builder.UseNServiceBus(endpointConfiguration);

var host = builder.Build();
await host.RunAsync();
