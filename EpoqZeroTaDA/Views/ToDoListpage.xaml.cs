using CommunityToolkit.Mvvm.Input;
using EpoqZeroTaDA.Models;
using EpoqZeroTaDA.ViewModels;

namespace EpoqZeroTaDA.Views;

public partial class ToDoListpage : ContentPage
{
    public ToDoListpage(ToDoListPageViewModel tdlpvm)
	{
		InitializeComponent();
        BindingContext = tdlpvm;
    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        ToDoListPageViewModel viewModel = (ToDoListPageViewModel)BindingContext;
        viewModel.MyCurrentState = "Loading";
        viewModel.ToDoItems.Clear();
        viewModel.DisplayTasksViaApi("Open");
        base.OnNavigatedTo(args);
        
    }
}