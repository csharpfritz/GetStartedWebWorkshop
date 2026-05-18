using Microsoft.Extensions.AI;
using System.ClientModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NServiceBus;
using OpenAI;
using OpenAI.Chat;

namespace MyCollection.AiWorker;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.Services
            .AddOptions<AiOptions>()
            .Bind(builder.Configuration.GetSection(AiOptions.SectionName))
            .Validate(options => Uri.TryCreate(options.Endpoint, UriKind.Absolute, out _), "AI:Endpoint must be a valid absolute URI.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.Model), "AI:Model is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.ApiKey), "AI:ApiKey is required.")
            .ValidateOnStart();

        builder.Services.AddSingleton<IChatClient>(services =>
        {
            var aiOptions = services.GetRequiredService<IOptions<AiOptions>>().Value;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            IChatClient innerClient = new ChatClient(
                model: aiOptions.Model,
                credential: new ApiKeyCredential(aiOptions.ApiKey),
                options: new OpenAIClientOptions
                {
                    Endpoint = new Uri(aiOptions.Endpoint)
                })
                .AsIChatClient();

            return new ChatClientBuilder(innerClient)
                .UseLogging(loggerFactory)
                .Build();
        });

        builder.Services.AddSingleton<ImageAnalysisService>();
        builder.Services.AddSingleton<CollectionAnalysisStore>();

        var endpointConfiguration = new EndpointConfiguration("MyCollection.AiWorker");
        endpointConfiguration.UseSerialization<SystemJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.StorageDirectory(Path.GetFullPath(
            Path.Combine(builder.Environment.ContentRootPath, "..", ".learningtransport")));

        builder.UseNServiceBus(endpointConfiguration);

        await builder.Build().RunAsync();
    }
}
