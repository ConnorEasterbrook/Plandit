using System.Collections.ObjectModel;

namespace Plandit;

public partial class MainPage : ContentPage
{
	private ObservableCollection<Task> _tasks = new ObservableCollection<Task>();
    public ObservableCollection<Task> tasks
	{
		get { return _tasks; }
	}

    public MainPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private void AddTaskOnClick(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(taskEntry.Text))
		{
            tasks.Add(new Task { TaskItem = taskEntry.Text });
            taskEntry.Text = string.Empty;
        }
	}
}

public class Task
{
	public string TaskItem { get; set; }
}

