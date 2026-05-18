# Module 5: Git & GitHub

[← Previous Module](04-csharp-basics.md) | [Back to README](../README.md)

In this module, you will take the `MyCollection` work from Module 4 and start tracking it with Git. That means your Blazor app and your `MyCollection.Models` class library will have a history you can review, save, and share on GitHub.

Git can feel strange at first because it adds new words like repository, stage, commit, and remote. That is normal. Go slowly, run the commands exactly as shown, and use `git status` often to see what is happening.

---

## 1. What Is Version Control?

**Expected outcome:** You understand what version control does and why it helps with your `MyCollection` project.

Version control is a system for tracking changes to your files over time.

A good beginner analogy is a **save point in a game**. Each time you make a useful change, you can create a save point. If something breaks later, you can go back and see what changed. Git is also a little like a very smart **undo history** that lasts across days, weeks, and even across different computers.

For your project, that means you can:

- Save important milestones as you build features
- See exactly which files changed
- Go back and review earlier versions
- Share your work on GitHub
- Work more confidently because mistakes are easier to recover from

Right now, your solution includes two projects:

- `MyCollection\`
- `MyCollection.Models\`

Git will track both of them together in one repository.

---

## 2. Installing and Configuring Git

**Expected outcome:** Git is installed on your machine, and your name and email are configured.

You may already have Git installed from your earlier VS Code setup. Let's check first.

```bash
git --version
```

If Git is installed, you will see output similar to this:

```text
git version 2.49.0.windows.1
```

If the command is not recognized, install Git from [https://git-scm.com](https://git-scm.com) and then reopen your terminal.

Next, configure the name and email that Git will attach to your commits:

```bash
git config --global user.name "Your Name"
git config --global user.email "you@example.com"
```

For example:

```bash
git config --global user.name "Jeffrey Fritz"
git config --global user.email "jeffrey@example.com"
```

These settings do **not** control how you sign into GitHub. They simply label your commits so Git knows who made them.

You can review your settings with:

```bash
git config --global --list
```

---

## 3. Initializing Your Repository

**Expected outcome:** Your project folder is now a Git repository.

Open a terminal in VS Code and move to the folder that contains **both** project folders:

```bash
cd D:\path\to\your\workshop-folder
```

This should be the folder that contains:

- `MyCollection\`
- `MyCollection.Models\`

Now initialize Git:

```bash
git init
```

Git will create a hidden folder named `.git`. That folder is where Git stores its history, branch information, and repository settings.

You do not edit the `.git` folder yourself, and you do not upload it as a normal project folder. Git manages it for you.

After `git init`, your folder becomes a **repository**. A repository is just a project folder that Git is tracking.

---

## 4. Staging, Commits, and History

**Expected outcome:** You can stage files, create your first commit, and view commit history.

Git has a simple three-state model that is helpful to know:

1. **Working directory** - your actual files on disk
2. **Staging area** - the list of changes you want in the next commit
3. **Repository history** - the saved commits Git remembers

Think of it like this:

- You edit files in the **working directory**
- You choose what to include by **staging** changes
- You save a permanent checkpoint by making a **commit**

Start by checking the state of your files:

```bash
git status
```

On a new repository, you will usually see untracked files because Git sees your projects but has not saved them yet.

Next, stage everything in the repository:

```bash
git add .
```

The dot means “from the current folder, include everything underneath it.”

Check the status again:

```bash
git status
```

Now Git should show those files as staged.

Create your first commit:

```bash
git commit -m "Initial commit: MyCollection app and Models library"
```

A commit message should explain what changed. Good commit messages are short and specific.

To see your history, run:

```bash
git log --oneline
```

You will see a compact list of commits, something like this:

```text
8f3c2d1 Initial commit: MyCollection app and Models library
```

That short ID on the left uniquely identifies the commit.

---

## 5. Creating a GitHub Repository

**Expected outcome:** You have an empty GitHub repository ready to receive your local code.

Now that your code is saved locally with Git, create a GitHub repository to publish it online.

1. Go to [https://github.com/new](https://github.com/new)
2. Sign in if needed
3. Enter a repository name, such as `MyCollection`
4. Choose **Public** for this workshop
5. **Do not** add a README
6. **Do not** add a `.gitignore`
7. Click **Create repository**

Why skip the README and `.gitignore` here? Because you already have a local repository, and you want GitHub to start empty so it connects cleanly to what you already created on your computer.

After the repository is created, GitHub will show you a page with commands for pushing an existing repository. Keep that page open and copy the repository URL.

It will look something like this:

```text
https://github.com/your-account/MyCollection.git
```

---

## 6. Connecting Local to Remote

**Expected outcome:** Your local repository is linked to GitHub, and your first commit is pushed.

In Git terms, GitHub is your **remote** repository. Your computer is the **local** repository.

Add GitHub as the `origin` remote:

```bash
git remote add origin https://github.com/your-account/MyCollection.git
```

You can verify it with:

```bash
git remote -v
```

If your default branch is already named `main`, you can push with:

```bash
git push -u origin main
```

If Git says your branch is named `master`, rename it first and then push:

```bash
git branch -M main
git push -u origin main
```

The `-u` flag tells Git to remember that your local `main` branch should push to `origin/main` by default in the future.

After the push finishes, refresh the repository page on GitHub. You should now see:

- Your project files and folders
- Your first commit
- A `main` branch
- The latest commit message near the top of the page

At that point, your code exists in two places: on your computer and on GitHub.

---

## 7. Basic Workflow: Edit -> Stage -> Commit -> Push

**Expected outcome:** You can make a small change and send it to GitHub using the normal Git workflow.

This is the cycle you will use over and over:

1. Edit files
2. Check what changed
3. Stage the files you want
4. Commit the change
5. Push to GitHub

Let's practice with a tiny change in `MyCollection.Models\CollectionItem.cs`.

Suppose your file currently looks like this:

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

Add a small comment above `Description`:

```csharp
namespace MyCollection.Models;

public class CollectionItem
{
    public string Name { get; set; } = string.Empty;
    // Short notes about the item.
    public string Description { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.Today;
    public bool IsFavorite { get; set; }
}
```

Now run:

```bash
git status
```

Git should show `MyCollection.Models\CollectionItem.cs` as modified.

Stage the change:

```bash
git add MyCollection.Models\CollectionItem.cs
```

You can also use `git add .` if you want to stage everything, but when you are learning, staging a specific file makes it easier to see what is happening.

Create a commit:

```bash
git commit -m "Add description to CollectionItem"
```

Push the commit to GitHub:

```bash
git push
```

That is the everyday workflow:

```text
edit -> status -> add -> commit -> push
```

Use it every time you finish a small, meaningful piece of work.

---

## 8. Using a .gitignore File

**Expected outcome:** Your repository ignores build output and user-specific files that should not be committed.

A `.gitignore` file tells Git which files and folders it should leave alone.

Ideally, you create `.gitignore` **before** your first commit. But if you already committed without it, that is okay. You can add it now.

Create a file named `.gitignore` in the repository root with this content:

```text
bin/
obj/
.vs/
*.user
```

These entries matter because:

- `bin/` contains build output
- `obj/` contains temporary build files
- `.vs/` contains Visual Studio workspace data
- `*.user` contains user-specific settings

You generally do **not** want to track generated files or personal machine settings in Git. They change often, they can be recreated, and they make repository history noisy.

After creating `.gitignore`, save it and stage it:

```bash
git add .gitignore
git commit -m "Add .gitignore for .NET build files"
git push
```

If you accidentally committed ignored files before creating `.gitignore`, Git may keep tracking them until you remove them from tracking. For this workshop, the main thing to learn is: create `.gitignore` early whenever you can.

---

## 9. Viewing History on GitHub

**Expected outcome:** You can find your code, commits, and diffs in the GitHub website.

Once your code is on GitHub, spend a minute learning the basic interface.

### Code tab

This is the main view of your repository. You can browse folders like:

- `MyCollection\`
- `MyCollection.Models\`

You will also see the latest commit message near the top of the file list.

### Commits

Near the top of the repository page, click the commit count or the latest commit message to open the commit history.

There you can:

- See every commit in order
- Read commit messages
- Click a specific commit

### Commit details and diff

When you click a commit, GitHub shows the **diff** for that commit.

A diff is a before-and-after view of what changed.

In GitHub's diff view:

- Removed lines are usually shown in red
- Added lines are usually shown in green

This is one of the most useful GitHub features because it helps you review exactly what changed in each step of your project.

---

## 10. Branches

**Expected outcome:** You know what a branch is and how to create one.

A branch is a separate line of development.

A simple way to think about it is a **parallel timeline**. Your `main` branch is the primary timeline. A feature branch lets you try a change without mixing it into `main` right away.

We will not use branches heavily in this workshop, but you should know they exist.

To see your branches:

```bash
git branch
```

You will usually see:

```text
* main
```

The asterisk shows your current branch.

To create a new branch:

```bash
git branch feature-name
```

For example:

```bash
git branch add-item-notes
```

That command creates the branch. Later, you can switch to it and do work there. For now, just remember the big idea: branches let you work on changes in isolation.

---

## Wrap-Up

You now know the beginner Git workflow for your `MyCollection` app:

- Initialize a repository with `git init`
- Check changes with `git status`
- Stage files with `git add`
- Save work with `git commit`
- View history with `git log --oneline`
- Push your work to GitHub with `git push`

From this point on, use Git as a habit while you build. Small commits made often are much easier to understand than one giant commit at the end.

---
## Next Module

[Module 6: Entity Framework Core & SQLite →](06-entity-framework.md)
