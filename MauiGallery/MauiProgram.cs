using MauiGallery.ViewModel;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif

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

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(windowsLifecycleBuilder =>
            {
                windowsLifecycleBuilder.OnWindowCreated(window =>
                {
                    var mauiWindow = (Window)window.GetWindow();
                    if (mauiWindow.Page is not PresenterPage) return;

                    window.ExtendsContentIntoTitleBar = false;
                    var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var id = Win32Interop.GetWindowIdFromWindow(handle);
                    var appWindow = AppWindow.GetFromWindowId(id);
                    switch (appWindow.Presenter)
                    {
                        case OverlappedPresenter overlappedPresenter:
                            overlappedPresenter.SetBorderAndTitleBar(false, false);
                            overlappedPresenter.Maximize();
                            break;
                    }
                });
            });
        });
#endif

        builder.Services
			.AddSingleton<MainPage>()
			.AddSingleton<SharedViewModel>();

		return builder.Build();
	}
}
