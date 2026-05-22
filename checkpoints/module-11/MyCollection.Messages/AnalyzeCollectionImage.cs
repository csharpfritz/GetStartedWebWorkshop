using NServiceBus;

namespace MyCollection.Messages;

public record AnalyzeCollectionImage(
    int CollectionItemId,
    string OriginalImagePath,
    string ThumbnailPath) : IEvent;
