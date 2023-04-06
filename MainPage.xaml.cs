using Plandit.Models;
using Plandit.Pages;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;

namespace Plandit;

public partial class MainPage : ContentPage
{
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
        stackLayout.Padding = 5;
        stackLayout.VerticalOptions = LayoutOptions.Center;

        // Create two Entry fields for title and description and add the Entry fields to the StackLayout
        Entry titleEntry = new Entry { Placeholder = "Title" };
        titleEntry.TextColor = Application.Current.UserAppTheme == AppTheme.Dark ? GetColour("ExowebLightText") : GetColour("ExowebDarkText");
        stackLayout.Children.Add(titleEntry);

        Entry descriptionEntry = new Entry { Placeholder = "Description" };
        descriptionEntry.TextColor = Application.Current.UserAppTheme == AppTheme.Dark ? GetColour("ExowebLightText") : GetColour("ExowebDarkText");
        stackLayout.Children.Add(descriptionEntry);

        // Create a time picker so a deadline can be assigned
        DatePicker datePicker = new DatePicker();
        datePicker.HorizontalOptions = LayoutOptions.Center;
        datePicker.TextColor = Application.Current.UserAppTheme == AppTheme.Dark ? GetColour("ExowebLightText") : GetColour("ExowebDarkText");
        stackLayout.Children.Add(datePicker);

        // Create a Button for submitting the input and give it functionality. Then add it to the StackLayout
        Button submitButton = new Button { Text = "Submit" };
        submitButton.Clicked += (s, args) =>
        {
            ProjectModel project = new ProjectModel
            {
                ProjectTitle = titleEntry.Text,
                ProjectDescription = descriptionEntry.Text,
                DateSpan = datePicker.Date
            };

            AddProjectOnClick(project);
            popup.Close();
        };

        stackLayout.Children.Add(submitButton);

        // Assign the content inside the popup
        popup.Content = new Border
        {
            BackgroundColor = Application.Current.UserAppTheme == AppTheme.Dark ? GetColour("ExowebLightBackground") : GetColour("ExowebDarkBackground"),
            Padding = new Thickness(5),
            WidthRequest = 250,
            HeightRequest = 300,
            Content = stackLayout
        };
        popup.Color = Color.FromArgb("#00000000");

        Debug.WriteLine(DeviceDisplay.MainDisplayInfo.Width);

        // Show the popup
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

    private async void AddProjectOnClick(ProjectModel project)
    {
        Debug.WriteLine("AddProjectOnClick");
        if(!string.IsNullOrEmpty(project.ProjectTitle))
        {
            // Add task to list
            await App.ProjectRepository.AddProject(project);
            projectTitleEntry.Text = string.Empty;

            ShowProjects();

            Debug.WriteLine("Added project");
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

