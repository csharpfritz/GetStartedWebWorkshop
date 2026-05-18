# Squad Decisions

## Active Decisions

### 2026-05-18T10:09:38: No C# Dev Kit recommendation
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
- Do not promote or recommend the C# Dev Kit extension — it requires a paid subscription
- Only recommend the free C# extension (by Microsoft) unless a free license path is available for attendees

### 2026-05-18T10:15:36: Blazor render modes and app scaffolding in Module 3
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
- Module 3 (Blazor Fundamentals) must explain ServerInteractive, SSR, and WebAssemblyInteractive render modes
- Module 3 must create the first steps of the Collection application using `dotnet new`
- This is where the app is born

### 2026-05-18T10:15:36: Generic collection terminology (no hats references)
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
- Sample app is about collecting items, but do NOT directly reference or code "hats"
- Keep it generic — use "collection items," "items," etc.
- App should be relatable to any collection hobby, not specific to hats

### 2026-05-18T10:15:36: No authentication or authorization in workshop
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
- No security, authorization, or authentication features will be built
- Do not include auth/login/identity in any module content, sample code, or checkpoints
- Workshop is focused on web development fundamentals, not security concerns

### 2026-05-18T10:29: User directive
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
**What:** Each module must build on the previous module. Module 4 (C# Basics) should add features to the Blazor page defined in Module 3. Create a side class library project that students reference from the main app. Teach 'add a reference to a local project' in Module 4 to set up the pattern for adding EF Core NuGet reference later.
**Why:** User request — captured for team memory

### 2026-05-18T11:20:51: Module 6 architecture decisions
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
**What:**
- Use DI approach (AddDbContext<> in Program.cs), NOT OnConfiguring override
- Explain DI architecture to students as part of Module 6
- Include full CRUD (Create, Read, Update, Delete) in the module
- CollectionContext class goes in the MyCollection app project (not MyCollection.Models)
- Add *.db to .gitignore to reinforce Module 5 lessons
**Why:** User direction for Module 6 content structure

### 2026-05-18T12:31:39: Photo Storage Strategy for Module 7
**By:** Snape
**Status:** Active
**Decision:** Photos uploaded by users are stored as files in `wwwroot/uploads/` on the server. The database column (`PhotoFileName`, nullable `string?` on `CollectionItem`) stores only the filename, not the binary data.
**Rationale:**
- Storing binary image data in SQLite bloats the database and forces image bytes to be loaded on every query even when displaying only text metadata
- Static files middleware serves `wwwroot` content with zero C# overhead per request
- Storing only the filename decouples the file location from the data model — moving to cloud blob storage later requires changing only the save path and URL construction, not the database schema or model
**Filename Convention:** `Guid.NewGuid() + Path.GetExtension(originalFileName)` — unique, collision-free, no coordination required.

### 2026-05-18T13:44:54: SQLite Does Not Need an Aspire Resource Registration
**By:** Snape
**Status:** Proposed
**Decision:** The SQLite database used for the workshop is **not registered as an Aspire resource** in `MyCollection.AppHost/Program.cs`. The existing hardcoded connection string `"Data Source=MyCollection.db"` remains unchanged.
**Rationale:**
- SQLite has no network service to start or health-check — adding it as a resource would be artificial and confusing for beginners
- The EF Core connection string requires no injection — the file path is local and constant
- Introducing containers or Docker in this module would scope-creep into Module 11 (Azure Deploy)
- Module 9's teaching goal is **observability and orchestration**, not database infrastructure

### 2026-05-18T13:54:03: Module 9 Uses the Standalone Aspire CLI Flow
**By:** Snape
**Status:** Proposed
**Decision:** Module 9 documentation standardizes on the modern Aspire CLI workflow:
1. Install the standalone Aspire CLI
2. Verify with `aspire --version`
3. Run `aspire init` at the solution root
4. Use the project-based AppHost that is created because `MyCollection.sln` exists
5. Launch orchestration with `aspire run`
**Rationale:**
- This matches the current Aspire documentation and setup flow
- It removes outdated workload and template instructions that would confuse beginners
- It keeps the workshop aligned with the solution-based `MyCollection` repo structure
- It preserves a clean teaching story: AppHost for orchestration, `ServiceDefaults` for shared telemetry and health defaults

### 2026-05-20T12:56:20: Module restructure — swap 8 & 9, redefine 9 & 10
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
**What:**
- Swap modules 8 and 9: Aspire becomes Module 8, NServiceBus becomes Module 9
- Module 9 (NServiceBus): A sample console app that resizes an uploaded image to create a thumbnail and converts format to WebP
- Module 10 (AI): A second console application — an NServiceBus endpoint that analyzes the image using Microsoft Agent Framework and a Foundry endpoint (provided by instructor)
**Why:** User request — workshop flow improvement, AI module now builds on NServiceBus infrastructure

### 2026-05-20T12:56:20: Module 9 NServiceBus Architecture
**By:** Hermione
**Status:** Proposed
**Proposed Decision:** Document Module 9 around a dedicated console worker endpoint named `MyCollection.ThumbnailWorker`, a shared `MyCollection.Messages` contract project, and a shared solution-root `.learningtransport` folder so the Aspire-orchestrated web app and worker can communicate reliably during the workshop.
**Why this matters:**
1. It keeps the beginner story concrete: upload in the web app, thumbnail generation in the worker.
2. It avoids introducing external queue infrastructure during a live beginner workshop.
3. It makes the Aspire dashboard more meaningful because two orchestrated resources now participate in one observable workflow.
4. It prevents a confusing local failure mode where each endpoint uses a different Learning Transport root and never sees the other's messages.
**Impact on documentation:**
- The module teaches `ProcessPhotoUploaded` as a published event.
- The worker creates `200x200` WebP thumbnails with SkiaSharp.
- The AppHost adds the worker as a project resource.
- `ServiceDefaults` is updated to include `AddSource("NServiceBus.Core")` so message flow is visible in Aspire traces.

### 2026-05-20T12:56:20: Module 10 Defaults to GitHub Models with Configurable Endpoint Fallback
**By:** Snape
**Status:** Proposed
**Decision:** Module 10 uses **GitHub Models** as the first-choice endpoint and configures the worker through `Microsoft.Extensions.AI` plus an OpenAI-compatible client abstraction. The worker reads `AI:Endpoint`, `AI:Model`, and `AI:ApiKey` from configuration so an instructor-provided endpoint URL can replace GitHub Models without changing application code.
**Rationale:**
1. GitHub Models is easier for workshop attendees to access than provisioning a dedicated paid AI service up front.
2. Using `IChatClient` from `Microsoft.Extensions.AI` keeps the handler logic provider-neutral and teaches a durable abstraction pattern.
3. A configuration-only fallback protects the live workshop from endpoint outages or quota issues.
4. The approach keeps the AI lesson focused on application architecture rather than vendor-specific SDK code.
**Implications:**
- Module 10 documentation should describe GitHub Models first.
- Module 10 documentation should explicitly state: "If GitHub Models is unavailable, your instructor will provide an endpoint URL."
- Future code or docs for this module should prefer configuration changes over handler rewrites when switching AI providers.

### 2026-05-20T22:13:31: Module 7/8 swap for dev tunnel-first flow
**By:** Hermione
**Status:** Active
**Decision:** Place the Aspire module before the photo upload module so attendees configure observability and external access first.
**Rationale:**
- A phone cannot reach `localhost`, so photo capture and upload are much easier to teach after a dev tunnel exists.
- Aspire's AppHost configuration and `.WithExternalHttpEndpoints()` give a natural place to introduce external access before the QR-code-based phone flow.
- This keeps Module 8 focused on the user-facing photo experience instead of stopping to explain networking prerequisites.
**Documentation impact:**
- `docs/07-aspire.md` now includes dev tunnel setup guidance.
- `docs/08-photo-upload.md` now references the Home page QR code and points forward to Module 9.
- Adjacent navigation and README module links must treat Aspire as Module 7 and Photo Upload as Module 8.

### 2026-05-20T22:13:31: Module 7/8 external access flow (QR code & dev tunnel)
**By:** Snape
**Status:** Active
**Decision:** When the workshop sequence moves Aspire ahead of photo upload, both checkpoint AppHosts expose `MyCollection` with `WithExternalHttpEndpoints()`, and Module 8 adds a QR code on `Home.razor` using QRCoder to advertise the app URL (`Navigation.BaseUri`).
**Rationale:**
1. Dev tunnels let attendees open the Aspire-hosted app on a phone without introducing deployment steps early.
2. The QR code removes mobile typing friction exactly when the photo upload lesson starts needing camera access.
3. Keeping Module 8 cumulative preserves the workshop rule that each module builds on the previous one.
**Impacted files:**
- `checkpoints/module-07/MyCollection.AppHost/Program.cs`
- `checkpoints/module-08/MyCollection.AppHost/Program.cs`
- `checkpoints/module-08/MyCollection/Components/Pages/Home.razor`
- `checkpoints/module-08/MyCollection/MyCollection.csproj`

### 2026-05-20T22:34:50: User directive
**By:** Jeffrey T. Fritz (via Copilot)
**Status:** Active
**What:** All projects, checkpoints, and documentation must target .NET 10. .NET 10 has been available since November 2025.
**Why:** User request — captured for team memory

### 2026-05-20T22:34:50: Upgrade checkpoint projects to .NET 10
**By:** Snape
**Status:** Active
**Decision:** All projects under `checkpoints/` should target `net10.0` instead of `net9.0`. `MyCollection.AppHost` projects keep `Aspire.AppHost.Sdk/13.3.4`; only the project target framework changes.
**Rationale:**
- The workshop checkpoints should track the current stable .NET release rather than teaching against the previous major version.
- Keeping every checkpoint on the same target framework avoids confusion when attendees compare modules or copy code forward.
- The update was validated by a successful `dotnet build` of `checkpoints/module-08/MyCollection.sln` on the installed .NET 10 SDK.

## Governance

- All meaningful changes require team consensus
- Document architectural decisions here
- Keep history focused on work, decisions focused on direction
