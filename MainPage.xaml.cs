namespace Plandit;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		Application.Current.UserAppTheme = count % 2 == 0 ? AppTheme.Light : AppTheme.Dark;

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

