using Microsoft.Extensions.Logging;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Maui.DotObfuscatorApp;

public static class HttpClientTypes
{
    public readonly static string Regular = "Regular";
}

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var certificateHandler = new HttpClientHandler();
        certificateHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
        certificateHandler.ServerCertificateCustomValidationCallback = ValidateCertificate;

        builder.Services.AddHttpClient();
        builder.Services.AddHttpClient(HttpClientTypes.Regular, (client) =>
        {
            client.BaseAddress = new Uri("https://efs.btgpactual.com");
        }).ConfigurePrimaryHttpMessageHandler(() => certificateHandler);

        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
    }

    private static bool ValidateCertificate(HttpRequestMessage message, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors errors)
    {
        var certificateHash = certificate?.GetCertHashString();

        return string.IsNullOrEmpty(certificateHash);
    }
}
