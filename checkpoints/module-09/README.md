# Module 9 Checkpoint - MyCollection with NServiceBus

This checkpoint adds background thumbnail generation with NServiceBus and Learning Transport.

## Included in this checkpoint
- `MyCollection.Messages` shared contracts project
- `MyCollection.ThumbnailWorker` console endpoint
- `ProcessPhotoUploaded` event contract
- Thumbnail generation with SkiaSharp and WebP output
- Blazor upload flow publishes an event after saving the original photo
- Aspire AppHost updated to start the web app and thumbnail worker together
- Service defaults updated to capture NServiceBus tracing activity
