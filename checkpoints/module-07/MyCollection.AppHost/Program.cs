var builder = DistributedApplication.CreateBuilder(args);

var web = builder.AddProject<Projects.MyCollection>("mycollection");

builder.AddDevTunnel("mycollection-tunnel")
    .WithReference(web)
    .WithAnonymousAccess();

builder.Build().Run();