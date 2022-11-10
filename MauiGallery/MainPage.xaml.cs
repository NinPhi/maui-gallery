using MauiGallery.ViewModel;
using Microsoft.UI.Windowing;
using Microsoft.UI;

namespace MauiGallery;

public partial class MainPage : ContentPage
{
    private readonly SharedViewModel _vm;

	public MainPage(SharedViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
        _vm = vm;
    }

	public void PresentImageFlyout_Clicked(object sender, EventArgs e)
	{
		var item = (MenuFlyoutItem)sender;

		if (item.BindingContext is FileResult image)
			_vm.Target = image;
		else return;

        if (!Application.Current.Windows.Contains(_vm.Presenter))
			Application.Current.OpenWindow(_vm.Presenter);
    }

	public void DeleteImageFlyout_Clicked(object sender, EventArgs e)
	{
		var item = (MenuFlyoutItem)sender;
		var image = (FileResult)item.BindingContext;

		if (_vm.DeleteCommand.CanExecute(image))
            _vm.DeleteCommand.Execute(image);
	}

	public void ManipulatePresenterFlyout_Clicked(object sender, EventArgs e)
	{
        if (_vm?.Presenter?.Handler?.PlatformView == null) return;

#if WINDOWS
        var nativeWindow = _vm.Presenter.Handler.PlatformView;
        var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
        var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        var p = appWindow.Presenter as OverlappedPresenter;

        if (_vm.IsMaximized && p.IsMinimizable)
        {
            p.Minimize();
            _vm.IsMaximized = false;
            return;
        }

        if (!_vm.IsMaximized && p.IsMaximizable)
        {
            p.Maximize();
            _vm.IsMaximized = true;
            return;
        }
#endif
    }

    public void ClosePresenterFlyout_Clicked(object sender, EventArgs e)
	{
        if (_vm?.Presenter != null && Application.Current.Windows.Contains(_vm.Presenter))
        {
            Application.Current.CloseWindow(_vm.Presenter);
            _vm.ResetPresenter();
        }
    }
}

