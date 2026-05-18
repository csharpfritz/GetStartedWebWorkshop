using Microsoft.Extensions.Logging;
using MyCollection.Messages;
using NServiceBus;

namespace MyCollection.AiWorker;

public sealed class AnalyzeCollectionImageHandler(
    ImageAnalysisService imageAnalysisService,
    CollectionAnalysisStore store,
    ILogger<AnalyzeCollectionImageHandler> logger)
    : IHandleMessages<AnalyzeCollectionImage>
{
    public async Task Handle(AnalyzeCollectionImage message, IMessageHandlerContext context)
    {
        var imagePath = File.Exists(message.ThumbnailPath)
            ? message.ThumbnailPath
            : message.OriginalImagePath;

        logger.LogInformation(
            "Analyzing collection item {CollectionItemId} using image {ImagePath}",
            message.CollectionItemId,
            imagePath);

        var result = await imageAnalysisService.AnalyzeAsync(imagePath, context.CancellationToken);

        await store.SaveAsync(message.CollectionItemId, result, context.CancellationToken);

        logger.LogInformation(
            "Saved AI analysis for collection item {CollectionItemId}",
            message.CollectionItemId);
    }
}
