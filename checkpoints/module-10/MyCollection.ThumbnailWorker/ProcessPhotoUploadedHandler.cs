using Microsoft.Extensions.Logging;
using MyCollection.Messages;
using MyCollection.ThumbnailWorker.Services;
using NServiceBus;

namespace MyCollection.ThumbnailWorker;

public class ProcessPhotoUploadedHandler(
    ThumbnailGenerator thumbnailGenerator,
    ILogger<ProcessPhotoUploadedHandler> logger) : IHandleMessages<ProcessPhotoUploaded>
{
    public async Task Handle(ProcessPhotoUploaded message, IMessageHandlerContext context)
    {
        logger.LogInformation(
            "Processing uploaded photo for item {ItemId}: {PhotoFileName}",
            message.ItemId,
            message.PhotoFileName);

        await thumbnailGenerator.CreateThumbnailAsync(
            message.OriginalPhotoPath,
            message.ThumbnailPhotoPath);

        logger.LogInformation(
            "Thumbnail created at {ThumbnailPhotoPath}",
            message.ThumbnailPhotoPath);
    }
}
