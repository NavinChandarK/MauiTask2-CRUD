using CRUDMauiAppTask2.Services;
using CRUDMauiAppTask2.ViewModels;
using CRUDMauiAppTask2.Views;
using Microsoft.Extensions.Logging;

namespace CRUDMauiAppTask2
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "employees.db3");

            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<EmployeeService>(s, dbPath));


            builder.Services.AddSingleton<EmployeesViewModel>();
            builder.Services.AddSingleton<EmployeeDetailsViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<EmployeeDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
