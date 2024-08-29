using Microsoft.Extensions.Logging;
using MovieCollectionSystem.MVVM.models;
using MovieCollectionSystem.Repositories;

namespace MovieCollectionSystem
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

            builder.Services.AddSingleton<BaseRepository<User>>();
            builder.Services.AddSingleton<BaseRepository<Movie>>();
            builder.Services.AddSingleton<BaseRepository<Genre>>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
