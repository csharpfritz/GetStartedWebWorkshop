using System.Text.Json;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace MyCollection.AiWorker;

public sealed class ImageAnalysisService(
    IChatClient chatClient,
    ILogger<ImageAnalysisService> logger)
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<ImageAnalysisResult> AnalyzeAsync(
        string imagePath,
        CancellationToken cancellationToken = default)
    {
        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException($"Could not find image for analysis: {imagePath}", imagePath);
        }

        var imageBytes = await File.ReadAllBytesAsync(imagePath, cancellationToken);
        var mediaType = GetMediaType(imagePath);

        List<ChatMessage> messages =
        [
            new(ChatRole.System, """
                You create accessibility-friendly descriptions for a beginner collection app.
                Return strict JSON and nothing else.
                {
                  \"description\": \"One plain-language sentence under 30 words.\",
                  \"tags\": [\"tag-one\", \"tag-two\", \"tag-three\"]
                }
                Tags must be lowercase.
                Use 3 to 5 short tags.
                If the image is unclear, say so briefly and still return helpful tags.
                """),
            new(ChatRole.User, "Describe this collection item and suggest tags.")
        ];

        messages[1].Contents.Add(new DataContent(imageBytes, mediaType));

        var response = await chatClient.GetResponseAsync(messages, cancellationToken: cancellationToken);

        var rawJson = StripMarkdownCodeFence(response.Text ?? string.Empty);
        var result = JsonSerializer.Deserialize<ImageAnalysisResult>(rawJson, SerializerOptions);

        if (result is null || string.IsNullOrWhiteSpace(result.Description))
        {
            logger.LogWarning("AI returned an unreadable response: {ResponseText}", response.Text);
            throw new InvalidOperationException("The AI response could not be parsed into ImageAnalysisResult.");
        }

        var cleanedTags = (result.Tags ?? [])
            .Where(tag => !string.IsNullOrWhiteSpace(tag))
            .Select(tag => tag.Trim().ToLowerInvariant())
            .Distinct()
            .Take(5)
            .ToArray();

        return result with { Tags = cleanedTags };
    }

    private static string GetMediaType(string imagePath) =>
        Path.GetExtension(imagePath).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".webp" => "image/webp",
            ".gif" => "image/gif",
            _ => "application/octet-stream"
        };

    private static string StripMarkdownCodeFence(string text)
    {
        var trimmed = text.Trim();

        if (!trimmed.StartsWith("```", StringComparison.Ordinal))
        {
            return trimmed;
        }

        var firstNewLine = trimmed.IndexOf('\n');
        var lastFence = trimmed.LastIndexOf("```", StringComparison.Ordinal);

        if (firstNewLine < 0 || lastFence <= firstNewLine)
        {
            return trimmed;
        }

        return trimmed[(firstNewLine + 1)..lastFence].Trim();
    }
}
