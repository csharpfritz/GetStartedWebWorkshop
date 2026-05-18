# McGonagall Module Outline Review

- **Date:** 2026-05-18T09:56:15.272-04:00
- **Requested by:** Jeffrey T. Fritz
- **Decision area:** Workshop structure and module ordering

## Decision

Revise the workshop into a tighter beginner-first progression that introduces source control earlier, delays deep .NET concepts until learners need them, and places the NServiceBus photo-processing demo after photo upload/persistence but before AI.

## Rationale

1. Beginners need visible wins early: page structure, simple Blazor UI, then just enough C# to understand event handlers and state.
2. Git and GitHub are more teachable once learners have made a meaningful change worth saving, but they should appear before the stack becomes operationally heavy.
3. Database work should come after learners understand the app model and basic C#.
4. Photo upload must exist before messaging is introduced, otherwise NServiceBus appears abstract.
5. Aspire, AI, and Azure are valuable, but together they are ambitious for one live day; each must be demonstrated with strict scope control.

## Recommended Outline

1. **Setup and orientation**  
   Install prerequisites, open the starter solution in Visual Studio Code, explain the workshop path, and run the starting app.

2. **HTML foundations and the collection page**  
   Introduce HTML structure and basic page composition by building the first visible collection experience.

3. **Blazor fundamentals: components, binding, and events**  
   Show how the UI becomes interactive in Blazor before naming too many .NET internals.

4. **C# basics in context**  
   Teach variables, types, methods, simple models, and event-handling logic only as needed for the app.

5. **Save your work with Git and GitHub**  
   Create the repository flow once learners have real progress, but before the app grows more complex.

6. **Data persistence with Entity Framework and SQLite**  
   Store collection items so the app becomes a real application rather than a temporary demo.

7. **Photo capture and upload for collection items**  
   Add the sponsor-relevant feature first: attach photos to items and persist the upload metadata.

8. **Background processing with NServiceBus**  
   Use NServiceBus to process uploaded photos asynchronously (for example thumbnailing, metadata extraction, moderation, or notification), making the messaging story concrete and justified.

9. **Add Aspire for orchestration and observability**  
   Introduce Aspire only after the app has multiple moving parts worth orchestrating.

10. **Add AI to enrich the collection experience**  
    Use AI after photo upload and messaging are in place so AI can classify, describe, or suggest details for uploaded items.

11. **Deploy to Azure**  
    End with a constrained deployment story that shows the path to production without drowning beginners in portal complexity.

## Teaching Notes

- Module 3 should absorb most of the original “Introducing .NET and Blazor” content; keep it practical.
- Module 4 should stay intentionally narrow. Do not drift into general C# instruction unrelated to the workshop app.
- Module 7 and 8 should be taught as one narrative: user uploads a photo, the app saves the request, NServiceBus handles work in the background.
- Module 9 through 11 are the danger zone for scope. If time is tight, keep Aspire, AI, and Azure as guided demos rather than full build-along segments.

## Scope Judgment

This is achievable in one live workshop day only if the workshop uses starter checkpoints and keeps later modules tightly demo-driven. Without checkpoints, the full list is too much for beginners.

## Specific Review of Original Outline

- **Git/GitHub at module 6:** too late. Better after learners have a modest working app but before database, messaging, and cloud topics accumulate.
- **Original module 3 and 4 split:** acceptable in principle, but the .NET introduction must remain lighter than the C# teaching. Beginners do not need platform taxonomy before they can click a button and see state change.
- **NServiceBus placement:** yes, between Aspire and AI is close, but it should come immediately after the photo upload feature is introduced. Messaging must follow a concrete user action, not precede it.
- **One-day feasibility:** possible only with ruthless scope control, starter assets, and a willingness to demo the advanced tail end instead of building every step from nothing.

