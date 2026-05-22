using NServiceBus;

namespace MyCollection.Messages;

public class ProcessPhotoUploaded : IEvent
{
    public int ItemId { get; set; }
    public string PhotoFileName { get; set; } = string.Empty;
    public string OriginalPhotoPath { get; set; } = string.Empty;
    public string ThumbnailPhotoPath { get; set; } = string.Empty;
}
