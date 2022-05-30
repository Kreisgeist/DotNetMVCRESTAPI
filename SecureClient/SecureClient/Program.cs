using SecureClient;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

Console.WriteLine("Making the call, fingers crossed...");
RunAsync().GetAwaiter().GetResult();

static async Task RunAsync()
{
    AuthConfig config = AuthConfig.ReadJsonFromFile("appsettings.json");

    IConfidentialClientApplication app;

    app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
        .WithClientSecret(config.ClientSecret)
        .WithAuthority(new Uri(config.Authority))
        .Build();

    string[] ResourceIds = new string[] {config.ResourceId};

    AuthenticationResult result = null;

    try
    {
        result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Token Aquired \n");
        Console.WriteLine(result.AccessToken);
        Console.ResetColor();
    }
    catch (MsalClientException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }
}