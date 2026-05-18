namespace MyCollection.Models;

public class CollectionItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.Today;
    public bool IsFavorite { get; set; }
    public string? PhotoFileName { get; set; }
}
