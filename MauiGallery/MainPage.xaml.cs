using MauiGallery.ViewModel;

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
}

