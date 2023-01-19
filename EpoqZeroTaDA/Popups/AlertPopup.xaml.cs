using EpoqZeroTaDA.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace EpoqZeroTaDA.Popups;

public partial class AlertPopup : PopupPage
{
	public AlertPopup(string title, string description, string okText)
	{
		InitializeComponent();
		HeadingLabel.Text = title;
		DescriptionLabel.Text = description;
		OkButton.Text = okText;
		BackgroundColor = Color.FromArgb("#80000000");
	}

    private async void OkButton_Clicked(object sender, EventArgs e)
    {
		await MopupService.Instance.PopAsync();
    }
}