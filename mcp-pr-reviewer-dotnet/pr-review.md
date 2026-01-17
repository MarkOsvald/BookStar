## Summary

The PR refactors the navigation button handlers to use a common method for loading forms, improving code readability and maintainability.

## Findings

- [minor] The PR adds a new `.gitignore` file for the `mcp-pr-reviewer-dotnet` project, which is a good practice to exclude build outputs and local settings.
- [minor] The PR adds a new `McpPrReviewer.csproj` file, which is the project file for the .NET application.
- [minor] The PR adds a new `Program.cs` file, which is the main entry point for the .NET application. This file contains the implementation of the CLI and MCP server modes.

## Suggestions

No additional suggestions.

## Tests

The PR includes a new README.md file that provides detailed instructions for setting up, configuring, and using the .NET application in both CLI and MCP server modes. The README also includes information about integrating the tool with GitHub Actions for automated PR reviews.