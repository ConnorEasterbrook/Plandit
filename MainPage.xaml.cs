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

    private async void GoToTodoListPage(object sender, EventArgs e)
    {
        ProjectPlanPage planPage = new ProjectPlanPage();
        await Navigation.PushAsync(planPage);
    }
}

