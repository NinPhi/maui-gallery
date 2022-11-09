using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiGallery.ViewModel;

public partial class SharedViewModel : ObservableObject
{
    private readonly ImageManager _manager;

    public Window Presenter { get; }

    [ObservableProperty]
    private ObservableCollection<FileResult> images;

    [ObservableProperty]
    private FileResult target;

    public SharedViewModel(ImageManager manager)
    {
        _manager = manager;

        Presenter = new Window(new PresenterPage(this));

        var dir = FileSystem.AppDataDirectory;
        Images = new ObservableCollection<FileResult>(
            Directory.GetFiles(dir).Select(path => new FileResult(path)));
        Target = Images.FirstOrDefault();
    }

    [RelayCommand]
    private void SetTarget(FileResult image)
    {
        if (image != null)
            Target = image;
    }

    [RelayCommand]
    private void Show(FileResult image)
    {
    }

    [RelayCommand]
    private async Task Add()
    {

    }

    [RelayCommand]
    private void Delete()
    {
        if (Images.Contains(Target))
            Images.Remove(Target);
    }
}
