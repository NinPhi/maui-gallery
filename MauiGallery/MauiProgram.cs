using MauiGallery.Services;
using MauiGallery.ViewModel;

namespace MauiGallery;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services
			.AddSingleton<MainPage>()
			.AddSingleton<SharedViewModel>()
			.AddTransient<ImageManager>();

		return builder.Build();
	}
}
