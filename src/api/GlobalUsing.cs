global using System;

global using OpenTelemetry.Metrics;
global using OpenTelemetry.Trace;
global using OpenTelemetry.Logs;
global using OpenTelemetry.Resources;
global using System.Diagnostics.Metrics;
global using System.Diagnostics;

global using Azure.Identity;

global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Server.Kestrel.Core;

global using Azure.Monitor.OpenTelemetry.AspNetCore;

global using Microsoft.SemanticKernel;
global using Microsoft.SemanticKernel.ChatCompletion;
global using Microsoft.SemanticKernel.Connectors.OpenAI;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

global using ric.analyzer.api;