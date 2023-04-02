using Plandit.Models;
using Plandit.Pages;
using SQLite;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using System.Runtime.CompilerServices;

namespace Plandit;

public partial class MainPage : ContentPage
{
    private ObservableCollection<ProjectPlanPage> _projectPlans = new ObservableCollection<ProjectPlanPage>();
    public ObservableCollection<ProjectPlanPage> ProjectPlans
    {
        get { return _projectPlans; }
    }

    private string _projectName;
    public string ProjectName
    {
        get { return _projectName; }
        set { _projectName = value; OnPropertyChanged(); }
    }

    private string _projectDescription;
    public string ProjectDescription
    {
        get { return _projectDescription; }
        set { _projectDescription = value; OnPropertyChanged(); }
    }

    public MainPage()
	{
		InitializeComponent(); // Initialize the XAML

        ShowProjects();
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
    /// Show all projects. Called whenever a change occurs
    /// </summary>
    private async void ShowProjects()
    {
        List<ProjectModel> projectList = await App.ProjectRepository.GetProjects();
        todoListView.ItemsSource = projectList;
    }

    private async void GoToProjectPage(object sender, EventArgs e)
    {
        var selectedProject = (sender as Grid).BindingContext as ProjectModel;
        ProjectPlanPage planPage = new ProjectPlanPage(selectedProject);
        await Navigation.PushAsync(planPage);
    }

    private async void AddProjectButtonClicked(object sender, EventArgs e)
    {
        // Create the popup object
        Popup popup = new Popup();

        // Create a StackLayout to hold the input fields
        StackLayout stackLayout = new StackLayout();

        // Create two Entry fields for title and description and add the Entry fields to the StackLayout
        Entry titleEntry = new Entry { Placeholder = "Title" };
        stackLayout.Children.Add(titleEntry);

        Entry descriptionEntry = new Entry { Placeholder = "Description" };
        stackLayout.Children.Add(descriptionEntry);

        // Create a Button for submitting the input
        Button submitButton = new Button { Text = "Submit" };

        submitButton.Clicked += async (s, args) =>
        {
            ProjectName = titleEntry.Text;
            ProjectDescription = descriptionEntry.Text;

            AddProjectOnClick();
            popup.Close();
        };

        popup.Content = new Border
        {
            BackgroundColor = Application.Current.UserAppTheme == AppTheme.Dark ? GetColour("ExowebLightBackground") : GetColour("ExowebDarkBackground"),
            Padding = new Thickness(20),
            Content = stackLayout
        };

        await this.ShowPopupAsync(popup);
    }

    private Color GetColour(string colourName)
    {
        Color returnColour = Color.FromArgb("ffffff");
        if (Application.Current.Resources.TryGetValue(colourName, out var colour))
        {
            returnColour = (Color)colour;
        }

        return returnColour;
    }

    private async void AddProjectOnClick()
    {
        if(!string.IsNullOrEmpty(_projectName))
        {
            // Add task to list
            await App.ProjectRepository.AddProject(_projectName);
            _projectName = string.Empty;

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

