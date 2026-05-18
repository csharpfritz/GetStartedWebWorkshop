# Module 4: C# Basics in Context

[← Previous Module](03-blazor-fundamentals.md) | [Back to README](../README.md)

In Module 3, you created the `MyCollection` Blazor app and built a simple `/collection` page with a `List<string>`. In this module, you will keep building that same page, but you will organize the C# more like a real application.

Instead of keeping everything inside the Blazor project, you will create a separate class library named `MyCollection.Models`. That library will hold the `CollectionItem` model and any helper classes you add later.

## What you'll build in this module

By the end, you will be able to:

1. Create a class library project with `dotnet new classlib`
2. Add a local project reference with `dotnet add reference`
3. Create a `CollectionItem` class with properties
4. Use `List<CollectionItem>` on the Blazor page you started in Module 3
5. Write methods that respond to UI events and update the page
6. Render items with `@foreach` and a small reusable component

## Step 1: Start from your Module 3 app

Keep working with the `MyCollection` app you already created in Module 3.

Right now, your `Collection.razor` page probably looks something like this:

```razor
@page "/collection"
@rendermode InteractiveServer

<h1>My Collection</h1>

<input @bind="newItemName" placeholder="Enter an item name" />
<button @onclick="AddItem">Add item</button>

<ul>
    @foreach (var item in items)
    {
        <li>@item</li>
    }
</ul>

@code {
    private string newItemName = "";
    private List<string> items = new();

    private void AddItem()
    {
        if (string.IsNullOrWhiteSpace(newItemName))
        {
            return;
        }

        items.Add(newItemName);
        newItemName = "";
    }
}
```

That page works, but every item is only a string. We are ready for a richer type.

## Step 2: Create a class library project

From the folder that contains `MyCollection`, create a second project:

```powershell
dotnet new classlib -n MyCollection.Models
```

That command creates a **class library** project. A class library is a project that stores reusable C# code without being its own website.

In this workshop, the library will hold:

- The `CollectionItem` model
- Any helper classes you create later

This matters because separating models and logic from the UI is a common real-world pattern. It keeps your application easier to grow and easier to test.

## Step 3: Add a local project reference

Next, move into the main Blazor project and add a reference to the library:

```powershell
cd MyCollection
dotnet add reference ..\MyCollection.Models
```

A **project reference** tells .NET:

- Build the referenced project too
- Make its classes available in this project
- Let this project use code from that other project

After this, `MyCollection` can use classes from `MyCollection.Models`.

This is also good preparation for later modules. Soon you will add NuGet packages with a similar command such as:

```powershell
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

A project reference adds code from another project you own. A package reference adds code from a published NuGet package. Both are forms of **references**, and that concept is worth learning early.

## Step 4: Move `CollectionItem` into the class library

Open the new `MyCollection.Models` project, replace the template file, and create `CollectionItem.cs`:

```csharp
namespace MyCollection.Models;

public class CollectionItem
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.Today;
    public bool IsFavorite { get; set; }
}
```

This class teaches several core C# ideas:

- `string` stores text
- `DateTime` stores a date value
- `bool` stores `true` or `false`
- **Properties** hold data on a class

Now the Blazor page can work with real objects instead of plain strings.

## Step 5: Use the model in the Blazor page

Open `Components\Pages\Collection.razor` in `MyCollection` and import the class library namespace:

```razor
@using MyCollection.Models
```

Now you can change your fields from simple text values to a richer combination of values:

```csharp
private string newItemName = string.Empty;
private string newItemDescription = string.Empty;
private string statusMessage = "Ready to add your first item.";
private List<CollectionItem> items = new();
```

### Why these fields matter

- `newItemName` stores the current text from the name input
- `newItemDescription` stores the description input
- `statusMessage` gives the user feedback
- `items` is a `List<T>` where `T` is `CollectionItem`

`List<T>` means “a list of one specific type.” Here, that type is `CollectionItem`.

## Step 6: Add methods that work with the model

A **method** is a named block of code that does work.

Here is an `AddItem` method for the updated page:

```csharp
private void AddItem()
{
    if (string.IsNullOrWhiteSpace(newItemName))
    {
        statusMessage = "Enter a name before adding an item.";
        return;
    }

    var item = new CollectionItem
    {
        Name = newItemName,
        Description = string.IsNullOrWhiteSpace(newItemDescription)
            ? "Added from the Collection page."
            : newItemDescription,
        DateAdded = DateTime.Today
    };

    items.Add(item);
    statusMessage = $"Added: {item.Name}";
    newItemName = string.Empty;
    newItemDescription = string.Empty;
}
```

This method:

1. Validates the name
2. Creates a new `CollectionItem`
3. Adds it to `List<CollectionItem>`
4. Updates the status message
5. Clears the inputs

You can also update items after they already exist:

```csharp
private void ToggleFavorite(CollectionItem item)
{
    item.IsFavorite = !item.IsFavorite;
    statusMessage = item.IsFavorite
        ? $"{item.Name} is now marked as a favorite."
        : $"{item.Name} is no longer marked as a favorite.";
}
```

That keeps the event → method → update → render pattern from Module 3:

- The UI raises an event
- The event calls a C# method
- The method updates data
- Blazor re-renders the component

## Step 7: Iterate with `@foreach`

When you want to render every item, loop through the list:

```razor
@foreach (var item in items)
{
    <p>@item.Name</p>
}
```

`@foreach` means “repeat this markup for every item in the collection.”

Because `items` is a `List<CollectionItem>`, each `item` in the loop is a `CollectionItem`. That means you can access its properties like `item.Name`, `item.Description`, and `item.DateAdded`.

## Step 8: Add a simple reusable component

To show how Blazor components compose together, create `Components\CollectionItemCard.razor`:

```razor
@using MyCollection.Models

<article class="card shadow-sm">
    <div class="card-body">
        <h2 class="h5 card-title mb-2">@Item.Name</h2>
        <p class="card-text mb-2">@Item.Description</p>
        <p class="text-body-secondary mb-0">Added on @Item.DateAdded.ToShortDateString()</p>

        @if (Item.IsFavorite)
        {
            <span class="badge text-bg-success mt-3">Favorite</span>
        }
    </div>
</article>

@code {
    [Parameter, EditorRequired]
    public CollectionItem Item { get; set; } = default!;
}
```

This component receives one `CollectionItem` and displays it. The main page stays responsible for the list and button clicks.

## Step 9: Put the Module 3 page and Module 4 C# together

Here is a full version of `Collection.razor` that continues directly from the page you built in Module 3:

```razor
@page "/collection"
@rendermode InteractiveServer
@using MyCollection.Models

<PageTitle>My Collection</PageTitle>

<h1>My Collection</h1>
<p>Module 4 builds on the page from Module 3 by replacing simple strings with real C# objects.</p>

<div class="row g-3 align-items-end">
    <div class="col-md-4">
        <label for="newItemName" class="form-label">Item name</label>
        <input id="newItemName" class="form-control" @bind="newItemName" />
    </div>
    <div class="col-md-5">
        <label for="newItemDescription" class="form-label">Description</label>
        <input id="newItemDescription" class="form-control" @bind="newItemDescription" />
    </div>
    <div class="col-md-3">
        <button class="btn btn-primary w-100" @onclick="AddItem">Add item</button>
    </div>
</div>

<p class="mt-3">@statusMessage</p>

@if (items.Count == 0)
{
    <p>No items added yet.</p>
}
else
{
    <div class="mt-3">
        @foreach (var item in items)
        {
            <div class="mb-3">
                <CollectionItemCard Item="item" />
                <button class="btn btn-outline-secondary btn-sm mt-2" @onclick="() => ToggleFavorite(item)">
                    @(item.IsFavorite ? "Remove favorite" : "Mark as favorite")
                </button>
            </div>
        }
    </div>
}

@code {
    private string newItemName = string.Empty;
    private string newItemDescription = string.Empty;
    private string statusMessage = "Ready to add your first item.";
    private List<CollectionItem> items = new()
    {
        new CollectionItem
        {
            Name = "Starter item",
            Description = "This item shows how Module 4 continues from the Module 3 page.",
            DateAdded = DateTime.Today
        }
    };

    private void AddItem()
    {
        if (string.IsNullOrWhiteSpace(newItemName))
        {
            statusMessage = "Enter a name before adding an item.";
            return;
        }

        var item = new CollectionItem
        {
            Name = newItemName,
            Description = string.IsNullOrWhiteSpace(newItemDescription)
                ? "Added from the Collection page."
                : newItemDescription,
            DateAdded = DateTime.Today
        };

        items.Add(item);
        statusMessage = $"Added: {item.Name}";
        newItemName = string.Empty;
        newItemDescription = string.Empty;
    }

    private void ToggleFavorite(CollectionItem item)
    {
        item.IsFavorite = !item.IsFavorite;
        statusMessage = item.IsFavorite
            ? $"{item.Name} is now marked as a favorite."
            : $"{item.Name} is no longer marked as a favorite.";
    }
}
```

## Key ideas to remember

- **Types** tell C# what kind of data a value holds
- A **class** defines the shape of your data
- **Properties** hold values on a class
- **Methods** do work
- `List<T>` stores multiple values of the same type
- `@foreach` renders each item in a list
- A **project reference** makes code from one project available to another
- Separating UI from models and logic is a normal real-world pattern

## Step 10: Build the projects

When you finish the changes, build the app:

```powershell
dotnet build
```

Because `MyCollection` references `MyCollection.Models`, building the Blazor project also builds the class library.

---
## Next Module

Now that your C# is organized into reusable projects, you're ready to track those changes. Head to [Module 5: Git & GitHub](05-git-and-github.md).
