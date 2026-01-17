# MCP PR Reviewer Server (.NET)

A C#/.NET MCP server that reviews GitHub PRs using Claude.

## Setup

1. Build:
   - `cd mcp-pr-reviewer-dotnet`
   - `dotnet build`

2. Configure secrets (pick one option):

### Option A: Local settings file (recommended)
Edit `appsettings.local.json` in this folder and fill in your keys:
```json
{
  "GitHub": {
    "Token": "your_github_token_here"
  },
  "Anthropic": {
    "ApiKey": "your_anthropic_key_here"
  }
}
```
This file is git-ignored and will be copied to the build output.

### Option B: Environment variables
Set variables in your shell or MCP client config:
- `GITHUB_TOKEN` (for GitHub API access)
- `ANTHROPIC_API_KEY`

Optional env vars:
- `ANTHROPIC_MODEL` (default: `claude-sonnet-4-20250514`)
- `ANTHROPIC_VERSION` (default: `2023-06-01`)
- `ANTHROPIC_BASE_URL` (default: `https://api.anthropic.com/v1`)
- `REVIEW_MAX_CHARS`, `REVIEW_MAX_FILES`, `REVIEW_MAX_TOKENS`

## MCP client config example

Build first (`dotnet build`), then add this to your MCP config:

```
{
  "mcpServers": {
    "pr-reviewer-dotnet": {
      "command": "dotnet",
      "args": ["C:/Users/marka/source/repos/BookStar/mcp-pr-reviewer-dotnet/bin/Debug/net8.0/McpPrReviewer.dll"],
      "env": {
        "GITHUB_TOKEN": "ghp_...",
        "ANTHROPIC_API_KEY": "sk-ant-..."
      }
    }
  }
}
```

## Tools

- `list_open_prs`
  - Inputs: `owner`, `repo`, optional `state`, optional `limit`
  - Output: list of PRs with URLs

- `review_pr`
  - Inputs: `owner`, `repo`, `pr_number`, optional `instructions`, `max_files`, `max_chars`
  - Output: review text (uses Claude when `ANTHROPIC_API_KEY` is set)

## Notes

- Large PRs may be truncated with `REVIEW_MAX_CHARS` and `REVIEW_MAX_FILES`.
- `REVIEW_MAX_TOKENS` controls Claude response size.
- If `ANTHROPIC_API_KEY` is not set, the tool returns a PR summary instead of a review.
