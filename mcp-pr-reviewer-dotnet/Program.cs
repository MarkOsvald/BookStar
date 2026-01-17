using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McpPrReviewer;

internal static class Program
{
    private const string ServerName = "mcp-pr-reviewer-dotnet";
    private const string ServerVersion = "0.1.0";
    private const string DefaultProtocolVersion = "2024-11-05";

    public static async Task Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        var config = AppConfig.Load();
        var server = new McpServer(config);

        // CLI mode: detect command-line arguments
        if (args.Length > 0)
        {
            var exitCode = await RunCliAsync(args, server);
            Environment.Exit(exitCode);
            return;
        }

        // Default: MCP protocol mode (stdin/stdout JSON-RPC)
        await server.RunAsync();
    }

    private static async Task<int> RunCliAsync(string[] args, McpServer server)
    {
        var command = args[0].ToLowerInvariant();

        try
        {
            switch (command)
            {
                case "review":
                    return await RunReviewCommandAsync(args, server);

                case "list":
                    return await RunListCommandAsync(args, server);

                case "help":
                case "--help":
                case "-h":
                    PrintHelp();
                    return 0;

                case "version":
                case "--version":
                case "-v":
                    Console.WriteLine($"{ServerName} v{ServerVersion}");
                    return 0;

                default:
                    Console.Error.WriteLine($"Unknown command: {command}");
                    Console.Error.WriteLine("Use 'help' to see available commands.");
                    return 1;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    private static async Task<int> RunReviewCommandAsync(string[] args, McpServer server)
    {
        var owner = GetArgValue(args, "--owner", "-o");
        var repo = GetArgValue(args, "--repo", "-r");
        var prNumberStr = GetArgValue(args, "--pr", "-p");
        var instructions = GetArgValue(args, "--instructions", "-i");
        var maxFilesStr = GetArgValue(args, "--max-files");
        var maxCharsStr = GetArgValue(args, "--max-chars");
        var outputFile = GetArgValue(args, "--output", "-O");

        if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(repo) || string.IsNullOrEmpty(prNumberStr))
        {
            Console.Error.WriteLine("Usage: review --owner <owner> --repo <repo> --pr <number> [options]");
            Console.Error.WriteLine();
            Console.Error.WriteLine("Required:");
            Console.Error.WriteLine("  --owner, -o    Repository owner (user or organization)");
            Console.Error.WriteLine("  --repo, -r     Repository name");
            Console.Error.WriteLine("  --pr, -p       Pull request number");
            Console.Error.WriteLine();
            Console.Error.WriteLine("Optional:");
            Console.Error.WriteLine("  --instructions, -i   Additional review instructions");
            Console.Error.WriteLine("  --max-files          Maximum files to include (default: 100)");
            Console.Error.WriteLine("  --max-chars          Maximum diff characters (default: 120000)");
            Console.Error.WriteLine("  --output, -O         Write output to file instead of stdout");
            return 1;
        }

        if (!int.TryParse(prNumberStr, out var prNumber))
        {
            Console.Error.WriteLine($"Invalid PR number: {prNumberStr}");
            return 1;
        }

        int? maxFiles = int.TryParse(maxFilesStr, out var mf) ? mf : null;
        int? maxChars = int.TryParse(maxCharsStr, out var mc) ? mc : null;

        Console.Error.WriteLine($"Reviewing PR #{prNumber} in {owner}/{repo}...");

        var result = await server.ReviewPrCliAsync(owner, repo, prNumber, instructions, maxFiles, maxChars);

        if (!string.IsNullOrEmpty(outputFile))
        {
            await File.WriteAllTextAsync(outputFile, result);
            Console.Error.WriteLine($"Review written to: {outputFile}");
        }
        else
        {
            Console.WriteLine(result);
        }

        return 0;
    }

    private static async Task<int> RunListCommandAsync(string[] args, McpServer server)
    {
        var owner = GetArgValue(args, "--owner", "-o");
        var repo = GetArgValue(args, "--repo", "-r");
        var state = GetArgValue(args, "--state", "-s") ?? "open";
        var limitStr = GetArgValue(args, "--limit", "-l");

        if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(repo))
        {
            Console.Error.WriteLine("Usage: list --owner <owner> --repo <repo> [options]");
            Console.Error.WriteLine();
            Console.Error.WriteLine("Required:");
            Console.Error.WriteLine("  --owner, -o    Repository owner (user or organization)");
            Console.Error.WriteLine("  --repo, -r     Repository name");
            Console.Error.WriteLine();
            Console.Error.WriteLine("Optional:");
            Console.Error.WriteLine("  --state, -s    PR state: open, closed, all (default: open)");
            Console.Error.WriteLine("  --limit, -l    Maximum PRs to list (default: 20)");
            return 1;
        }

        int limit = int.TryParse(limitStr, out var lim) ? lim : 20;

        var result = await server.ListPrsCliAsync(owner, repo, state, limit);
        Console.WriteLine(result);

        return 0;
    }

    private static string? GetArgValue(string[] args, string longName, string? shortName = null)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            // Long names are case-insensitive, short names are case-sensitive
            if (args[i].Equals(longName, StringComparison.OrdinalIgnoreCase) ||
                (shortName != null && args[i].Equals(shortName, StringComparison.Ordinal)))
            {
                return args[i + 1];
            }

            // Support --arg=value format
            if (args[i].StartsWith($"{longName}=", StringComparison.OrdinalIgnoreCase))
            {
                return args[i].Substring(longName.Length + 1);
            }
            if (shortName != null && args[i].StartsWith($"{shortName}=", StringComparison.Ordinal))
            {
                return args[i].Substring(shortName.Length + 1);
            }
        }
        return null;
    }

    private static void PrintHelp()
    {
        Console.WriteLine($"{ServerName} v{ServerVersion}");
        Console.WriteLine();
        Console.WriteLine("A PR review tool powered by Claude. Can run as MCP server or CLI.");
        Console.WriteLine();
        Console.WriteLine("USAGE:");
        Console.WriteLine("  McpPrReviewer                    Run as MCP server (stdin/stdout)");
        Console.WriteLine("  McpPrReviewer <command> [args]   Run CLI command");
        Console.WriteLine();
        Console.WriteLine("COMMANDS:");
        Console.WriteLine("  review    Generate an AI code review for a pull request");
        Console.WriteLine("  list      List pull requests in a repository");
        Console.WriteLine("  help      Show this help message");
        Console.WriteLine("  version   Show version information");
        Console.WriteLine();
        Console.WriteLine("EXAMPLES:");
        Console.WriteLine("  McpPrReviewer review --owner microsoft --repo vscode --pr 12345");
        Console.WriteLine("  McpPrReviewer list --owner microsoft --repo vscode --state open");
        Console.WriteLine("  McpPrReviewer review -o myorg -r myrepo -p 42 --output review.md");
        Console.WriteLine();
        Console.WriteLine("ENVIRONMENT VARIABLES:");
        Console.WriteLine("  GITHUB_TOKEN        GitHub API token (required)");
        Console.WriteLine("  ANTHROPIC_API_KEY   Anthropic API key (required for AI reviews)");
        Console.WriteLine("  ANTHROPIC_MODEL     Model to use (default: claude-sonnet-4-20250514)");
        Console.WriteLine();
        Console.WriteLine("Or configure via appsettings.local.json in the app directory.");
    }

    private sealed class McpServer
    {
        private readonly AppConfig _config;
        private readonly HttpClient _githubHttp;
        private readonly HttpClient _anthropicHttp;

        public McpServer(AppConfig config)
        {
            _config = config;
            _githubHttp = new HttpClient();
            _githubHttp.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(ServerName, ServerVersion));
            _githubHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            _anthropicHttp = new HttpClient();
        }

        // CLI entry points for direct invocation
        public async Task<string> ReviewPrCliAsync(string owner, string repo, int prNumber, 
            string? instructions = null, int? maxFiles = null, int? maxChars = null, 
            CancellationToken cancellationToken = default)
        {
            var effectiveMaxFiles = maxFiles ?? _config.ReviewMaxFiles;
            var effectiveMaxChars = maxChars ?? _config.ReviewMaxChars;

            var pr = await GitHubGetAsync<GitHubPull>(
                $"/repos/{owner}/{repo}/pulls/{prNumber}",
                null,
                cancellationToken);

            var files = await GetPullFilesAsync(owner, repo, prNumber, effectiveMaxFiles, cancellationToken);
            var diff = BuildDiff(files, effectiveMaxChars);
            var summary = BuildSummary(pr, files, effectiveMaxFiles);
            var prompt = BuildPrompt(pr, files, diff.DiffText, diff.Truncated, instructions);
            var review = await GenerateReviewAsync(prompt, summary, cancellationToken);

            return review;
        }

        public async Task<string> ListPrsCliAsync(string owner, string repo, string state = "open", 
            int limit = 20, CancellationToken cancellationToken = default)
        {
            var prs = await GitHubGetAsync<List<GitHubPull>>(
                $"/repos/{owner}/{repo}/pulls",
                new Dictionary<string, string>
                {
                    ["state"] = state,
                    ["per_page"] = limit.ToString(),
                },
                cancellationToken);

            if (prs.Count == 0)
            {
                return "No pull requests found.";
            }

            var lines = prs.Select(pr =>
            {
                var title = string.Join(" ", pr.Title.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                return $"#{pr.Number} {title} ({pr.State}) - {pr.HtmlUrl}";
            });

            return string.Join("\n", lines);
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            var input = Console.OpenStandardInput();
            var output = Console.OpenStandardOutput();

            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await ReadMessageAsync(input, cancellationToken);
                if (message is null) break;

                await HandleMessageAsync(message.Value, output, cancellationToken);
                message.Value.Dispose();
            }
        }

        private async Task HandleMessageAsync(RpcMessage message, Stream output, CancellationToken cancellationToken)
        {
            try
            {
                if (!message.Root.TryGetProperty("method", out var methodElement) ||
                    methodElement.ValueKind != JsonValueKind.String)
                {
                    await WriteErrorAsync(output, message.Id, -32600, "Invalid Request", cancellationToken);
                    return;
                }

                var method = methodElement.GetString() ?? string.Empty;
                var hasId = message.HasId;

                if (method == "initialize")
                {
                    var protocolVersion = DefaultProtocolVersion;
                    if (message.Root.TryGetProperty("params", out var initParams) &&
                        initParams.ValueKind == JsonValueKind.Object &&
                        initParams.TryGetProperty("protocolVersion", out var protocolElement) &&
                        protocolElement.ValueKind == JsonValueKind.String)
                    {
                        protocolVersion = protocolElement.GetString() ?? DefaultProtocolVersion;
                    }

                    if (hasId)
                    {
                        var result = new InitializeResult
                        {
                            ProtocolVersion = protocolVersion,
                            Capabilities = new ServerCapabilities { Tools = new Dictionary<string, object>() },
                            ServerInfo = new ServerInfo { Name = ServerName, Version = ServerVersion },
                        };
                        await WriteResultAsync(output, message.Id, result, cancellationToken);
                    }
                    return;
                }

                if (method == "initialized") return;

                if (method == "tools/list")
                {
                    if (!hasId) return;
                    var result = new ToolsListResult { Tools = BuildToolDefinitions() };
                    await WriteResultAsync(output, message.Id, result, cancellationToken);
                    return;
                }

                if (method == "tools/call")
                {
                    if (!hasId) return;
                    var toolResult = await HandleToolCallAsync(message.Root, cancellationToken);
                    await WriteResultAsync(output, message.Id, toolResult, cancellationToken);
                    return;
                }

                if (hasId)
                {
                    await WriteErrorAsync(output, message.Id, -32601, $"Method not found: {method}", cancellationToken);
                }
            }
            catch (Exception ex)
            {
                if (message.HasId)
                {
                    await WriteErrorAsync(output, message.Id, -32603, ex.Message, cancellationToken);
                }
            }
        }

        private async Task<ToolCallResult> HandleToolCallAsync(JsonElement root, CancellationToken cancellationToken)
        {
            try
            {
                if (!root.TryGetProperty("params", out var parameters) ||
                    parameters.ValueKind != JsonValueKind.Object)
                {
                    return ToolCallResult.Error("Missing params for tools/call.");
                }

                if (!parameters.TryGetProperty("name", out var nameElement) ||
                    nameElement.ValueKind != JsonValueKind.String)
                {
                    return ToolCallResult.Error("Tool name is required.");
                }

                var toolName = nameElement.GetString() ?? string.Empty;
                var arguments = parameters.TryGetProperty("arguments", out var argsElement)
                    ? argsElement
                    : default;

                return toolName switch
                {
                    "list_open_prs" => await HandleListOpenPrsAsync(arguments, cancellationToken),
                    "review_pr" => await HandleReviewPrAsync(arguments, cancellationToken),
                    _ => ToolCallResult.Error($"Unknown tool: {toolName}"),
                };
            }
            catch (Exception ex)
            {
                return ToolCallResult.Error(ex.Message);
            }
        }

        private async Task<ToolCallResult> HandleListOpenPrsAsync(JsonElement arguments, CancellationToken cancellationToken)
        {
            var owner = GetRequiredString(arguments, "owner");
            var repo = GetRequiredString(arguments, "repo");
            var state = GetOptionalString(arguments, "state") ?? "open";
            var limit = GetOptionalInt(arguments, "limit") ?? 20;

            var prs = await GitHubGetAsync<List<GitHubPull>>(
                $"/repos/{owner}/{repo}/pulls",
                new Dictionary<string, string>
                {
                    ["state"] = state,
                    ["per_page"] = limit.ToString(),
                },
                cancellationToken);

            if (prs.Count == 0)
            {
                return ToolCallResult.Text("No pull requests found.");
            }

            var lines = prs.Select(pr =>
            {
                var title = string.Join(" ", pr.Title.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                return $"#{pr.Number} {title} ({pr.State}) - {pr.HtmlUrl}";
            });

            return ToolCallResult.Text(string.Join("\n", lines));
        }

        private async Task<ToolCallResult> HandleReviewPrAsync(JsonElement arguments, CancellationToken cancellationToken)
        {
            var owner = GetRequiredString(arguments, "owner");
            var repo = GetRequiredString(arguments, "repo");
            var prNumber = GetRequiredInt(arguments, "pr_number");
            var instructions = GetOptionalString(arguments, "instructions");
            var maxFiles = GetOptionalInt(arguments, "max_files") ?? _config.ReviewMaxFiles;
            var maxChars = GetOptionalInt(arguments, "max_chars") ?? _config.ReviewMaxChars;

            var pr = await GitHubGetAsync<GitHubPull>(
                $"/repos/{owner}/{repo}/pulls/{prNumber}",
                null,
                cancellationToken);

            var files = await GetPullFilesAsync(owner, repo, prNumber, maxFiles, cancellationToken);
            var diff = BuildDiff(files, maxChars);
            var summary = BuildSummary(pr, files, maxFiles);
            var prompt = BuildPrompt(pr, files, diff.DiffText, diff.Truncated, instructions);
            var review = await GenerateReviewAsync(prompt, summary, cancellationToken);

            return ToolCallResult.Text(review);
        }

        private async Task<List<GitHubPullFile>> GetPullFilesAsync(
            string owner, string repo, int prNumber, int maxFiles, CancellationToken cancellationToken)
        {
            var files = new List<GitHubPullFile>();
            var page = 1;

            while (files.Count < maxFiles)
            {
                var perPage = Math.Min(100, maxFiles - files.Count);
                var pageFiles = await GitHubGetAsync<List<GitHubPullFile>>(
                    $"/repos/{owner}/{repo}/pulls/{prNumber}/files",
                    new Dictionary<string, string>
                    {
                        ["per_page"] = perPage.ToString(),
                        ["page"] = page.ToString(),
                    },
                    cancellationToken);

                if (pageFiles.Count == 0) break;
                files.AddRange(pageFiles);
                if (pageFiles.Count < perPage) break;
                page += 1;
            }

            return files;
        }

        private DiffResult BuildDiff(List<GitHubPullFile> files, int maxChars)
        {
            var builder = new StringBuilder();
            var truncated = false;

            foreach (var file in files)
            {
                var patch = file.Patch ?? "(No patch available; file may be binary or too large.)";
                var block = $"File: {file.FileName}\nStatus: {file.Status} (+{file.Additions}/-{file.Deletions})\n{patch}";

                if (builder.Length + block.Length + 2 > maxChars)
                {
                    truncated = true;
                    break;
                }

                if (builder.Length > 0) builder.Append("\n\n");
                builder.Append(block);
            }

            return new DiffResult(builder.ToString(), truncated);
        }

        private string BuildSummary(GitHubPull pr, List<GitHubPullFile> files, int maxFiles)
        {
            var filesShown = Math.Min(files.Count, maxFiles);
            var fileLines = files.Take(filesShown)
                .Select(file => $"- {file.FileName} ({file.Status}, +{file.Additions}/-{file.Deletions})")
                .ToArray();

            return string.Join("\n", new[]
            {
                $"PR #{pr.Number}: {pr.Title}",
                $"Author: {pr.User.Login}",
                $"State: {pr.State}",
                $"Base: {pr.Base.Ref} <- Head: {pr.Head.Ref}",
                $"Files: {pr.ChangedFiles}, Additions: {pr.Additions}, Deletions: {pr.Deletions}",
                "",
                "Files touched:",
                fileLines.Length > 0 ? string.Join("\n", fileLines) : "(No files returned by GitHub.)",
            });
        }

        private string BuildPrompt(GitHubPull pr, List<GitHubPullFile> files, string diffText, bool truncated, string? instructions)
        {
            var filesSummary = string.Join("\n", files.Select(file =>
                $"- {file.FileName} ({file.Status}, +{file.Additions}/-{file.Deletions})"));

            var truncationNote = truncated ? "\nNote: The diff was truncated to stay within size limits." : string.Empty;

            var reviewInstructions = string.Join("\n", new[]
            {
                "You are a senior code reviewer.",
                "Review the PR for correctness, security, maintainability, performance, and tests.",
                "Return markdown with sections:",
                "## Summary",
                "## Findings (bulleted, include severity tags: [blocker], [major], [minor], [nit])",
                "## Suggestions",
                "## Tests",
                "Only reference files and changes present in the diff context.",
            });

            var extra = string.IsNullOrWhiteSpace(instructions) ? string.Empty : $"\nAdditional instructions:\n{instructions}\n";

            return string.Join("\n", new[]
            {
                $"{reviewInstructions}{extra}",
                "",
                $"PR: {pr.Title}",
                $"Author: {pr.User.Login}",
                $"URL: {pr.HtmlUrl}",
                $"Base: {pr.Base.Ref} <- Head: {pr.Head.Ref}",
                $"Body:\n{(string.IsNullOrWhiteSpace(pr.Body) ? "(empty)" : pr.Body)}",
                "",
                "Files changed:",
                string.IsNullOrWhiteSpace(filesSummary) ? "(No files returned by GitHub.)" : filesSummary,
                truncationNote,
                "",
                "Diff:",
                "```diff",
                string.IsNullOrWhiteSpace(diffText) ? "(No diff text returned by GitHub.)" : diffText,
                "```",
            });
        }

        private async Task<string> GenerateReviewAsync(string prompt, string fallbackSummary, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_config.AnthropicApiKey))
            {
                return string.Join("\n", new[]
                {
                    "ANTHROPIC_API_KEY is not set. Unable to generate an automated review.",
                    "Set ANTHROPIC_API_KEY to enable LLM reviews.",
                    "",
                    "PR summary:",
                    fallbackSummary,
                });
            }

            var request = new AnthropicMessageRequest
            {
                Model = _config.AnthropicModel,
                MaxTokens = _config.ReviewMaxTokens,
                Temperature = 0.2,
                System = "You are a careful, pragmatic code reviewer.",
                Messages = new List<AnthropicMessage>
                {
                    new AnthropicMessage { Role = "user", Content = prompt },
                },
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(request, JsonOptions), Encoding.UTF8, "application/json");
            var requestUrl = ResolveUrl(_config.AnthropicBaseUrl, "messages");
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Headers.Add("x-api-key", _config.AnthropicApiKey);
            httpRequest.Headers.Add("anthropic-version", _config.AnthropicVersion);
            httpRequest.Content = requestContent;

            using var response = await _anthropicHttp.SendAsync(httpRequest, cancellationToken);
            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Anthropic error {(int)response.StatusCode}: {responseText}");
            }

            var payload = JsonSerializer.Deserialize<AnthropicMessageResponse>(responseText, JsonOptions);
            var content = payload?.Content?
                .Select(part => part.Text)
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToArray();

            if (content == null || content.Length == 0)
            {
                throw new InvalidOperationException("Anthropic response did not include review content.");
            }

            return string.Join("\n", content!);
        }

        private async Task<T> GitHubGetAsync<T>(string path, Dictionary<string, string>? query, CancellationToken cancellationToken)
        {
            var requestUrl = ResolveUrl(_config.GitHubApiBase, path);
            if (query != null && query.Count > 0)
            {
                var builder = new StringBuilder(requestUrl);
                builder.Append(requestUrl.Contains('?') ? '&' : '?');
                builder.Append(string.Join("&", query
                    .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Value))
                    .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}")));
                requestUrl = builder.ToString();
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            if (!string.IsNullOrWhiteSpace(_config.GitHubToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config.GitHubToken);
            }

            using var response = await _githubHttp.SendAsync(request, cancellationToken);
            var body = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"GitHub API error {(int)response.StatusCode}: {body}");
            }

            var data = JsonSerializer.Deserialize<T>(body, JsonOptions);
            if (data == null)
            {
                throw new InvalidOperationException("Failed to parse GitHub API response.");
            }

            return data;
        }

        private static string ResolveUrl(string baseUrl, string path)
        {
            var normalized = baseUrl.EndsWith("/", StringComparison.Ordinal) ? baseUrl : $"{baseUrl}/";
            var trimmed = path.StartsWith("/", StringComparison.Ordinal) ? path[1..] : path;
            return new Uri(new Uri(normalized), trimmed).ToString();
        }

        private static int? GetOptionalInt(JsonElement args, string propertyName)
        {
            if (args.ValueKind != JsonValueKind.Object) return null;
            if (!args.TryGetProperty(propertyName, out var value)) return null;
            if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number)) return number;
            return null;
        }

        private static string? GetOptionalString(JsonElement args, string propertyName)
        {
            if (args.ValueKind != JsonValueKind.Object) return null;
            if (!args.TryGetProperty(propertyName, out var value)) return null;
            return value.ValueKind == JsonValueKind.String ? value.GetString() : null;
        }

        private static string GetRequiredString(JsonElement args, string propertyName)
        {
            var value = GetOptionalString(args, propertyName);
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"Missing required argument: {propertyName}");
            return value;
        }

        private static int GetRequiredInt(JsonElement args, string propertyName)
        {
            var value = GetOptionalInt(args, propertyName);
            if (value is null)
                throw new InvalidOperationException($"Missing required argument: {propertyName}");
            return value.Value;
        }

        private static List<ToolDefinition> BuildToolDefinitions() => new()
        {
            new ToolDefinition
            {
                Name = "list_open_prs",
                Description = "List pull requests for a repository.",
                InputSchema = new Dictionary<string, object?>
                {
                    ["type"] = "object",
                    ["properties"] = new Dictionary<string, object?>
                    {
                        ["owner"] = new Dictionary<string, object?> { ["type"] = "string" },
                        ["repo"] = new Dictionary<string, object?> { ["type"] = "string" },
                        ["state"] = new Dictionary<string, object?>
                        {
                            ["type"] = "string",
                            ["enum"] = new[] { "open", "closed", "all" },
                            ["default"] = "open",
                        },
                        ["limit"] = new Dictionary<string, object?>
                        {
                            ["type"] = "integer",
                            ["minimum"] = 1,
                            ["maximum"] = 100,
                            ["default"] = 20,
                        },
                    },
                    ["required"] = new[] { "owner", "repo" },
                },
            },
            new ToolDefinition
            {
                Name = "review_pr",
                Description = "Fetch a PR diff and generate a review with Claude.",
                InputSchema = new Dictionary<string, object?>
                {
                    ["type"] = "object",
                    ["properties"] = new Dictionary<string, object?>
                    {
                        ["owner"] = new Dictionary<string, object?> { ["type"] = "string" },
                        ["repo"] = new Dictionary<string, object?> { ["type"] = "string" },
                        ["pr_number"] = new Dictionary<string, object?> { ["type"] = "integer" },
                        ["instructions"] = new Dictionary<string, object?> { ["type"] = "string" },
                        ["max_files"] = new Dictionary<string, object?>
                        {
                            ["type"] = "integer",
                            ["minimum"] = 1,
                            ["maximum"] = 300,
                        },
                        ["max_chars"] = new Dictionary<string, object?>
                        {
                            ["type"] = "integer",
                            ["minimum"] = 1000,
                            ["maximum"] = 400000,
                        },
                    },
                    ["required"] = new[] { "owner", "repo", "pr_number" },
                },
            },
        };
    }

    private static async Task<RpcMessage?> ReadMessageAsync(Stream input, CancellationToken cancellationToken)
    {
        var line = await ReadLineAsync(input, cancellationToken);
        if (line == null) return null;
        if (string.IsNullOrWhiteSpace(line)) return await ReadMessageAsync(input, cancellationToken);

        // Check if this is Content-Length header (LSP format) or raw JSON (NDJSON format)
        if (line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase))
        {
            var lengthStr = line.Substring("Content-Length:".Length).Trim();
            if (!int.TryParse(lengthStr, out var contentLength) || contentLength <= 0) return null;
            await ReadLineAsync(input, cancellationToken); // blank line
            var payload = await ReadExactlyAsync(input, contentLength, cancellationToken);
            return new RpcMessage(JsonDocument.Parse(payload));
        }
        else if (line.StartsWith("{"))
        {
            // NDJSON format: raw JSON per line (what Cursor uses)
            return new RpcMessage(JsonDocument.Parse(line));
        }
        else
        {
            return await ReadMessageAsync(input, cancellationToken);
        }
    }

    private static async Task<string?> ReadLineAsync(Stream input, CancellationToken cancellationToken)
    {
        var buffer = new List<byte>(128);
        var single = new byte[1];

        while (true)
        {
            var read = await input.ReadAsync(single.AsMemory(0, 1), cancellationToken);
            if (read == 0) return buffer.Count == 0 ? null : Encoding.UTF8.GetString(buffer.ToArray());
            if (single[0] == '\n') break;
            if (single[0] != '\r') buffer.Add(single[0]);
        }

        return Encoding.UTF8.GetString(buffer.ToArray());
    }

    private static async Task<byte[]> ReadExactlyAsync(Stream input, int length, CancellationToken cancellationToken)
    {
        var buffer = new byte[length];
        var offset = 0;
        while (offset < length)
        {
            var read = await input.ReadAsync(buffer.AsMemory(offset, length - offset), cancellationToken);
            if (read == 0) throw new EndOfStreamException("Unexpected end of stream.");
            offset += read;
        }
        return buffer;
    }

    private static async Task WriteResultAsync<T>(Stream output, JsonElement id, T result, CancellationToken cancellationToken)
    {
        await WriteMessageAsync(output, new RpcResponse<T> { Jsonrpc = "2.0", Id = id, Result = result }, cancellationToken);
    }

    private static async Task WriteErrorAsync(Stream output, JsonElement id, int code, string message, CancellationToken cancellationToken)
    {
        await WriteMessageAsync(output, new RpcErrorResponse { Jsonrpc = "2.0", Id = id, Error = new RpcError { Code = code, Message = message } }, cancellationToken);
    }

    private static async Task WriteMessageAsync<T>(Stream output, T payload, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(payload, JsonOptions);
        await output.WriteAsync(json, cancellationToken);
        await output.WriteAsync("\n"u8.ToArray(), cancellationToken);
        await output.FlushAsync(cancellationToken);
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private readonly record struct RpcMessage(JsonDocument Document) : IDisposable
    {
        public JsonElement Root => Document.RootElement;
        public bool HasId => Root.TryGetProperty("id", out _);
        public JsonElement Id => Root.TryGetProperty("id", out var id) ? id : default;
        public void Dispose() => Document.Dispose();
    }

    private sealed class AppConfig
    {
        public string GitHubToken { get; init; } = string.Empty;
        public string GitHubApiBase { get; init; } = "https://api.github.com";
        public string AnthropicApiKey { get; init; } = string.Empty;
        public string AnthropicBaseUrl { get; init; } = "https://api.anthropic.com/v1";
        public string AnthropicVersion { get; init; } = "2023-06-01";
        public string AnthropicModel { get; init; } = "claude-sonnet-4-20250514";
        public int ReviewMaxChars { get; init; } = 120000;
        public int ReviewMaxFiles { get; init; } = 100;
        public int ReviewMaxTokens { get; init; } = 4096;

        public static AppConfig Load()
        {
            var localSettings = LoadLocalSettings();
            return new AppConfig
            {
                GitHubToken = localSettings?.GitHub?.Token ?? GetEnv("GITHUB_TOKEN"),
                GitHubApiBase = GetEnv("GITHUB_API_BASE", "https://api.github.com"),
                AnthropicApiKey = localSettings?.Anthropic?.ApiKey ?? GetEnv("ANTHROPIC_API_KEY"),
                AnthropicBaseUrl = localSettings?.Anthropic?.BaseUrl ?? GetEnv("ANTHROPIC_BASE_URL", "https://api.anthropic.com/v1"),
                AnthropicVersion = localSettings?.Anthropic?.Version ?? GetEnv("ANTHROPIC_VERSION", "2023-06-01"),
                AnthropicModel = localSettings?.Anthropic?.Model ?? GetEnv("ANTHROPIC_MODEL", "claude-sonnet-4-20250514"),
                ReviewMaxChars = localSettings?.Review?.MaxChars ?? GetEnvInt("REVIEW_MAX_CHARS", 120000),
                ReviewMaxFiles = localSettings?.Review?.MaxFiles ?? GetEnvInt("REVIEW_MAX_FILES", 100),
                ReviewMaxTokens = localSettings?.Review?.MaxTokens ?? GetEnvInt("REVIEW_MAX_TOKENS", 4096),
            };
        }

        private static LocalSettings? LoadLocalSettings()
        {
            var paths = new[]
            {
                Path.Combine(AppContext.BaseDirectory, "appsettings.local.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "appsettings.local.json"),
            };

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    try
                    {
                        var json = File.ReadAllText(path);
                        return JsonSerializer.Deserialize<LocalSettings>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    catch { }
                }
            }
            return null;
        }

        private static string GetEnv(string key, string defaultValue = "") =>
            Environment.GetEnvironmentVariable(key) is { Length: > 0 } value ? value : defaultValue;

        private static int GetEnvInt(string key, int defaultValue) =>
            int.TryParse(Environment.GetEnvironmentVariable(key), out var result) ? result : defaultValue;
    }

    private sealed class LocalSettings
    {
        public GitHubSettings? GitHub { get; set; }
        public AnthropicSettings? Anthropic { get; set; }
        public ReviewSettings? Review { get; set; }
    }

    private sealed class GitHubSettings { public string? Token { get; set; } }
    private sealed class AnthropicSettings { public string? ApiKey { get; set; } public string? Model { get; set; } public string? Version { get; set; } public string? BaseUrl { get; set; } }
    private sealed class ReviewSettings { public int? MaxChars { get; set; } public int? MaxFiles { get; set; } public int? MaxTokens { get; set; } }

    private sealed class ToolDefinition { public string Name { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public Dictionary<string, object?> InputSchema { get; set; } = new(); }
    private sealed class ToolsListResult { public List<ToolDefinition> Tools { get; set; } = new(); }
    private sealed class InitializeResult { public string ProtocolVersion { get; set; } = DefaultProtocolVersion; public ServerCapabilities Capabilities { get; set; } = new(); public ServerInfo ServerInfo { get; set; } = new(); }
    private sealed class ServerCapabilities { public Dictionary<string, object> Tools { get; set; } = new(); }
    private sealed class ServerInfo { public string Name { get; set; } = string.Empty; public string Version { get; set; } = string.Empty; }

    private sealed class ToolCallResult
    {
        public List<ContentItem> Content { get; set; } = new();
        public bool? IsError { get; set; }
        public static ToolCallResult Text(string text) => new() { Content = new List<ContentItem> { new() { Type = "text", Text = text } } };
        public static ToolCallResult Error(string message) => new() { IsError = true, Content = new List<ContentItem> { new() { Type = "text", Text = message } } };
    }

    private sealed class ContentItem { public string Type { get; set; } = "text"; public string Text { get; set; } = string.Empty; }
    private sealed class RpcResponse<T> { public string Jsonrpc { get; set; } = "2.0"; public JsonElement Id { get; set; } public T? Result { get; set; } }
    private sealed class RpcErrorResponse { public string Jsonrpc { get; set; } = "2.0"; public JsonElement Id { get; set; } public RpcError Error { get; set; } = new(); }
    private sealed class RpcError { public int Code { get; set; } public string Message { get; set; } = string.Empty; }
    private sealed record DiffResult(string DiffText, bool Truncated);

    private sealed class GitHubUser { [JsonPropertyName("login")] public string Login { get; set; } = string.Empty; }
    private sealed class GitHubRef { [JsonPropertyName("ref")] public string Ref { get; set; } = string.Empty; }

    private sealed class GitHubPull
    {
        [JsonPropertyName("number")] public int Number { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("body")] public string? Body { get; set; }
        [JsonPropertyName("state")] public string State { get; set; } = string.Empty;
        [JsonPropertyName("html_url")] public string HtmlUrl { get; set; } = string.Empty;
        [JsonPropertyName("user")] public GitHubUser User { get; set; } = new();
        [JsonPropertyName("base")] public GitHubRef Base { get; set; } = new();
        [JsonPropertyName("head")] public GitHubRef Head { get; set; } = new();
        [JsonPropertyName("additions")] public int Additions { get; set; }
        [JsonPropertyName("deletions")] public int Deletions { get; set; }
        [JsonPropertyName("changed_files")] public int ChangedFiles { get; set; }
    }

    private sealed class GitHubPullFile
    {
        [JsonPropertyName("filename")] public string FileName { get; set; } = string.Empty;
        [JsonPropertyName("status")] public string Status { get; set; } = string.Empty;
        [JsonPropertyName("additions")] public int Additions { get; set; }
        [JsonPropertyName("deletions")] public int Deletions { get; set; }
        [JsonPropertyName("changes")] public int Changes { get; set; }
        [JsonPropertyName("patch")] public string? Patch { get; set; }
    }

    private sealed class AnthropicMessageRequest
    {
        [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;
        [JsonPropertyName("max_tokens")] public int MaxTokens { get; set; }
        [JsonPropertyName("temperature")] public double Temperature { get; set; }
        [JsonPropertyName("system")] public string System { get; set; } = string.Empty;
        [JsonPropertyName("messages")] public List<AnthropicMessage> Messages { get; set; } = new();
    }

    private sealed class AnthropicMessage { [JsonPropertyName("role")] public string Role { get; set; } = string.Empty; [JsonPropertyName("content")] public string Content { get; set; } = string.Empty; }
    private sealed class AnthropicMessageResponse { [JsonPropertyName("content")] public List<AnthropicContent>? Content { get; set; } }
    private sealed class AnthropicContent { [JsonPropertyName("type")] public string Type { get; set; } = string.Empty; [JsonPropertyName("text")] public string? Text { get; set; } }
}
