using Microsoft.EntityFrameworkCore;
using MyCollection.Models;

namespace MyCollection.Data;

public class CollectionContext(DbContextOptions<CollectionContext> options) : DbContext(options)
{
    public DbSet<CollectionItem> CollectionItems => Set<CollectionItem>();
}
