# Copilot Instructions for GetStartedWebWorkshop

## Project Overview

This repository contains materials for the **"Get Started with Web Development"** live online workshop, taught by Jeffrey T. Fritz on May 22, 2026. The audience is beginners with no prior web development experience.

## Technology Stack

- **HTML** — page structure and semantic markup
- **C#** — server-side language fundamentals
- **Blazor** — interactive web UI framework (.NET)
- **.NET Aspire** — cloud-ready app orchestration
- **Git & GitHub** — version control and collaboration
- **Azure OpenAI** — adding AI features to the app
- **Azure** — cloud deployment target

## Repository Structure

```
docs/           — Markdown instructions for each workshop module (step-by-step guides)
checkpoints/    — Completed code for each module (learners can catch up here)
.squad/         — AI team coordination (do not modify manually)
.copilot/       — Copilot configuration and skills
```

## Conventions

### Documentation (`docs/`)

- Each module gets its own markdown file (e.g., `01-html-basics.md`, `02-blazor-intro.md`)
- Use numbered steps with clear expected outcomes
- Include "what you should see" checkpoints so learners can verify progress
- Write for absolute beginners — explain every concept, assume nothing
- Use fenced code blocks with language identifiers for all code samples
- Keep each module completable in 15-25 minutes of live instruction

### Checkpoints (`checkpoints/`)

- Each checkpoint folder matches a module number (e.g., `checkpoints/01/`, `checkpoints/02/`)
- Contains the complete working state of the project after that module
- Learners who fall behind can copy a checkpoint and continue from there
- All checkpoint code must build and run without errors

### Code Style

- Use latest C# language features appropriate for the .NET version in use
- Prefer minimal APIs and clean Blazor component patterns
- Keep code samples short and focused — workshop attendees are typing along live
- Add comments only where the "why" isn't obvious from context
- Use `dotnet watch` for development (hot reload)

### General

- Bias toward simplicity — this is a beginner workshop, not a best-practices showcase
- Every instruction should be verifiable ("you should see X")
- Prefer showing over telling — code samples over lengthy explanations
- Test all instructions on a clean machine before the workshop
