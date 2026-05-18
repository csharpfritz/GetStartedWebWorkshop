# Module 8 Checkpoint - MyCollection with Photo Uploads and QR Access

This checkpoint builds on Module 7 by keeping .NET Aspire orchestration, enabling dev tunnel-ready external endpoints, and adding photo uploads plus a QR code for quick mobile access.

## Included in this checkpoint
- `PhotoFileName` on `CollectionItem`
- Blazor `InputFile` support on the Collection page
- Server-side file validation and upload saving in `wwwroot/uploads`
- Thumbnail-style image rendering in the collection list
- EF Core migration for the photo filename column
- `.gitkeep` and `.gitignore` rules for the uploads folder
- `MyCollection.AppHost` with `WithExternalHttpEndpoints()` for Aspire dev tunnels
- `MyCollection.ServiceDefaults` for telemetry, health checks, and service discovery defaults
- Home page QR code generation with QRCoder