# Module 10 Checkpoint - MyCollection with AI Analysis

This checkpoint adds a second background worker that analyzes collection photos with AI.

## Included in this checkpoint
- `MyCollection.AiWorker` NServiceBus endpoint for image analysis
- `AnalyzeCollectionImage` follow-up event published after thumbnail creation
- `AiDescription` and `AiTags` stored on `CollectionItem`
- Direct SQLite updates from the AI worker using parameterized SQL
- AppHost configuration for GitHub Models-style endpoint settings
- UI updates that display AI-generated descriptions and tags
