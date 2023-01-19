namespace EpoqZeroTaDA.Views;

public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
		InitializeComponent();
	}

    private void LetUsGoButton_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(HomePage));
    }
}