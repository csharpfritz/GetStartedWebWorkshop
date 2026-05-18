# Project Context

- **Owner:** Jeffrey T. Fritz
- **Project:** Get Started with Web Development — live online workshop (May 22, 2026)
- **Stack:** HTML, Blazor, C#, .NET Aspire, Git, GitHub, AI, Azure
- **Structure:** `docs/` for module instructions, `checkpoints/` for completed code
- **Audience:** Beginners
- **Created:** 2026-05-18

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->

## 2026-05-20T22:34:50.050-04:00 — Team orchestration: Checkpoint projects upgraded to .NET 10

**Snape's contribution:**
- Updated every `checkpoints/**/*.csproj` TargetFramework from `net9.0` to `net10.0`, covering the Blazor app, model libraries, AppHosts, ServiceDefaults, message contracts, and worker projects.
- Kept `Aspire.AppHost.Sdk/13.3.4` unchanged in AppHost projects while moving those projects themselves to `net10.0`.
- Verified the migration by building `checkpoints/module-08/MyCollection.sln` successfully with the installed .NET 10 SDK.

**Team outcome:**
- Hermione updated documentation and READMEs to reflect .NET 10 target
- Scribe recorded decisions in decisions.md: "User directive" and "Upgrade checkpoint projects to .NET 10" (both Active status)
- Orchestration logs created and session recorded in .squad/log/

## 2026-05-20T22:13:31.459-04:00 — Snape's module restructuring background task completed

Snape's background task for checkpoint reorganization and external access implementation completed successfully (see orchestration-log/2026-05-20T22-13-snape.md). Decisions merged: Module 7/8 external access flow (QRCoder + dev tunnel integration). Both checkpoints build successfully and are ready for live workshop demo. Decision entry updated to Active status.

## 2026-05-20T22:13:31.459-04:00 — Module 7 and 8 checkpoints realigned for Aspire-first flow

- `checkpoints/module-07/` now represents the pure Aspire checkpoint: `MyCollection.AppHost`, `MyCollection.ServiceDefaults`, `aspire.config.json`, and `MyCollection.sln` form the 4-project orchestration setup while `MyCollection/Components/Pages/Collection.razor`, `MyCollection.Models/CollectionItem.cs`, and `MyCollection/Migrations/` were reset to the Module 6 EF-only state with no photo upload field or upload folder.
- `checkpoints/module-07/MyCollection.AppHost/Program.cs` and `checkpoints/module-08/MyCollection.AppHost/Program.cs` both use `WithExternalHttpEndpoints()` so `aspire run` can expose the app through a dev tunnel for phone testing.
- `checkpoints/module-08/` remains the cumulative photo-upload checkpoint and now explicitly includes Aspire infrastructure plus a `QRCoder`-generated QR code on `MyCollection/Components/Pages/Home.razor` that renders `Navigation.BaseUri` for quick mobile access.
- Updated checkpoint READMEs preserve the teaching sequence: Module 7 teaches Aspire orchestration and external access first; Module 8 layers photo uploads and QR onboarding on top of it.

## 2026-05-18T13:44:54-04:00 — Module 9 Aspire: architecture decisions and file paths

**docs/09-aspire.md** created (~498 lines) following Module 6/7 style.

Key architectural decisions and patterns established for this module:

- **Three-project Aspire structure:** `MyCollection.AppHost` (orchestrator), `MyCollection.ServiceDefaults` (shared telemetry/health library), `MyCollection` (application). Both new projects generated from `dotnet new aspire-apphost` and `dotnet new aspire-servicedefaults` templates.
- **SQLite not registered as Aspire resource:** SQLite is file-based and does not need container lifecycle management or connection-string injection. The existing `"Data Source=MyCollection.db"` literal is sufficient. A note was added explaining how this changes for network databases (PostgreSQL pattern documented in section 9).
- **AppHost Program.cs pattern:** `builder.AddProject<Projects.MyCollection>("mycollection")` — resource name `"mycollection"` is lowercase, matching Aspire conventions for environment variable naming.
- **ServiceDefaults `Extensions.cs`:** The generated file is presented in full as a teaching artifact. Students are explicitly told not to modify it. Key methods explained: `AddServiceDefaults()`, `ConfigureOpenTelemetry()`, `AddDefaultHealthChecks()`, `MapDefaultEndpoints()`.
- **Two lines added to `MyCollection/Program.cs`:** `builder.AddServiceDefaults()` (before `builder.Build()`) and `app.MapDefaultEndpoints()` (after `app.Build()`).
- **OTLP environment variable:** Aspire sets `OTEL_EXPORTER_OTLP_ENDPOINT` before starting child processes. `ServiceDefaults` reads this and activates the exporter. Students never set this manually.
- **Dashboard login token:** Explained as a local-only guard, not authentication. Token is printed to terminal on each start.
- **`git diff --cached --stat` introduced** as a pre-commit review habit in section 11.
- **Troubleshooting section added (section 12):** Covers `Projects.*` namespace missing, resource stuck in Starting state, `AddServiceDefaults` placement error, dashboard certificate issues.
- **OpenTelemetry vendor-neutrality** highlighted: the same `OTEL_EXPORTER_OTLP_ENDPOINT` variable works for Azure Monitor, Grafana, etc. — application code doesn't change.

## 2026-05-18T12:31:39-04:00 — Module 7 Photo Upload: key patterns and file paths

**docs/07-photo-upload.md** fully expanded from outline (651 lines) following Module 6 style.

Key architectural decisions made for this module:

- **File storage strategy:** Store only the filename string (`string?`, nullable) in `CollectionItem.PhotoFileName`. Images live in `wwwroot/uploads/` on disk and are served by ASP.NET Core static files middleware. Binary storage in SQLite was explicitly rejected for performance reasons.
- **Unique filename pattern:** `Guid.NewGuid() + Path.GetExtension(file.Name)` — prevents collisions without any database coordination.
- **IWebHostEnvironment injection:** Used `@inject IWebHostEnvironment Env` in `Collection.razor` to resolve the physical `wwwroot` path. No `Program.cs` registration needed — it is provided by ASP.NET Core automatically.
- **IBrowserFile handling:** `InputFile` component fires `OnChange` → `IBrowserFile` is stored in a field → validated and written to disk in `SavePhotoAsync` when Add is clicked. File is not read until the user confirms the form.
- **Validation order in SavePhotoAsync:** Size check first (no I/O), content type check second (no I/O), then disk write. Validation returns `null` with `statusMessage` set so `AddItemAsync` can early-return without adding the item.
- **MaxFileSize constant = 5 * 1024 * 1024 (5 MB)** — passed to both the validation guard and `OpenReadStream()` to prevent Blazor from buffering oversized files.
- **AllowedContentTypes:** `image/jpeg`, `image/png`, `image/gif`, `image/webp` — declared as `private static readonly string[]` for easy future changes.
- **`wwwroot/uploads/` in Git:** `.gitkeep` tracks the folder; `.gitignore` excludes all contents except `.gitkeep` using `!` exception pattern. `Directory.CreateDirectory` in code creates the folder at runtime if missing.
- **Migration name:** `AddPhotoFileName` — adds `TEXT nullable: true` column to `CollectionItems`.
- **Render mode:** `InteractiveServer` (unchanged from Module 6).
- **Module 7 builds on Module 6's `Collection.razor` CRUD page** — the full updated `Collection.razor` is included at the end as a reference listing.
- **capture attribute** (Section 8): `capture="environment"` on `InputFile` for mobile camera access. Noted as optional/use-case-specific; default in workshop leaves it off so users can choose gallery or camera.
- **Magic bytes validation** mentioned as a "beyond scope" note in Section 7 for awareness without scope creep.

## 2026-05-18T10:29:28.568-04:00 — Module 4 teaches project references early

Module 4 now continues directly from the Module 3 `MyCollection` Blazor page by moving `CollectionItem` into a sibling `MyCollection.Models` class library and adding a local project reference from the app. The checkpoint also demonstrates simple component composition with `CollectionItemCard.razor`, so later backend and data modules should keep using the shared model library rather than moving models back into the UI project.

## 2026-05-18 — 11-Module Workshop Outline Approved

The workshop curriculum has been finalized and approved by McGonagall. The 11-module structure is:

1. Setup
2. HTML
3. Blazor
4. C#
5. Git/GitHub
6. EF/SQLite
7. Photo Upload
8. NServiceBus
9. Aspire
10. AI
11. Azure Deploy

**Note:** NServiceBus photo demo is positioned after the photo upload module as part of the integrated workflow.

## 2026-05-18 — Team Synchronization: Modules 2-4 Complete

**Orchestration Status:** All three agents completed assigned work. Key team decisions recorded:
- No C# Dev Kit promotion (free extension only)
- Module 3 must include Blazor render modes and app scaffolding
- Generic terminology for collection items (no hat-specific references)
- No authentication/authorization in workshop

**Snape's Work:** Rewrote Module 3 (Blazor) documentation covering ServerInteractive, SSR, WebAssemblyInteractive render modes with `dotnet new` app scaffolding. Revised Module 4 (C#) with CollectionItem generic model. Documentation now aligned with user directives and Hagrid's working checkpoints.

**Team Coordination:** Lupin (Module 2 docs/checkpoints), Hagrid (Module 3-4 checkpoints), and Snape (Module 3-4 docs) have completed their phase. All output checked for consistency.


## 2026-05-18T13:54:03-04:00 — Module 9 Aspire rewritten for standalone CLI flow

- Rewrote `docs/09-aspire.md` around the current Aspire CLI workflow: install the standalone CLI, verify with `aspire --version`, initialize with `aspire init`, and run with `aspire run`. This entry supersedes the earlier template-based Module 9 note from the same day.
- Documented the solution-based outcome for this workshop: `MyCollection.AppHost` plus `MyCollection.ServiceDefaults`, with `builder.AddProject<Projects.MyCollection>("mycollection")` in the AppHost and `builder.AddServiceDefaults()` / `app.MapDefaultEndpoints()` in `MyCollection\Program.cs`.
- Explicitly called out that SQLite remains a normal EF Core file connection and is not modeled as an Aspire resource in Module 9.
- Added a direct old-vs-new comparison so future edits do not regress back to `dotnet workload install aspire` or manual `dotnet new aspire-*` instructions.

## 2026-05-20T12:56:20-04:00 — Module 10 AI worker documentation completed and team spawn finalized

- Wrote `docs/10-add-ai.md` as the full Module 10 guide in the same teaching style as the Aspire module: expected outcomes, numbered steps, and "What just happened" summaries.
- Standardized Module 10 on a second console app named `MyCollection.ImageAnalysisWorker`, implemented as another NServiceBus endpoint that subscribes to a follow-up `AnalyzeCollectionImage` event after thumbnail processing completes.
- Used `Microsoft.Extensions.AI` with an OpenAI-compatible client so the workshop starts with GitHub Models and can switch to an instructor-provided endpoint through configuration only.
- Chose to persist AI output as `AiDescription` and `AiTags` columns on `CollectionItems` so the Blazor UI can render generated metadata directly instead of reading a separate file.

Team spawn completed: All architectural decisions finalized and documented in decisions.md (no archive needed at 7892 bytes). Decisions merged: Module 10 GitHub Models endpoint configuration, Aspire CLI workflow standardization, SQLite file-based strategy, and NServiceBus Learning Transport architecture.

## 2026-05-20T15:44:15-04:00 — Checkpoints for modules 5 through 10 built cumulatively

- Created `checkpoints/module-05/` through `checkpoints/module-10/` as cumulative solution snapshots starting from `checkpoints/module-04/`, with every new checkpoint targeting `.NET 9` and including a checkpoint-level `README.md` plus a `MyCollection.sln` solution.
- Added Git onboarding files in `checkpoints/module-05/.gitignore` and `checkpoints/module-05/README.md`, then layered EF Core + SQLite in `checkpoints/module-06/MyCollection/Data/CollectionContext.cs`, `checkpoints/module-06/MyCollection/Components/Pages/Collection.razor`, and the Module 6 migrations under `checkpoints/module-06/MyCollection/Migrations/`.
- Added photo upload storage in `checkpoints/module-07/MyCollection/Components/Pages/Collection.razor`, `checkpoints/module-07/MyCollection.Models/CollectionItem.cs`, and `checkpoints/module-07/MyCollection/wwwroot/uploads/.gitkeep`, with the `AddPhotoFileName` migration included.
- Added Aspire orchestration in `checkpoints/module-08/aspire.config.json`, `checkpoints/module-08/MyCollection.AppHost/Program.cs`, and `checkpoints/module-08/MyCollection.ServiceDefaults/Extensions.cs`, then extended the solution with NServiceBus in `checkpoints/module-09/MyCollection.Messages/ProcessPhotoUploaded.cs`, `checkpoints/module-09/MyCollection.ThumbnailWorker/`, and the updated `checkpoints/module-09/MyCollection/Program.cs` / `Collection.razor` publisher flow.
- Added AI processing in `checkpoints/module-10/MyCollection.AiWorker/`, `checkpoints/module-10/MyCollection.Messages/AnalyzeCollectionImage.cs`, `checkpoints/module-10/MyCollection.Models/CollectionItem.cs`, `checkpoints/module-10/MyCollection/Components/CollectionItemCard.razor`, and the `AddAiDescriptionAndTags` migration, then validated each checkpoint by building its solution successfully.
