# MCP PR Reviewer Server (.NET)

A C#/.NET tool that reviews GitHub PRs using Claude. Works as both an **MCP server** (for Cursor/Claude Desktop) and a **CLI tool** (for GitHub Actions/pipelines).

## Setup

1. Build:
   ```bash
   cd mcp-pr-reviewer-dotnet
   dotnet build
   ```

2. Configure secrets (pick one option):

### Option A: Local settings file (recommended for local dev)
Edit `appsettings.local.json` in this folder:
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

### Option B: Environment variables (recommended for CI/CD)
- `GITHUB_TOKEN` (required for GitHub API access)
- `ANTHROPIC_API_KEY` (required for AI reviews)
- `ANTHROPIC_MODEL` (default: `claude-sonnet-4-20250514`)
- `ANTHROPIC_VERSION` (default: `2023-06-01`)
- `ANTHROPIC_BASE_URL` (default: `https://api.anthropic.com/v1`)
- `REVIEW_MAX_CHARS`, `REVIEW_MAX_FILES`, `REVIEW_MAX_TOKENS`

---

## CLI Usage

Run directly from the command line without an MCP client:

```bash
# Review a PR
dotnet McpPrReviewer.dll review --owner microsoft --repo vscode --pr 12345

# Review with custom instructions
dotnet McpPrReviewer.dll review -o myorg -r myrepo -p 42 --instructions "Focus on security"

# Save review to file
dotnet McpPrReviewer.dll review -o myorg -r myrepo -p 42 --output review.md

# List open PRs
dotnet McpPrReviewer.dll list --owner microsoft --repo vscode

# List closed PRs
dotnet McpPrReviewer.dll list -o microsoft -r vscode --state closed --limit 10

# Help
dotnet McpPrReviewer.dll help
```

### CLI Commands

| Command | Description |
|---------|-------------|
| `review` | Generate an AI code review for a pull request |
| `list` | List pull requests in a repository |
| `help` | Show help message |
| `version` | Show version information |

### Review Options

| Option | Short | Description |
|--------|-------|-------------|
| `--owner` | `-o` | Repository owner (required) |
| `--repo` | `-r` | Repository name (required) |
| `--pr` | `-p` | Pull request number (required) |
| `--instructions` | `-i` | Additional review instructions |
| `--max-files` | | Maximum files to include (default: 100) |
| `--max-chars` | | Maximum diff characters (default: 120000) |
| `--output` | `-O` | Write output to file |

---

## GitHub Actions Integration

Add this workflow to `.github/workflows/pr-review.yml`:

```yaml
name: AI PR Review

on:
  pull_request:
    types: [opened, synchronize]

permissions:
  contents: read
  pull-requests: write

jobs:
  review:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'

      - name: Checkout PR Reviewer
        uses: actions/checkout@v4
        with:
          repository: your-username/your-repo
          path: pr-reviewer
          sparse-checkout: mcp-pr-reviewer-dotnet

      - name: Build PR Reviewer
        run: dotnet build pr-reviewer/mcp-pr-reviewer-dotnet -c Release

      - name: Generate Review
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
          ANTHROPIC_API_KEY: ${{ secrets.ANTHROPIC_API_KEY }}
        run: |
          dotnet pr-reviewer/mcp-pr-reviewer-dotnet/bin/Release/net8.0/McpPrReviewer.dll \
            review \
            --owner ${{ github.repository_owner }} \
            --repo ${{ github.event.repository.name }} \
            --pr ${{ github.event.pull_request.number }} \
            --output review.md

      - name: Post Review Comment
        uses: actions/github-script@v7
        with:
          script: |
            const fs = require('fs');
            const review = fs.readFileSync('review.md', 'utf8');
            await github.rest.issues.createComment({
              owner: context.repo.owner,
              repo: context.repo.repo,
              issue_number: context.issue.number,
              body: `## 🤖 AI Code Review\n\n${review}`
            });
```

### Required Secrets

Add these to **Settings → Secrets and variables → Actions**:

| Secret | Description |
|--------|-------------|
| `ANTHROPIC_API_KEY` | Your Anthropic API key |

Note: `GITHUB_TOKEN` is automatically provided by GitHub Actions.

---

## MCP Server Mode

When run without arguments, it starts as an MCP server (stdin/stdout JSON-RPC).

### MCP Client Config (Cursor/Claude Desktop)

```json
{
  "mcpServers": {
    "pr-reviewer": {
      "command": "dotnet",
      "args": ["C:/path/to/McpPrReviewer.dll"],
      "env": {
        "GITHUB_TOKEN": "ghp_...",
        "ANTHROPIC_API_KEY": "sk-ant-..."
      }
    }
  }
}
```

### MCP Tools

| Tool | Inputs | Description |
|------|--------|-------------|
| `list_open_prs` | `owner`, `repo`, `state?`, `limit?` | List PRs with URLs |
| `review_pr` | `owner`, `repo`, `pr_number`, `instructions?`, `max_files?`, `max_chars?` | Generate AI review |

---

## Notes

- Large PRs are automatically truncated using `max_chars` and `max_files` settings
- If `ANTHROPIC_API_KEY` is not set, returns PR summary without AI review
- Progress messages go to stderr, review output goes to stdout
