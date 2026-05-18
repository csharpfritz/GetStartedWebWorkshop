using Microsoft.EntityFrameworkCore;
using MyCollection.Components;
using MyCollection.Data;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var endpointConfiguration = new EndpointConfiguration("MyCollection.Web");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

var transport = endpointConfiguration.UseTransport<LearningTransport>();
transport.StorageDirectory(
    Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", ".learningtransport")));

builder.UseNServiceBus(endpointConfiguration);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<CollectionContext>(options =>
    options.UseSqlite("Data Source=MyCollection.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CollectionContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
