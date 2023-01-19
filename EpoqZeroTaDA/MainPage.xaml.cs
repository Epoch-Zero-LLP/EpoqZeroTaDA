using EpoqZeroTaDA.ViewModels;
using EpoqZeroTaDA.Views;
using Microsoft.Maui.Dispatching;
namespace EpoqZeroTaDA;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
        //ToDoAppLabel.Opacity = 0;
        //EpoqZeroLabel.Opacity = 0;
    }

    private async void GoButton_Clicked(object sender, EventArgs e)
    {
        //await ToDoAppLabel.FadeTo(1, 1000);
        //await EpoqZeroLabel.FadeTo(1, 100);
        Shell.Current.GoToAsync(nameof(HomePage));
    }
}

