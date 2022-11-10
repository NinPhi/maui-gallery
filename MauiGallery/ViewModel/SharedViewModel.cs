using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiGallery.Services;
using System.Collections.ObjectModel;

namespace MauiGallery.ViewModel;

public partial class SharedViewModel : ObservableObject
{
    private readonly ImageManager _manager;

    public Window Presenter { get; private set; }
    public bool IsMaximized { get; set; }

    [ObservableProperty]
    private ObservableCollection<FileResult> images;

    [ObservableProperty]
    private FileResult target;

    public SharedViewModel(ImageManager manager)
    {
        _manager = manager;

        ResetPresenter();

        var dir = FileSystem.AppDataDirectory;
        Images = new ObservableCollection<FileResult>(
            Directory.GetFiles(dir).Select(path => new FileResult(path)));
        Target = Images.FirstOrDefault();
    }

    public void ResetPresenter()
    {
        Presenter = new Window(new PresenterPage(this));
        IsMaximized = true;
    }

    [RelayCommand]
    private async Task Add()
    {
        var images = await _manager.SaveMultipleAsync();

        if (images?.Any() ?? false)
            images.ForEach(Images.Add);
    }

    [RelayCommand]
    private void Delete(FileResult image)
    {
        if (!Images.Contains(image))
            return;

        if (_manager.TryDelete(image.FullPath))
            Images.Remove(image);
    }
}
