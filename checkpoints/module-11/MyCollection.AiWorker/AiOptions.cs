namespace MyCollection.AiWorker;

public sealed class AiOptions
{
    public const string SectionName = "AI";

    public string Endpoint { get; set; } = "https://models.github.ai/inference";
    public string Model { get; set; } = "openai/gpt-4.1";
    public string ApiKey { get; set; } = string.Empty;
}
