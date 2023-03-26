using System.Collections.ObjectModel;

namespace Plandit;

public partial class MainPage : ContentPage
{
	// Create a list of tasks
	private ObservableCollection<TaskClass> _tasks = new ObservableCollection<TaskClass>();
    public ObservableCollection<TaskClass> tasks
	{
		get { return _tasks; }
	}

    public MainPage()
	{
		InitializeComponent(); // Initialize the XAML
		BindingContext = this; // Bind the XAML to the C# code
	}

	/// <summary>
	/// Change the light mode of the application
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ChangeLightMode(object sender, EventArgs e)
	{
        /*Application.Current.UserAppTheme = count % 2 == 0 ? AppTheme.Light : AppTheme.Dark;*/
    }

	/// <summary>
	/// Handle task addition
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void AddTaskOnClick(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(taskEntry.Text))
		{
            // Add task to list
            tasks.Add(new TaskClass { TaskTitle = taskEntry.Text });
            taskEntry.Text = string.Empty;
        }
	}

	/// <summary>
	/// Handle task deletion
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void DeleteTaskOnClick(object sender, EventArgs e)
	{
        var task = (sender as Button).BindingContext as TaskClass;
        if(task != null)
        {
			// Remove task from list
            tasks.Remove(task);
        }
    }
}

/// <summary>
/// Class that stores Task Information
/// </summary>
public class TaskClass
{
	/*public int Id { get; set; }*/
	public string TaskTitle { get; set; }
	/*public bool IsCompleted { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime CompletedAt { get; set; }
	public DateTime DeadlineAt { get; set; }*/
}

