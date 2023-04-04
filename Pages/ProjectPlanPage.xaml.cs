using System.Formats.Tar;
using Plandit.Models;

namespace Plandit.Pages;

public partial class ProjectPlanPage : ContentPage
{
    private int projectId;

    public ProjectPlanPage(ProjectModel project)
	{
		InitializeComponent();

        projectId = project.Id;
        projectTitle.Text = project.ProjectTitle;
        projectDescription.Text = project.ProjectDescription;
        projectDeadline.Text = "Deadline: " + project.DateSpan.ToShortDateString();

        ShowTasks();
    }

    /// <summary>
    /// Called to update the view context
    /// </summary>
    private async void ShowTasks()
    {
        List<TodoTask> taskList = await App.ProjectRepository.GetTasks(projectId);
        todoListView.ItemsSource = taskList;
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
            await App.ProjectRepository.AddTask(taskEntry.Text, projectId);
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
            await App.ProjectRepository.DeleteTask(taskID);

            ShowTasks();
        }
    }
}