using QuickBooksSharp.Authentication;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;
using Serilog;
using Serilog.Extensions.Logging;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var loggerFactory = new SerilogLoggerFactory(Log.Logger);
var logger = loggerFactory.CreateLogger("QuickBooksSharp");

var clientId = Environment.GetEnvironmentVariable("QUICKBOOKS_SHARP_CLIENT_ID")!;
var clientSecret = Environment.GetEnvironmentVariable("QUICKBOOKS_SHARP_CLIENT_SECRET")!;
var refreshToken = Environment.GetEnvironmentVariable("QUICKBOOKS_SHARP_REFRESH_TOKEN")!;
var realmId = long.Parse(Environment.GetEnvironmentVariable("QUICKBOOKS_SHARP_REALMID")!);
var useSandbox = true;

Console.WriteLine("Refreshing access token...");
var authService = new AuthenticationService();
var tokenResponse = await authService.RefreshOAuthTokenAsync(clientId, clientSecret, refreshToken);
var accessToken = tokenResponse.access_token;
Console.WriteLine($"Access token obtained (expires in {tokenResponse.expires_in}s)");

Console.WriteLine("\n=== Projects API ===");
try
{
    var projectService = new ProjectService(accessToken, realmId, useSandbox, logger: logger);
    var projects = await projectService.GetProjectsAsync(first: 5);

    if (projects.HasErrors)
    {
        Console.WriteLine($"Error: {string.Join(", ", projects.Errors!.Select(e => e.Message))}");
    }
    else
    {
        var edges = projects.Data?.Projects?.Edges;
        Console.WriteLine($"Found {edges?.Length ?? 0} project(s)");
        if (edges != null)
        {
            foreach (var edge in edges)
            {
                var p = edge.Node!;
                Console.WriteLine($"  - {p.Name} (Status: {p.Status}, Id: {p.Id})");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Projects API error: {ex.Message}");
}

Console.WriteLine("\n=== Custom Fields API ===");
try
{
    var cfService = new CustomFieldService(accessToken, realmId, useSandbox, logger: logger);
    var fields = await cfService.GetCustomFieldDefinitionsAsync(first: 10);

    if (fields.HasErrors)
    {
        Console.WriteLine($"Error: {string.Join(", ", fields.Errors!.Select(e => e.Message))}");
    }
    else
    {
        var edges = fields.Data?.CustomFieldDefinitions?.Edges;
        Console.WriteLine($"Found {edges?.Length ?? 0} custom field definition(s)");
        if (edges != null)
        {
            foreach (var edge in edges)
            {
                var cf = edge.Node!;
                Console.WriteLine($"  - {cf.Label} (Type: {cf.DataType}, Active: {cf.Active}, Id: {cf.Id})");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Custom Fields API error: {ex.Message}");
}

Console.WriteLine("\n=== Sales Tax API ===");
try
{
    var taxService = new SalesTaxService(accessToken, realmId, useSandbox, logger: logger);
    var taxResult = await taxService.CalculateSalesTaxAsync(new SalesTaxCalculationInput
    {
        TransactionDate = DateTime.Today.ToString("yyyy-MM-dd"),
        Subject = new SalesTaxSubjectInput { QbCustomerId = "1" },
        LineItems =
        [
            new SalesTaxLineItemInput
            {
                NumberOfUnits = 1,
                PricePerUnitExcludingTaxes = new SalesTaxMoneyInput { Value = 100.00m }
            }
        ]
    });

    if (taxResult.HasErrors)
    {
        Console.WriteLine($"Error: {string.Join(", ", taxResult.Errors!.Select(e => e.Message))}");
    }
    else
    {
        var calc = taxResult.Data?.Result?.TaxCalculation;
        Console.WriteLine($"Tax calculated: {calc?.TaxTotals?.TotalTaxAmountExcludingShipping?.Value}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Sales Tax API error: {ex.Message}");
}

Console.WriteLine("\n=== Dimensions API ===");
try
{
    var dimService = new DimensionService(accessToken, realmId, useSandbox, logger: logger);
    var dims = await dimService.GetDimensionDefinitionsAsync(first: 10);

    if (dims.HasErrors)
    {
        Console.WriteLine($"Error: {string.Join(", ", dims.Errors!.Select(e => e.Message))}");
    }
    else
    {
        var edges = dims.Data?.DimensionDefinitions?.Edges;
        Console.WriteLine($"Found {edges?.Length ?? 0} dimension definition(s)");
        if (edges != null)
        {
            foreach (var edge in edges)
            {
                var d = edge.Node!;
                Console.WriteLine($"  - {d.Label} (Type: {d.DataType}, Active: {d.Active})");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Dimensions API error: {ex.Message}");
}

Console.WriteLine("\nDone.");
