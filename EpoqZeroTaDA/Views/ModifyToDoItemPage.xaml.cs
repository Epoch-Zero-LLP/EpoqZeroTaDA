using EpoqZeroTaDA.ViewModels;

namespace EpoqZeroTaDA.Views;

public partial class ModifyToDoItemPage : ContentPage
{
	public ModifyToDoItemPage(ToDoListPageViewModel tdlpvm)
	{
		InitializeComponent();
		BindingContext = tdlpvm;
	}
}