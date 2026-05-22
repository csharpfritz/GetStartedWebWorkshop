using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace MyCollection.AiWorker;

public sealed class CollectionAnalysisStore(IConfiguration configuration)
{
    private readonly string connectionString =
        configuration.GetConnectionString("collectiondb")
        ?? "Data Source=..\\MyCollection\\MyCollection.db";

    public async Task SaveAsync(
        int collectionItemId,
        ImageAnalysisResult result,
        CancellationToken cancellationToken = default)
    {
        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE CollectionItems
            SET AiDescription = $description,
                AiTags = $tags
            WHERE Id = $id;
            """;

        command.Parameters.AddWithValue("$description", result.Description);
        command.Parameters.AddWithValue("$tags", string.Join(", ", result.Tags));
        command.Parameters.AddWithValue("$id", collectionItemId);

        var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);

        if (rowsAffected == 0)
        {
            throw new InvalidOperationException($"Collection item {collectionItemId} was not found.");
        }
    }
}
