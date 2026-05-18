# Project Context

- **Owner:** Jeffrey T. Fritz
- **Project:** Get Started with Web Development — live online workshop (May 22, 2026)
- **Stack:** HTML, Blazor, C#, .NET Aspire, Git, GitHub, AI, Azure
- **Structure:** `docs/` for module instructions, `checkpoints/` for completed code
- **Audience:** Beginners
- **Created:** 2026-05-18

## Learnings

<!-- Append new learnings below. Each entry is something lasting about the project. -->
- 2026-05-18T09:56:15.272-04:00 — For beginners, Git/GitHub should move earlier as a light workflow checkpoint after the first Blazor progress, not remain a late standalone topic after databases.
- 2026-05-18T09:56:15.272-04:00 — The workshop should progress from page basics to Blazor interaction, then C# only when learners need it, followed by persistence, photo upload, messaging with NServiceBus, operational polish with Aspire, AI enrichment, and Azure deployment.
- 2026-05-18T09:56:15.272-04:00 — The sponsor demo fits best after learners already have file upload and persistence working; NServiceBus should be framed as background processing for uploaded photos, before any AI feature that consumes those photos.
- 2026-05-20T12:56:20-04:00 — **Module restructure completed:** Swapped modules 8 ↔ 9 (NServiceBus now Module 9, Aspire now Module 8). This orders observability after photo upload and before the AI module. Created Module 10 stub (10-add-ai.md) for AI content (pending full content from Snape). Updated all navigation links and README module table. Commit: 965ac29.
- 2026-05-20T12:56:20-04:00 — **Team spawn completed:** Hermione wrote full Module 9 NServiceBus documentation. Snape wrote full Module 10 AI documentation. Decision inbox processed: 8 files merged into decisions.md (no archive needed at 7892 bytes). Architectural decisions finalized for NServiceBus Learning Transport, Aspire CLI workflow, SQLite file-based strategy, and GitHub Models AI endpoint configuration.

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

