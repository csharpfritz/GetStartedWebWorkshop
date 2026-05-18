# Project Context

- **Owner:** Jeffrey T. Fritz
- **Project:** Get Started with Web Development — live online workshop (May 22, 2026)
- **Stack:** HTML, Blazor, C#, .NET Aspire, Git, GitHub, AI, Azure
- **Structure:** `docs/` for module instructions, `checkpoints/` for completed code
- **Audience:** Beginners
- **Created:** 2026-05-18

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->

## 2026-05-20T22:13:31.459-04:00 — Hermione's module restructuring background task completed

Hermione's background task for documentation reordering and dev tunnel guidance completed successfully (see orchestration-log/2026-05-20T22-13-hermione.md). Decision merged: Module 7/8 swap for dev tunnel-first flow. All docs 07–08 renamed, navigation updated, README table reordered, and QR code reference integrated. Decision entry updated to Active status.

## 2026-05-20T22:13:31.459-04:00 — Modules 7 and 8 swapped for phone-first flow

Renamed `docs\08-aspire.md` to `docs\07-aspire.md` and `docs\07-photo-upload.md` to `docs\08-photo-upload.md`, then updated adjacent navigation in `docs\06-entity-framework.md`, `docs\09-nservicebus.md`, and the workshop table in `README.md`.

Pattern noticed: when module order changes, this repo needs the file rename, the top and bottom navigation links, the README table row, and any in-body "connection to Module X" references updated together.

I also added a beginner-focused dev tunnel section to `docs\07-aspire.md` and added the QR-code/dev-tunnel context near the top of `docs\08-photo-upload.md` so the phone upload story stays clear.

## 2026-05-20T12:56:20-04:00 — Module 9 NServiceBus documentation authored

Wrote the full `docs/09-nservicebus.md` module in the same beginner-first style as the Aspire module. The module now teaches a shared `MyCollection.Messages` contract project, a `MyCollection.ThumbnailWorker` console endpoint, Learning Transport with a shared `.learningtransport` folder, WebP thumbnail generation with SkiaSharp, publishing from the Blazor upload flow, AppHost orchestration, and Aspire dashboard trace setup with `AddSource("NServiceBus.Core")`.

## 2026-05-20T22:34:50.050-04:00 — Team orchestration: Documentation updated for .NET 10 upgrade

**Hermione's contribution:**
- Updated root README.md with .NET 10 framework references
- Updated all checkpoint README files (modules 2–10) to reference .NET 10 as the target framework
- Updated module setup instructions and prerequisites to reflect .NET 10 as the current stable release

**Team outcome:**
- Snape completed checkpoint projects upgrade from net9.0 to net10.0
- Scribe recorded decisions in decisions.md: "User directive" and "Upgrade checkpoint projects to .NET 10" (both Active status)
- Orchestration logs created and session recorded in .squad/log/

## 2026-05-20T12:56:20-04:00 — Team spawn completed with decision documentation

Architectural decision documented and merged: Module 9 NServiceBus uses a dedicated `MyCollection.ThumbnailWorker` endpoint with `MyCollection.Messages` contract project and shared `.learningtransport` folder for reliable message communication during live workshop. Decision finalized for team alignment.

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

