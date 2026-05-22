var builder = DistributedApplication.CreateBuilder(args);

var githubModelsToken = builder.AddParameter("github-models-token", secret: true);

var web = builder.AddProject<Projects.MyCollection>("mycollection");
builder.AddProject<Projects.MyCollection_ThumbnailWorker>("thumbnailworker");
builder.AddProject<Projects.MyCollection_AiWorker>("aiworker")
    .WithEnvironment("AI__Endpoint", "https://models.github.ai/inference")
    .WithEnvironment("AI__Model", "openai/gpt-4.1")
    .WithEnvironment("AI__ApiKey", githubModelsToken)
    .WithEnvironment("ConnectionStrings__collectiondb", "Data Source=..\\MyCollection\\MyCollection.db");

var tunnel = builder.AddDevTunnel("mycollection-tunnel")
    .WithReference(web)
    .WithAnonymousAccess();

web.WithEnvironment("DEVTUNNEL_URL", tunnel.GetEndpoint(web, "https"));

builder.Build().Run();
