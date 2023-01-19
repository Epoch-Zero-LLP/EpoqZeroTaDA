using EpoqZeroTaDA.Models;
using EpoqZeroTaDA.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EpoqZeroTaDA.Views;

public partial class AddToDoItemPage : ContentPage
{
    public AddToDoItemPage(ToDoListPageViewModel tdlvm)
	{
		InitializeComponent();
		BindingContext = tdlvm;
	}
    
}