using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plandit.Repositories;

namespace Plandit;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
        builder.Logging.AddDebug();
#endif

		string databaseName2 = "projects";
		string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		string databasePath2 = Path.Combine(folderPath2, databaseName2);
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ProjectRepository>(s, databasePath2));

		return builder.Build();
	}
}
