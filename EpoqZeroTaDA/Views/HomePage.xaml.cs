using EpoqZeroTaDA.ViewModels;

namespace EpoqZeroTaDA.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel();
    }
}