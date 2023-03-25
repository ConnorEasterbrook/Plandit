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

	private void ChangeLightMode(object sender, EventArgs e)
	{
        /*Application.Current.UserAppTheme = count % 2 == 0 ? AppTheme.Light : AppTheme.Dark;*/
    }

	private void AddTaskOnClick(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(taskEntry.Text))
		{
            tasks.Add(new Task { TaskItem = taskEntry.Text });
            taskEntry.Text = string.Empty;
        }
	}

	private void DeleteTaskOnClick(object sender, EventArgs e)
	{
        var task = (sender as Button).BindingContext as Task;
        if(task != null)
        {
            tasks.Remove(task);
        }
    }
}

public class Task
{
	public string TaskItem { get; set; }
}

