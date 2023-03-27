using Microsoft.Extensions.Logging;

namespace Plandit;

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

		// Get local path for tasks.db3
		string databaseName = "tasks";
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		string databasePath = Path.Combine(folderPath, databaseName);
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<TaskRepository>(s, databasePath));

		return builder.Build();
	}
}
