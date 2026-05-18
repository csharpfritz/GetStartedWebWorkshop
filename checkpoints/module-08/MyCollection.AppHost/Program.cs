var builder = DistributedApplication.CreateBuilder(args);

var web = builder.AddProject<Projects.MyCollection>("mycollection");

var tunnel = builder.AddDevTunnel("mycollection-tunnel")
    .WithReference(web)
    .WithAnonymousAccess();

web.WithEnvironment("DEVTUNNEL_URL", tunnel.GetEndpoint(web, "https"));

builder.Build().Run();