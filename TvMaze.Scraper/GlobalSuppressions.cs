// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "This case is not related to any security relevent functionallity.", Scope = "member", Target = "~M:TvMaze.Scraper.Helpers.Polly.PollyPolicies.GetRetryPolicy(System.Int32,System.Int32)~Polly.IAsyncPolicy{System.Net.Http.HttpResponseMessage}")]
[assembly: SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", Justification = "We will ignore this alert for now. However, to optimize the logging performance, this alert should be solved.", Scope = "module")]
