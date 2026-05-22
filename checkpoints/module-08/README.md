# Module 7 Checkpoint - MyCollection with .NET Aspire

This checkpoint adds .NET Aspire orchestration and dev tunnel-ready external endpoints to the EF Core collection app.

## Included in this checkpoint
- `MyCollection.AppHost` for local orchestration
- `MyCollection.ServiceDefaults` for telemetry, health checks, and service discovery defaults
- `aspire.config.json` so `aspire run` can locate the AppHost
- `MyCollection` wired to `AddServiceDefaults()` and `MapDefaultEndpoints()`
- AppHost `WithExternalHttpEndpoints()` support for Aspire dev tunnels
- Solution updates for the new projects