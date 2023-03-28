using Plandit.Repositories;

namespace Plandit;

public partial class App : Application
{
	public static ProjectRepository ProjectRepository { get; private set; }

	public App(ProjectRepository projectRepository)
	{
		InitializeComponent();

		MainPage = new AppShell();

		ProjectRepository = projectRepository;
	}
}
