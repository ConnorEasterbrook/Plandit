using Plandit.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace Plandit;

public partial class MainPage : ContentPage
{
	// Create a list of tasks
	private ObservableCollection<TodoTask> _tasks = new ObservableCollection<TodoTask>();
    public ObservableCollection<TodoTask> Tasks
	{
		get { return _tasks; }
	}

    public MainPage()
	{
		InitializeComponent(); // Initialize the XAML
		BindingContext = this; // Bind the XAML to the C# code

		todoListView.ItemsSource = App.TaskRepository.GetTasks();
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
			App.TaskRepository.AddTask(taskEntry.Text);
            taskEntry.Text = string.Empty;

            todoListView.ItemsSource = App.TaskRepository.GetTasks();
        }
	}

	/// <summary>
	/// Handle task deletion
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void DeleteTaskOnClick(object sender, EventArgs e)
	{
        var task = (sender as Button).BindingContext as TodoTask;
        if(task != null)
        {
			// Remove task from list
			int taskID = task.Id;
			App.TaskRepository.DeleteTask(taskID);

            todoListView.ItemsSource = App.TaskRepository.GetTasks();
        }
    }
}

