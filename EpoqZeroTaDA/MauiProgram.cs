using CommunityToolkit.Maui;
using EpoqZeroTaDA.ViewModels;
using EpoqZeroTaDA.Views;
using Mopups.Hosting;

namespace EpoqZeroTaDA;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureMopups()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<HomePage>();
		builder.Services.AddSingleton<AddToDoItemPage>();
		builder.Services.AddSingleton<ModifyToDoItemPage>();
		builder.Services.AddSingleton<ToDoListpage>();
		builder.Services.AddSingleton<ToDoListPageViewModel>();

		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(ToDoListpage), typeof(ToDoListpage));
		Routing.RegisterRoute(nameof(AddToDoItemPage), typeof(AddToDoItemPage));
		Routing.RegisterRoute(nameof(ModifyToDoItemPage), typeof(ModifyToDoItemPage));

		return builder.Build();
	}
}
