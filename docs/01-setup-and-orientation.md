# Module 1: Setup & Orientation

[← Back to README](../README.md)

Welcome to **Get Started with Web Development**! I'm going to teach you how to build a real web application from scratch. In this module, we'll set up everything you need and get your first app running.

## What You'll Build

By the end of this workshop, you'll have built a **Collection App** — a web application where users can:
- Take photos of items they own
- Upload those photos to a collection
- See all their items displayed on a webpage
- Delete items from their collection

This is a real web app that runs on your computer during the workshop and could be deployed to the internet later.

## Prerequisites You'll Need to Install

Before we start, make sure you have these three tools on your computer. Don't worry — they're all free!

### 1. **Visual Studio Code** (Your Code Editor)

Visual Studio Code (often called "VS Code") is where you'll write all your code. Think of it like Microsoft Word, but for programming.

**Steps:**
1. Go to https://code.visualstudio.com
2. Click the big blue "Download" button
3. Run the installer and follow the setup steps
4. When it finishes, launch VS Code

**What you should see:** A welcome screen with a sidebar on the left showing icons and an empty editor area.

### 2. **.NET SDK** (Development Tools)

The .NET SDK gives your computer the tools needed to run and build web applications. You need version 10.0 or later.

**Steps:**
1. Go to https://dotnet.microsoft.com/download
2. Download the **.NET 10** SDK for your operating system
3. Run the installer and follow the setup steps
4. When done, open a terminal or PowerShell and type:
   ```
   dotnet --version
   ```
5. Press Enter

**What you should see:** A version number (like `10.0.x` or higher). If you see a version number, you're good to go!

> **Windows Tip:** To open a terminal, press `Win + R`, type `powershell`, and press Enter.

### 3. **Git** (Version Control)

Git helps track changes to your code over time. Think of it like "Track Changes" in Word, but for programming.

**Steps:**
1. Go to https://git-scm.com
2. Download the installer for your operating system
3. Run it and follow the setup steps (you can use all the default options)
4. When done, restart your computer

**What you should see:** You don't need to see anything specific right now — we'll use Git later in the workshop.

---

## VS Code Extensions (Supercharge Your Editor)

Extensions are add-ons that make VS Code smarter for specific programming languages. We need a couple of these to make web development easier — and trust me, they're worth it.

### Installing Extensions

**Steps:**
1. Open VS Code
2. On the left sidebar, find the **Extensions** icon (it looks like four small squares)
3. Click it to open the Extensions panel
4. In the search box at the top, type: `C#`
5. Click the result by Microsoft and click the blue **Install** button
6. Wait for it to finish (you might see a message asking to reload VS Code — go ahead and reload)
7. Repeat this process for this extension:
   - `REST Client` (by Huachao Mao) — this helps test web requests

**What you should see:** In the Extensions panel, you should see your installed extensions listed with a green checkmark and an "Uninstall" button.

---

## Opening the Starter Project

Now it's time to open the project for this workshop!

### Steps:

1. Open VS Code
2. Click **File** → **Open Folder**
3. Navigate to `D:\GetStartedWebWorkshop` (or wherever you saved this project)
4. Click **Select Folder**
5. You might see a notification asking about required assets — click **Yes** if prompted

**What you should see:** 
- The left sidebar shows a folder tree with files like `Program.cs` and folders like `wwwroot`
- You're now viewing the starter project

> **What's in this folder?**
> - **Program.cs** — The main file that tells your app how to run
> - **wwwroot/** — Stores images, CSS, and HTML that visitors see
> - **appsettings.json** — Configuration settings for your app
> - **.csproj** — A file that describes your project to .NET

---

## Running Your First App

Let's fire up the app and see it run!

### Steps:

1. At the bottom of VS Code, look for the **Terminal** area (if you don't see it, click **View** → **Terminal**)
2. In the terminal, type:
   ```
   dotnet watch
   ```
3. Press Enter

This command starts your app and watches for changes. When you edit files, the app automatically reloads — no manual restart needed!

**What you should see:**
- The terminal shows messages like `Building...` and then `Now listening on: https://localhost:7224`
- After a few seconds, your browser might open automatically
- If not, copy the URL (like `https://localhost:7224`) and paste it in your browser
- You should see a simple webpage with a title

> **Windows Tip:** The browser might show a security warning about the certificate — that's normal for local development. Click "Continue" or "Proceed anyway" to move forward.

### Your App is Running!

Congratulations! You've successfully started a real web application. The `dotnet watch` command is still running in the terminal — it'll keep your app up and running. You can leave it there.

**To stop the app later:** Press `Ctrl + C` in the terminal.

---

## Quick Tour: VS Code for Beginners

Now that your app is running, let's learn the VS Code interface. Here's what you'll see:

### Left Sidebar

The left sidebar has several icons:

1. **Explorer** (top icon, looks like two files) — Shows all your project files. Click a file to open it.
2. **Search** (looks like a magnifying glass) — Find text across your files.
3. **Source Control** (looks like three connected circles) — Shows Git changes (we'll use this later).
4. **Extensions** (four small squares) — Install add-ons for VS Code.
5. **Run & Debug** (play button with a bug) — Run and debug your apps.

### Main Editor Area

The middle area is where you write code. You can open multiple files as tabs at the top.

### Terminal Area

At the bottom, the terminal shows output from commands (like `dotnet watch`).

### Command Palette

Press `Ctrl + Shift + P` to open the Command Palette. This is like a search box that lets you run any VS Code command. Try typing "Format Document" to auto-format your code!

### Keyboard Shortcuts You'll Use

- `Ctrl + S` — Save a file
- `Ctrl + /` — Comment/uncomment a line
- `Ctrl + F` — Find text in the current file
- `Ctrl + Shift + F` — Find text across all files
- `F5` — Start debugging
- `Ctrl + K, Ctrl + C` — Add a comment
- `Alt + Up/Down` — Move a line up or down

---

## You're Ready!

You've completed Module 1! Your environment is set up, and you've got a running web app.

**Checkpoint:** Before moving to the next module, make sure:
- ✅ Visual Studio Code is installed and running
- ✅ .NET SDK is installed (you tested it with `dotnet --version`)
- ✅ Git is installed
- ✅ You've installed the C# and REST Client extensions
- ✅ You can open the starter project in VS Code
- ✅ `dotnet watch` successfully runs your app
- ✅ You can see the app in your browser at https://localhost:7224

---

## Next Module

Ready to build your first web page? Head to [Module 2: HTML Foundations](02-html-foundations.md) to start writing HTML!
