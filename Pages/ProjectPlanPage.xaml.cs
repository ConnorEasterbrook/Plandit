using System.Formats.Tar;
using Plandit.Models;

namespace Plandit.Pages;

public partial class ProjectPlanPage : ContentPage
{
	public ProjectPlanPage()
	{
		InitializeComponent();

        ShowTasks();
    }

    private async void ShowTasks()
    {
        List<TodoTask> taskList = await App.TaskRepository.GetTasks();
        todoListView.ItemsSource = taskList;
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
    private async void AddTaskOnClick(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(taskEntry.Text))
        {
            // Add task to list
            await App.TaskRepository.AddTask(taskEntry.Text);
            taskEntry.Text = string.Empty;

            ShowTasks();
        }
    }

    /// <summary>
    /// Handle task deletion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void DeleteTaskOnClick(object sender, EventArgs e)
    {
        var task = (sender as Button).BindingContext as TodoTask;
        if(task != null)
        {
            // Remove task from list
            int taskID = task.Id;
            await App.TaskRepository.DeleteTask(taskID);

            ShowTasks();
        }
    }
}