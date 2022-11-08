using MauiGallery.ViewModel;

namespace MauiGallery;

public partial class MainPage : ContentPage
{
	public MainPage(SharedViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

