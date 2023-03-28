using Plandit.Models;
using Plandit.Pages;
using SQLite;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Plandit;

public partial class MainPage : ContentPage
{
    private ObservableCollection<ProjectPlanPage> _projectPlans = new ObservableCollection<ProjectPlanPage>();
    public ObservableCollection<ProjectPlanPage> ProjectPlans
    {
        get { return _projectPlans; }
    }

    public MainPage()
	{
		InitializeComponent(); // Initialize the XAML
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

    private async void ShowProjects()
    {
        List<ProjectModel> projectList = await App.ProjectRepository.GetProjects();
        todoListView.ItemsSource = projectList;
    }

    private async void GoToProjectPage(object sender, EventArgs e)
    {
        var selectedProject = (sender as Button).BindingContext as ProjectModel;
        ProjectPlanPage planPage = new ProjectPlanPage(selectedProject);
        await Navigation.PushAsync(planPage);
    }

    private async void AddProjectOnClick(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(taskEntry.Text))
        {
            // Add task to list
            await App.ProjectRepository.AddProject(taskEntry.Text);
            taskEntry.Text = string.Empty;

            ShowProjects();
        }
    }

    private async void DeleteProjectOnClick(object sender, EventArgs e)
    {
        var task = (sender as Button).BindingContext as ProjectModel;
        if(task != null)
        {
            // Remove task from list
            int taskID = task.Id;
            await App.ProjectRepository.DeleteProject(taskID);

            ShowProjects();
        }
    }
}

