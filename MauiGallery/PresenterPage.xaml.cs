using MauiGallery.ViewModel;

namespace MauiGallery;

public partial class PresenterPage : ContentPage
{
    private readonly SharedViewModel _vm;

    public PresenterPage(SharedViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _vm = vm;
    }

    public void PresenterFlyout_Clicked(object sender, EventArgs e)
    {
        Application.Current.CloseWindow(_vm.Presenter);
    }
}