using zelos_hub.Hubs;

var builder = WebApplication.CreateBuilder(args);

string[] origins = new[] {"http://localhost"};
var domain = Environment.GetEnvironmentVariable("ORIGIN_DOMAIN");
if (domain != null)
    origins = origins.Append(domain).ToArray();

Console.WriteLine("Allowing origins: " + string.Join(", ", origins));

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
            policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors();
app.MapGet("/", () => "HUB is running");

app.MapHub<PrinterHub>("/printer-status");
app.Run();