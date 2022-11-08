using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiGallery.Services;
using System.Collections.ObjectModel;

namespace MauiGallery.ViewModel;

public partial class SharedViewModel : ObservableObject
{
    private readonly ImageManager _manager;

    [ObservableProperty]
    private ObservableCollection<FileResult> images;

    [ObservableProperty]
    private FileResult target;

    public SharedViewModel(ImageManager manager)
    {
        _manager = manager;

        var dir = FileSystem.AppDataDirectory;
        Images = new ObservableCollection<FileResult>(
            Directory.GetFiles(dir).Select(path => new FileResult(path)));
    }

    [RelayCommand]
    private void SetTarget(FileResult image)
    {
        if (image != null)
            Target = image;
    }

    [RelayCommand]
    private void Show()
    {

    }

    [RelayCommand]
    private async Task Add()
    {
        var images = await _manager.SaveMultipleAsync();

        if (images?.Any() ?? false)
            images.ForEach(image => Images.Add(image));
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
