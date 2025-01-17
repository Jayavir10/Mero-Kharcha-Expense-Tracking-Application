using Microsoft.Extensions.Logging;
using Mero_Kharcha.Services;
using MudBlazor.Services;

namespace Mero_Kharcha
{
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

            //Registeing user services
            builder.Services.AddSingleton<IUserServices, UserServices>();
            builder.Services.AddSingleton<ITransactionServices, TransactionServices>();
            builder.Services.AddSingleton<IDebtServices, DebtServices>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
