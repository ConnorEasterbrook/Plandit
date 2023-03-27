namespace Plandit;

public partial class App : Application
{
	public static TaskRepository TaskRepository { get; private set; }

	public App(TaskRepository repo)
	{
		InitializeComponent();

		MainPage = new AppShell();

		TaskRepository = repo;
	}
}
